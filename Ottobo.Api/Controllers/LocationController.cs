using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ottobo.Api.Dtos;
using Ottobo.Entities;
using Ottobo.Services;

namespace Ottobo.Api.Controllers
{
    public class LocationController: CustomControllerBase<Location, LocationDto, LocationCreationDto, LocationFilterDto, LocationPatchDto>
    {
        
        public LocationController(ILogger<Location> logger,
            IMapper mapper, 
            LocationService locationService) : base(logger, mapper, locationService)
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
        public new ActionResult<IEnumerable<LocationDto>> Get([FromQuery] PaginationDto paginationDto)
        {
            return base.Get(paginationDto);
        }

        /// <summary>
        /// Get Location Type By Id
        /// </summary>
        /// <param name="id">Id of the item to get</param>
        /// <returns></returns>
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(LocationDto), 200)]
        [HttpGet("{id}")]
        public new ActionResult<LocationDto> Get(long id)
        {
            return base.Get(id);
        }


        /// <summary>
        /// Adding Item Type
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public new ActionResult Post(LocationCreationDto creationDto)
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
        public new ActionResult Put(int id, LocationCreationDto updateDto)
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