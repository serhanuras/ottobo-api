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
    public class MasterDataController : CustomControllerBase<MasterData, MasterDataDto, MasterDataCreationDto,
        MasterDataFilterDto, MasterDataPatchDto>
    {
        private readonly MasterDataService _masterDataService;
        private readonly ILogger _logger;
        private readonly PurchaseTypeService _purchaseTypeService;
        private readonly IMapper _mapper;

        public MasterDataController(ILogger<MasterDataController> logger,
            IMapper mapper,
            MasterDataService masterDataService,
            PurchaseTypeService purchaseTypeService)
            : base(logger, mapper, masterDataService)
        {
            _masterDataService = masterDataService;
            _logger = logger;
            _purchaseTypeService = purchaseTypeService;
            _mapper = mapper;
        }

        /// <summary>
        /// Getting all items.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("list")]
        [ResponseCache(Duration = 60)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public new ActionResult<IEnumerable<MasterDataDto>> Get([FromQuery] PaginationDto paginationDto)
        {
            List<MasterDataDto> FilterDataMethod(PaginationDto paginationDto, MasterDataFilterDto masterDataFilterDto)
            {
            
                List<MasterData> masterDatas = this._masterDataService.Filter(
                    masterDataFilterDto.SkuCode,
                    masterDataFilterDto.Barcode,
                    masterDataFilterDto.SkuName,
                    !string.IsNullOrWhiteSpace(masterDataFilterDto.OrderingField) ? masterDataFilterDto.OrderingField : null,
                    masterDataFilterDto.AscendingOrder ? DataSortType.Asc : DataSortType.Desc,
                    paginationDto.Page,
                    paginationDto.RecordsPerPage);

                return this._mapper.Map<List<MasterData>, List<MasterDataDto>>(masterDatas);
            }
            
            return base.Get(paginationDto, FilterDataMethod);
        }

        /// <summary>
        /// Get Item Type By Id
        /// </summary>
        /// <param name="id">Id of the item to get</param>
        /// <returns></returns>
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(MasterDataDto), 200)]
        [HttpGet("{id}")]
        public new ActionResult<MasterDataDto> Get(Guid id)
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
            if (!_purchaseTypeService.Exists(creationDto.PurchaseTypeId))
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
        [HttpPut("{id:Guid}")]
        public new ActionResult Put(Guid id, MasterDataCreationDto updateDTO)
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
        public new ActionResult Patch(Guid id, JsonPatchDocument<MasterDataPatchDto> patchDocument)
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