using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using Ottobo.Api.Dtos;
using Ottobo.Entities;
using Ottobo.Services;

namespace Ottobo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController  : CustomControllerBase<Order, OrderDto, OrderCreationDto,OrderFilterDto, OrderPatchDto>
    {

        private readonly IServiceBase<Order> _orderService;
        private readonly ILogger _logger;
        private readonly string _includeProperties;
        private readonly  IMapper _mapper;
        
        public OrderController(ILogger<OrderType> logger,
            IMapper mapper, 
            OrderService orderService,
            string includeProperties = "OrderDetails>OrderType|MasterData") : base(logger, mapper, orderService, includeProperties)
        {
            _orderService = orderService;
            _logger = logger;
            _includeProperties = includeProperties;
            _mapper = mapper;
        }
        
        
         /// <summary>
        /// Getting all items.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("list")]
        //[ResponseCache(Duration = 60)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public  new ActionResult<IEnumerable<OrderDto>> Get([FromQuery] PaginationDto paginationDto)
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
        public new ActionResult<OrderDto> Get(long id)
        {
            return base.Get(id);
        }


        /// <summary>
        /// Adding Item Type
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public new ActionResult Post(OrderCreationDto creationDto)
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
        public new ActionResult Put(int id, OrderCreationDto updateDTO)
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
        public new ActionResult Patch(int id, JsonPatchDocument<OrderPatchDto> patchDocument)
        {
            return base.Patch(id, patchDocument);
        }


        /// <summary>
        /// Delete a item
        /// </summary>
        /// <param name="id">Id of the item to delete</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
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
            Func<Entities.Order,bool> filterFunction = (s) =>
            {
                if (!string.IsNullOrWhiteSpace(orderFilterDto.Name))
                {
                    if (!s.Name.Contains(orderFilterDto.Name))
                        return false;
                }
                
                if (orderFilterDto.CityId != 0)
                {
                    if (s.CityId != orderFilterDto.CityId)
                        return false;
                }
                
                if (orderFilterDto.TownId != 0)
                {
                    if (s.TownId != orderFilterDto.TownId)
                        return false;
                }
                
                if (orderFilterDto.startDate.Year != 1 && orderFilterDto.endDate.Year != 1)
                {
                    if (!(s.Date >= orderFilterDto.startDate && s.Date <= orderFilterDto.endDate))
                        return false;
                }
                
                return true;
            };


            List<Order> orders = _orderService.Read(paginationDto.Page,
                paginationDto.RecordsPerPage,
                _includeProperties,
                filterFunction,
                !string.IsNullOrWhiteSpace(orderFilterDto.OrderingField) ? orderFilterDto.OrderingField : null,
                orderFilterDto.AscendingOrder ? DataSortType.Asc : DataSortType.Desc
            );
            
          
            return Ok(this._mapper.Map<List<Order>, List<OrderDto>>(orders));
        }

       
    }
}