using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ottobo.Api.Attributes;
using Ottobo.Api.Dtos;
using Ottobo.Entities;
using Ottobo.Infrastructure.Data.PostgreSql;
using Ottobo.Services;

namespace Ottobo.Api.Controllers
{
    [ApiController]
    [LowerCaseRoute()]
    public class OrderDetailController : CustomControllerBase<OrderDetail, OrderDetailDto, OrderDetailCreationDto,
        OrderDetailFilterDto, OrderDetailPatchDto>
    {
        private readonly OrderDetailService _orderDetailService;
        private readonly IMapper _mapper;

        public OrderDetailController(ILogger<OrderDetailController> logger,
            ApplicationDbContext dbContext,
            IMapper mapper,
            OrderDetailService orderDetailService) :
            base(logger, mapper, orderDetailService)
        {
            this._orderDetailService = orderDetailService;
            this._mapper = mapper;
        }

        /// <summary>
        /// Getting all items.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("list")]
        //[ResponseCache(Duration = 60)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public new ActionResult<IEnumerable<OrderDetailDto>> Get([FromQuery] PaginationDto paginationDto)
        {
            return base.Get(paginationDto);
        }

        /// <summary>
        /// Get Item Type By Id
        /// </summary>
        /// <param name="id">Id of the item to get</param>
        /// <returns></returns>
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(OrderDetailDto), 200)]
        [HttpGet("{id}")]
        public new ActionResult<OrderDetailDto> Get(Guid id)
        {
            return base.Get(id);
        }

        /// <summary>
        /// Get Item Type By Robot Task Id
        /// </summary>
        /// <param name="robotTaskId">Id of the item to get</param>
        /// <returns></returns>
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(OrderDetailDto), 200)]
        [HttpGet("getbytaskid/{robotTaskId}")]
        public new ActionResult<List<OrderDetailDto>> GetByTaskId(Guid robotTaskId)
        {
            List<OrderDetail> orderDetails = _orderDetailService.GetByTaskId(robotTaskId);
               
            return _mapper.Map<List<OrderDetail>, List<OrderDetailDto>>(orderDetails);
        }
        
        

        /// <summary>
        /// Get Item Type By Robot Task Id
        /// </summary>
        /// <param name="robotTaskId">Id of the item to get</param>
        /// <param name="locationId"></param>
        /// <returns></returns>
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(OrderDetailDto), 200)]
        [HttpGet("getbylocationid/{robotTaskId}/{locationId}")]
        public new ActionResult<List<RobotOrderDetailDto>> GetByLocationId(Guid robotTaskId, Guid locationId)
        {
            List<OrderDetail> orderDetails =
                _orderDetailService.GetByLocationId(robotTaskId, locationId);
            
            return _mapper.Map<List<OrderDetail>, List<RobotOrderDetailDto>>(orderDetails);
        }


        /// <summary>
        /// Adding Item Type
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public new ActionResult Post(OrderDetailCreationDto creationDto)
        {
            return base.Post(creationDto);
        }


        /// <summary>
        /// Updating a Item
        /// </summary>
        /// <param name="id">Id of the order type to update</param>
        /// <param name="updateDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:Guid}")]
        public new ActionResult Put(Guid id, OrderDetailCreationDto updateDTO)
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
        public new ActionResult Patch(Guid id, JsonPatchDocument<OrderDetailPatchDto> patchDocument)
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