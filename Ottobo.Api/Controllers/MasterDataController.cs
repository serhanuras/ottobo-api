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
    public class MasterDataController :  OttoboBaseController<MasterData, MasterDataDto, MasterDataCreationDto, MasterDataFilterDto, MasterDataPatchDto>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        
        public MasterDataController(ILogger<MasterDataController> logger,
            IMapper mapper, 
            IUnitOfWork unitOfWork) : base(logger, mapper, unitOfWork, "PurchaseType,Stock>StockType")
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
        public  ActionResult<IEnumerable<MasterDataDto>> Get([FromQuery] PaginationDto paginationDto)
        {
            return base.Get(paginationDto);
        }

        /// <summary>
        /// Get Item Type By Id
        /// </summary>
        /// <param name="id">Id of the item to get</param>
        /// <returns></returns>
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(MasterDataDto), 200)]
        [HttpGet("{id}")]
        public ActionResult<MasterDataDto> Get(long id)
        {
            return base.Get(id);
        }


        /// <summary>
        /// Adding Item Type
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post(MasterDataCreationDto creationDto)
        {
            if (!_unitOfWork.GetRepository<PurchaseType>().Exists(creationDto.PurchaseTypeId))
            {
                return BadRequest(new ErrorDto("Invalid Purchase Type Id"));
            }
            
            return base.Post(creationDto);
        }


        /// <summary>
        /// Updating a Item
        /// </summary>
        /// <param name="id">Id of the order type to update</param>
        /// <param name="updateDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, MasterDataCreationDto updateDTO)
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
        public ActionResult Patch(int id, JsonPatchDocument<MasterDataPatchDto> patchDocument)
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
        
        
        /// <summary>
        /// Getting paged filtered orders.
        /// </summary>
        /// <returns></returns>
        [HttpGet("filter")]
        public  ActionResult<List<MasterDataDto>> Filter([FromQuery]PaginationDto paginationDto, MasterDataFilterDto masterDataFilterDto)
        {
            
            IQueryable<MasterData> masterDatasQueryable =_unitOfWork.GetRepository<MasterData>().Queryable();


            if (masterDataFilterDto.SkuCode != null)
            {
                masterDatasQueryable = masterDatasQueryable
                    .Where(x => x.SkuCode.Contains(masterDataFilterDto.SkuCode));
            }
            
            if (masterDataFilterDto.Barcode != null)
            {
                masterDatasQueryable = masterDatasQueryable
                    .Where(x => x.Barcode.Contains(masterDataFilterDto.Barcode));
            }
            
            if (masterDataFilterDto.SkuName != null)
            {
                masterDatasQueryable = masterDatasQueryable
                    .Where(x => x.SkuName.Contains(masterDataFilterDto.SkuName));
            }
            

            if (!string.IsNullOrWhiteSpace(masterDataFilterDto.OrderingField))
            {
                try
                {
                    masterDatasQueryable = masterDatasQueryable
                        .OrderBy($"{masterDataFilterDto.OrderingField} {(masterDataFilterDto.AscendingOrder ? "ascending" : "descending")}");
                }
                catch
                {
                    // log this
                    _logger.LogWarning("Could not order by field: " + masterDataFilterDto.OrderingField);
                }
            }

            
            var masterDatas = _unitOfWork.GetRepository<MasterData>().GetAll(masterDatasQueryable,"PurchaseType,Stock", paginationDto.Page, paginationDto.RecordsPerPage);

            return Ok(masterDatas);
        }

    }
}