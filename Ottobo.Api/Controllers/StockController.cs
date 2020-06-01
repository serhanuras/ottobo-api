using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
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
        StockController : CustomControllerBase<Stock, StockDto, StockCreationDto, StockFilterDto, StockPatchDto>
    {
        private readonly ILogger _logger;
        private readonly StockService _stockService;

        public StockController(ILogger<StockController> logger,
            IMapper mapper,
            StockService stockService) : 
            base(logger, mapper, stockService)
        {
            _logger = logger;
            _stockService = stockService;
        }

        /// <summary>
        /// Getting all items.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("list")]
        //[ResponseCache(Duration = 60)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public new ActionResult<IEnumerable<StockDto>> Get([FromQuery] PaginationDto paginationDto)
        {
            return base.Get(paginationDto);
        }

        /// <summary>
        /// Get Item Type By Id
        /// </summary>
        /// <param name="id">Id of the item to get</param>
        /// <returns></returns>
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(StockDto), 200)]
        [HttpGet("{id}")]
        public new ActionResult<StockDto> Get(Guid id)
        {
            return base.Get(id);
        }

        /// <summary>
        /// Adding Item Type
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public new ActionResult Post(StockCreationDto creationDto)
        {
            if (_stockService.Read((s) => (s.MasterDataId == creationDto.MasterDataId)).Count > 0)
                return BadRequest(new ErrorDto("Stock Data already exists please use Put to update it."));

            return base.Post(creationDto);
        }


        /// <summary>
        /// Updating a Item
        /// </summary>
        /// <param name="id">Id of the order type to update</param>
        /// <param name="updateDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:Guid}")]
        public new ActionResult Put(Guid id, StockCreationDto updateDTO)
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
        public new ActionResult Patch(Guid id, JsonPatchDocument<StockPatchDto> patchDocument)
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