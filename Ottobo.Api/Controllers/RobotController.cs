using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ottobo.Api.Dtos;
using Ottobo.Entities;
using Ottobo.Services;

namespace Ottobo.Api.Controllers
{
    public class RobotController: CustomControllerBase<Robot, RobotDto, RobotCreationDto, RobotFilterDto, RobotPatchDto>
    {
        
        public RobotController(ILogger<Location> logger,
            IMapper mapper, 
            RobotService robotService) : base(logger, mapper, robotService)
        {
            
        }
        
        /// <summary>
        /// Getting all locations.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("list")]
        //[ResponseCache(Duration = 60)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public new ActionResult<IEnumerable<RobotDto>> Get([FromQuery] PaginationDto paginationDto)
        {
            return base.Get(paginationDto);
        }

        /// <summary>
        /// Get Location Type By Id
        /// </summary>
        /// <param name="id">Id of the item to get</param>
        /// <returns></returns>
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(RobotDto), 200)]
        [HttpGet("{id}")]
        public new ActionResult<RobotDto> Get(long id)
        {
            return base.Get(id);
        }


        /// <summary>
        /// Adding Item Type
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public new ActionResult Post(RobotCreationDto creationDto)
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
        public new ActionResult Put(int id, RobotCreationDto updateDto)
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