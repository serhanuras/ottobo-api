using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using Ottobo.Api.Dtos;
using Ottobo.Data.Provider.IRepository;
using Ottobo.Entities;

namespace Ottobo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController  : OttoboBaseController<Order, OrderDto, OrderCreationDto,OrderFilterDto, OrderPatchDto>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        
        public OrderController(ILogger<OrderTypeController> logger,
            IMapper mapper, 
            IUnitOfWork unitOfWork) : base(logger, mapper, unitOfWork, "OrderDetails>OrderType|MasterData")
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        
        
         /// <summary>
        /// Getting all items.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("list")]
        //[ResponseCache(Duration = 60)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public  ActionResult<IEnumerable<OrderDto>> Get([FromQuery] PaginationDto paginationDto)
        {
            return base.Get(paginationDto);
        }

        /// <summary>
        /// Get Item Type By Id
        /// </summary>
        /// <param name="id">Id of the item to get</param>
        /// <returns></returns>
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(OrderDto), 200)]
        [HttpGet("{id}")]
        public ActionResult<OrderDto> Get(long id)
        {
            return base.Get(id);
        }


        /// <summary>
        /// Adding Item Type
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post(OrderCreationDto creationDto)
        {
            return base.Post(creationDto);
        }


        /// <summary>
        /// Updating a Item
        /// </summary>
        /// <param name="id">Id of the order type to update</param>
        /// <param name="updateDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, OrderCreationDto updateDTO)
        {
            return base.Put(id, updateDTO);
        }

        /// <summary>
        /// Patch Updating a Item
        /// </summary>
        /// <param name="id">Id of the order type to update</param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public ActionResult Patch(int id, JsonPatchDocument<OrderPatchDto> patchDocument)
        {
            return base.Patch(id, patchDocument);
        }


        /// <summary>
        /// Delete a item
        /// </summary>
        /// <param name="id">Id of the item to delete</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public  ActionResult Delete(int id)
        {
            return base.Delete(id);
        }


        /// <summary>
        /// Getting paged filtered orders.
        /// </summary>
        /// <returns></returns>
        [HttpGet("filter")]
        public  ActionResult<List<OrderDto>> Filter([FromQuery]PaginationDto paginationDto, OrderFilterDto orderFilterDto)
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