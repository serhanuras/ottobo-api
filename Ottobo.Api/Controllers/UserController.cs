using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ottobo.Api.Attributes;
using Ottobo.Api.Dtos;
using Ottobo.Api.RouteResult;
using Ottobo.Entities;
using Ottobo.Infrastructure.Data.PostgreSql;
using Ottobo.Infrastructure.Exceptions;
using Ottobo.Infrastructure.Extensions;
using Ottobo.Services;

namespace Ottobo.Api.Controllers
{
    [ApiController]
    [LowerCaseRoute()]
    public class UserController : CustomControllerBase<User, UserDto, UserCreationDto, UserFilterDto, UserPatchDto>
    {
        private readonly UserService _userService;
        private readonly RoleService _roleService;
        private readonly IMapper _mapper;

        public UserController(ILogger<PurchaseTypeController> logger,
            IMapper mapper, 
            UserService userService,
            RoleService roleService) : base(logger, mapper, userService)
        {
            _userService = userService;
            _mapper = mapper;
            _roleService = roleService;
        }

        /// <summary>
        /// Getting all items.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("list")]
        [ResponseCache(Duration = 60)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public  new ActionResult<IEnumerable<UserDto>> Get([FromQuery] PaginationDto paginationDto)
        {
            List<UserDto> FilterDataMethod(PaginationDto paginationDto, UserFilterDto userFilterDto)
            {
                DataSortType dataSortType = userFilterDto.AscendingOrder ? DataSortType.Asc : DataSortType.Desc;
                string orderingField = !string.IsNullOrWhiteSpace(userFilterDto.OrderingField)
                    ? userFilterDto.OrderingField
                    : null;

                return 
                    this._mapper.Map<List<UserDto>>(this._userService.Filter(orderingField, dataSortType, paginationDto.Page,
                    paginationDto.RecordsPerPage, userFilterDto.FirstName, userFilterDto.LastName, userFilterDto.Email));

            }
            
            return base.Get(paginationDto, FilterDataMethod);
        }

        /// <summary>
        /// Get Item Type By Id
        /// </summary>
        /// <param name="id">Id of the item to get</param>
        /// <returns></returns>
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(UserDto), 200)]
        [HttpGet("{id}")]
        public new ActionResult<UserDto> Get(Guid id)
        {
            return base.Get(id);
        }


        /// <summary>
        /// Adding Item Type
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<UserDto>> Post(UserCreationDto creationDto)
        {
            creationDto.Password = creationDto.Password.HashPassword(creationDto.Email);
            return base.Post(creationDto);

        }


        /// <summary>
        /// Updating a Item
        /// </summary>
        /// <param name="id">Id of the order type to update</param>
        /// <param name="updateDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> Put(Guid id, UserCreationDto updateDTO)
        {

            updateDTO.Password = updateDTO.Password.HashPassword(updateDTO.Email);
            return base.Post(updateDTO);
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