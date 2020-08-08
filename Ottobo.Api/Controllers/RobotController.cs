using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ottobo.Api.Attributes;
using Ottobo.Api.Dtos;
using Ottobo.Entities;
using Ottobo.Services;

namespace Ottobo.Api.Controllers
{
    [ApiController]
    [LowerCaseRoute()]
    public class RobotController: CustomControllerBase<Robot, RobotDto, RobotCreationDto, RobotFilterDto, RobotPatchDto>
    {
        
        public RobotController(ILogger<RobotController> logger,
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
            List<RobotDto> FilterDataMethod(PaginationDto paginationDto, RobotFilterDto robotFilterDto)
            {
            
                return null;
            }
            
            return base.Get(paginationDto,FilterDataMethod);
        }

        /// <summary>
        /// Get Location Type By Id
        /// </summary>
        /// <param name="id">Id of the item to get</param>
        /// <returns></returns>
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(RobotDto), 200)]
        [HttpGet("{id}")]
        public new ActionResult<RobotDto> Get(Guid id)
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
        /// <param name="updateDto"></param>
        /// <returns></returns>
        [HttpPut("{id:Guid}")]
        public new ActionResult Put(Guid id, RobotCreationDto updateDto)
        {
            return base.Put(id, updateDto);
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