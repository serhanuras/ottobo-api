using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.Extensions.Logging;
using Ottobo.Api.Dtos;
using Ottobo.Data.Provider.IRepository;
using Ottobo.Entities;

namespace Ottobo.Api.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController  : OttoboBaseController<Order, OrderDto, OrderCreationDto,OrderFilterDto, OrderPatchDto>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        
        public OrderController(ILogger<OrderTypeController> logger,
            IMapper mapper, 
            IUnitOfWork unitOfWork) : base(logger, mapper, unitOfWork, "OrderDetails>OrderType|Stock")
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
       

        /// <summary>
        /// Getting paged filtered orders.
        /// </summary>
        /// <returns></returns>
        [HttpGet("filter")]
        public override async Task<ActionResult<List<OrderDto>>> Filter(PaginationDto paginationDto, OrderFilterDto orderFilterDto)
        {
           
            
            IQueryable<Order> ordersQueryable = _unitOfWork.GetRepository<Order>().Queryable();
            
           
           

            if (!string.IsNullOrWhiteSpace(orderFilterDto.Name))
            {
                ordersQueryable = ordersQueryable.Where(x => x.Name.Contains(orderFilterDto.Name));
            }

            if (orderFilterDto.CityId != 0)
            {
                ordersQueryable = ordersQueryable
                    .Where(x => x.CityId == orderFilterDto.CityId);
            }

            if (orderFilterDto.TownId != 0)
            {
                ordersQueryable = ordersQueryable
                    .Where(x => x.TownId == orderFilterDto.TownId);
            }

            if (orderFilterDto.startDate.Year != 1 && orderFilterDto.endDate.Year != 1)
            {
                ordersQueryable = ordersQueryable
                    .Where(x => (x.Date >= orderFilterDto.startDate && x.Date <= orderFilterDto.endDate));
            }

            if (!string.IsNullOrWhiteSpace(orderFilterDto.OrderingField))
            {
                try
                {
                    ordersQueryable = ordersQueryable
                        .OrderBy($"{orderFilterDto.OrderingField} {(orderFilterDto.AscendingOrder ? "ascending" : "descending")}");
                }
                catch
                {
                    // log this
                    _logger.LogWarning("Could not order by field: " + orderFilterDto.OrderingField);
                }
            }

         
            
            var orders = _unitOfWork.GetRepository<Order>().GetAll(ordersQueryable,"OrderDetails", paginationDto.Page, paginationDto.RecordsPerPage);

            return Ok(orders);
        }

       
    }
}