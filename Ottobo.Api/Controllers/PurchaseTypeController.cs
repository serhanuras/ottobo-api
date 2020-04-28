using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ottobo.Api.Dtos;
using Ottobo.Data.Provider.IRepository;
using Ottobo.Entities;

namespace Ottobo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseTypeController : OttoboBaseController<PurchaseType, PurchaseTypeDto, PurchaseTypeCreationDto, PurchaseTypeFilterDto, PurchaseTypePatchDto>
    {
        
        public PurchaseTypeController(ILogger<PurchaseTypeController> logger,
            IMapper mapper, 
            IUnitOfWork unitOfWork) : base(logger, mapper, unitOfWork)
        {
            
        }

        /// <summary>
        /// Getting all items.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("list")]
        //[ResponseCache(Duration = 60)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public  ActionResult<IEnumerable<PurchaseTypeDto>> Get([FromQuery] PaginationDto paginationDto)
        {
            return base.Get(paginationDto);
        }

        /// <summary>
        /// Get Item Type By Id
        /// </summary>
        /// <param name="id">Id of the item to get</param>
        /// <returns></returns>
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(OrderTypeDto), 200)]
        [HttpGet("{id}")]
        public ActionResult<PurchaseTypeDto> Get(long id)
        {
            return base.Get(id);
        }


        /// <summary>
        /// Adding Item Type
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post(PurchaseTypeCreationDto creationDto)
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
        public ActionResult Put(int id, PurchaseTypeCreationDto updateDTO)
        {
            return base.Put(id, updateDTO);
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
