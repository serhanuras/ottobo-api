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
    public class LocationController: CustomControllerBase<Location, LocationDto, LocationCreationDto, LocationFilterDto, LocationPatchDto>
    {
        private readonly LocationService _locationService;
        private readonly IMapper _mapper;
        
        public LocationController(ILogger<LocationController> logger,
            IMapper mapper, 
            LocationService locationService) : base(logger, mapper, locationService)
        {
            this._locationService = locationService;
            this._mapper = mapper;
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
        public new ActionResult<LocationDto> Get(Guid id)
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
        /// <param name="updateDto"></param>
        /// <returns></returns>
        [HttpPut("{id:Guid}")]
        public new ActionResult Put(Guid id, LocationCreationDto updateDto)
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
        /// <summary>
        /// GetLocations
        /// </summary>
        /// <param name="id">Id of the item to delete</param>
        /// <returns></returns>
        [HttpGet("getbytaskid/{robotTaskId:Guid}")]
        public new ActionResult<List<LocationDto>> GetByTaskId(Guid robotTaskId)
        {
            List<Location> orderDetails = _locationService.GetLocationsByTaskId(robotTaskId);

            return _mapper.Map<List<Location>, List<LocationDto>>(orderDetails);
        }   
        

        
        /// <summary>
        /// Delete a item
        /// </summary>
        /// <param name="id">Id of the item to delete</param>
        /// <returns></returns>
        [HttpGet("getnextlocation/{robotTaskId:Guid}")]
        public new ActionResult<LocationDto> GetNextLocation(Guid robotTaskId, [FromQuery] Guid? currentLocationId)
        {
            return this._mapper.Map<LocationDto>( this._locationService.GetNextLocation(robotTaskId, currentLocationId));
        }   
    }
}