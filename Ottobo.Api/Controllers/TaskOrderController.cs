using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ottobo.Api.Dtos;
using Ottobo.Entities;
using Ottobo.Services;

namespace Ottobo.Api.Controllers
{
    public class TaskOrderController: CustomControllerBase<TaskOrder, TaskOrderDto, TaskOrderCreationDto, TaskOrderFilterDto, TaskOrderPatchDto>
    {
        
        public TaskOrderController(ILogger<Location> logger,
            IMapper mapper, 
            TaskOrderService taskOrderService) : base(logger, mapper, taskOrderService)
        {
            
        }
        
        /// <summary>
        /// Getting all task orders.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("list")]
        //[ResponseCache(Duration = 60)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public new ActionResult<IEnumerable<TaskOrderDto>> Get([FromQuery] PaginationDto paginationDto)
        {
            return base.Get(paginationDto);
        }

        /// <summary>
        /// Get Location Type By Id
        /// </summary>
        /// <param name="id">Id of the item to get</param>
        /// <returns></returns>
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(TaskOrderDto), 200)]
        [HttpGet("{id}")]
        public new ActionResult<TaskOrderDto> Get(long id)
        {
            return base.Get(id);
        }


        /// <summary>
        /// Adding Item Type
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public new ActionResult Post(TaskOrderCreationDto creationDto)
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
        public new ActionResult Put(int id, TaskOrderCreationDto updateDto)
        {
            return base.Put(id, updateDto);
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

    }
}