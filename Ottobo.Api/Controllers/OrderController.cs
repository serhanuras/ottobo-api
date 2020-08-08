using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using Ottobo.Api.Attributes;
using Ottobo.Api.Dtos;
using Ottobo.Entities;
using Ottobo.Services;

namespace Ottobo.Api.Controllers
{
    [ApiController]
    [LowerCaseRoute()]
    public class
        OrderController : CustomControllerBase<Order, OrderDto, OrderCreationDto, OrderFilterDto, OrderPatchDto>
    {
        private readonly OrderService _orderService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public OrderController(ILogger<OrderController> logger,
            IMapper mapper,
            OrderService orderService)
            : base(logger, mapper, orderService)
        {
            _orderService = orderService;
            _logger = logger;
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
        public new ActionResult<IEnumerable<OrderDto>> Get([FromQuery] PaginationDto paginationDto)
        {
            List<OrderDto> FilterDataMethod(PaginationDto paginationDto, OrderFilterDto orderFilterDto)
            {

                orderFilterDto.StartDate = orderFilterDto.Date;
                orderFilterDto.EndDate = orderFilterDto.Date.AddDays(1);
                
                List<Order> orders = _orderService.Filter(
                    orderFilterDto.Name,
                    orderFilterDto.CityId,
                    orderFilterDto.TownId,
                    orderFilterDto.StartDate,
                    orderFilterDto.EndDate,
                    !string.IsNullOrWhiteSpace(orderFilterDto.OrderingField) ? orderFilterDto.OrderingField : null,
                    orderFilterDto.AscendingOrder ? DataSortType.Asc : DataSortType.Desc,
                    paginationDto.Page,
                    paginationDto.RecordsPerPage
                );

                return this._mapper.Map<List<Order>, List<OrderDto>>(orders);
            }
            
            ActionResult<IEnumerable<OrderDto>> orderDtos = base.Get(paginationDto, FilterDataMethod);
            return orderDtos;
        }

        /// <summary>
        /// Get Item Type By Id
        /// </summary>
        /// <param name="id">Id of the item to get</param>
        /// <returns></returns>
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(OrderDto), 200)]
        [HttpGet("{id}")]
        public new ActionResult<OrderDto> Get(Guid id)
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
        /// <param name="updateDto"></param>
        /// <returns></returns>
        [HttpPut("{id:Guid}")]
        public new ActionResult Put(Guid id, OrderCreationDto updateDto)
        {
            return base.Put(id, updateDto);
        }

        /// <summary>
        /// Patch Updating a Item
        /// </summary>
        /// <param name="id">Id of the order type to update</param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public new ActionResult Patch(Guid id, JsonPatchDocument<OrderPatchDto> patchDocument)
        {
            return base.Patch(id, patchDocument);
        }


        /// <summary>
        /// Delete a item
        /// </summary>
        /// <param name="id">Id of the item to delete</param>
        /// <returns></returns>
        [HttpDelete("{id:Guid}")]
        public new ActionResult Delete(Guid id)
        {
            return base.Delete(id);
        }


        
          
    }
}