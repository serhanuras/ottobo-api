using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using Ottobo.Api.Dtos;
using Ottobo.Entities;
using Ottobo.Services;

namespace Ottobo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MasterDataController : CustomControllerBase<MasterData, MasterDataDto, MasterDataCreationDto,
        MasterDataFilterDto, MasterDataPatchDto>
    {
        private readonly MasterDataService _masterDataService;
        private readonly ILogger _logger;
        private readonly PurchaseTypeService _purchaseTypeService;
        private readonly string _includeProperties;
        private readonly  IMapper _mapper;

        public MasterDataController(ILogger<MasterData> logger,
            IMapper mapper,
            MasterDataService masterDataService,
            PurchaseTypeService purchaseTypeService,
            string includeProperties= "PurchaseType,Stock>StockType")
            : base(logger, mapper, masterDataService, includeProperties)
        {
            _masterDataService = masterDataService;
            _logger = logger;
            _purchaseTypeService = purchaseTypeService;
            _includeProperties = includeProperties;
            _mapper = mapper;
        }

        /// <summary>
        /// Getting all items.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("list")]
        //[ResponseCache(Duration = 60)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public new ActionResult<IEnumerable<MasterDataDto>> Get([FromQuery] PaginationDto paginationDto)
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
        public new ActionResult<MasterDataDto> Get(long id)
        {
            return base.Get(id);
        }


        /// <summary>
        /// Adding Item Type
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public new ActionResult Post(MasterDataCreationDto creationDto)
        {
            if(!_purchaseTypeService.Exists(creationDto.PurchaseTypeId))
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
        public new ActionResult Put(int id, MasterDataCreationDto updateDTO)
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
        public new ActionResult Patch(int id, JsonPatchDocument<MasterDataPatchDto> patchDocument)
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
        public ActionResult<List<MasterDataDto>> Filter([FromQuery] PaginationDto paginationDto,
            MasterDataFilterDto masterDataFilterDto)
        {
            Func<MasterData,bool> filterFunction = (s) =>
            {
                if (masterDataFilterDto.SkuCode != null)
                {
                    if (!s.SkuCode.Contains(masterDataFilterDto.SkuCode))
                        return false;
                }
                
                if (masterDataFilterDto.Barcode != null)
                {
                    if (!s.SkuCode.Contains(masterDataFilterDto.Barcode))
                        return false;
                }
                
                if (masterDataFilterDto.SkuName != null)
                {
                    if (!s.SkuCode.Contains(masterDataFilterDto.SkuName))
                        return false;
                }

                return true;
            };

            List<MasterData> orders = _masterDataService.Read(paginationDto.Page,
                paginationDto.RecordsPerPage,
                _includeProperties,
                filterFunction,
                !string.IsNullOrWhiteSpace(masterDataFilterDto.OrderingField) ? masterDataFilterDto.OrderingField : null,
                masterDataFilterDto.AscendingOrder ? DataSortType.Asc : DataSortType.Desc
            );
            

            return  Ok(this._mapper.Map<List<MasterData>, List<MasterDataDto>>(orders));
        }
    }
}