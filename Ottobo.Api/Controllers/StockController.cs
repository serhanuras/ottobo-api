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
    public class StockController :  OttoboBaseController<Stock, StockDto, StockCreationDto, StockFilterDto, StockPatchDto>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        
        public StockController(ILogger<StockController> logger,
            IMapper mapper, 
            IUnitOfWork unitOfWork) : base(logger, mapper, unitOfWork, "StockType")
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
        public  ActionResult<IEnumerable<StockDto>> Get([FromQuery] PaginationDto paginationDto)
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
        public ActionResult<StockDto> Get(long id)
        {
            return base.Get(id);
        }

        
        /// <summary>
        /// Updating a Item
        /// </summary>
        /// <param name="id">Id of the order type to update</param>
        /// <param name="updateDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, StockCreationDto updateDTO)
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
        public ActionResult Patch(int id, JsonPatchDocument<StockPatchDto> patchDocument)
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


    }
}