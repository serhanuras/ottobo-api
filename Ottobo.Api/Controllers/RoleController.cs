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
    public class RoleController:CustomControllerBase<Role, RoleDto, RoleCreationDto, RoleFilterDto, RolePatchDto>
    {
        private readonly RoleService _roleService;
        private readonly IMapper _mapper;

        public RoleController(ILogger<PurchaseTypeController> logger,
            IMapper mapper,
            RoleService roleService) : base(logger, mapper, roleService)
        {
            _roleService = roleService;
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
        public new ActionResult<IEnumerable<RoleDto>> Get([FromQuery] PaginationDto paginationDto)
        {
            List<RoleDto> FilterDataMethod(PaginationDto paginationDto, RoleFilterDto roleFilterDto)
            {

                DataSortType dataSortType = roleFilterDto.AscendingOrder ? DataSortType.Asc : DataSortType.Desc;
                string orderingField = !string.IsNullOrWhiteSpace(roleFilterDto.OrderingField)
                    ? roleFilterDto.OrderingField
                    : null;

                return 
                    this._mapper.Map<List<RoleDto>>(this._roleService.Filter(orderingField, dataSortType, paginationDto.Page,
                        paginationDto.RecordsPerPage, null));
            }

            return base.Get(paginationDto, FilterDataMethod);
        }

        /// <summary>
        /// Get Item Type By Id
        /// </summary>
        /// <param name="id">Id of the item to get</param>
        /// <returns></returns>
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(RoleDto), 200)]
        [HttpGet("{id}")]
        public new ActionResult<UserDto> Get(Guid id)
        {
            return base.Get(id);
        }


        /// <summary>
        /// Adding Item Type
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public new ActionResult Post(RoleCreationDto creationDto)
        {
            return base.Post(creationDto);
        }


        /// <summary>
        /// Updating a Item
        /// </summary>
        /// <param name="id">Id of the order type to update</param>
        /// <param name="updateDTO"></param>
        /// <returns></returns>
        //[HttpPut("{id:Guid}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public new ActionResult Put(Guid id, RoleCreationDto updateDTO)
        {
            return base.Put(id, updateDTO);
        }


        /// <summary>
        /// Delete a item
        /// </summary>
        /// <param name="id">Id of the item to delete</param>
        /// <returns></returns>
        //[HttpDelete("{id:Guid}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public new ActionResult Delete(Guid id)
        {
            return base.Delete(id);
        }

    }
}