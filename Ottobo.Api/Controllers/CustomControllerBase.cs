using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using Ottobo.Api.Dtos;
using Ottobo.Api.RouteResult;
using Ottobo.Entities;
using Ottobo.Services;

namespace Ottobo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class CustomControllerBase<TEntity, TDto, TCreationDto, TFilterDto, TPatchDto>: ControllerBase
        where TEntity:class, IEntity
        where TDto: class, IDto
        where TCreationDto: class, ICreationDto
        where TFilterDto: class, IFilterDto
        where TPatchDto : class, IPatchDto
    
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IServiceBase<TEntity> _service;
        private readonly string _includeProperties;

        public CustomControllerBase(
            ILogger logger,
            IMapper mapper, 
            IServiceBase<TEntity> service,
            string includeProperties="")
        {
            _logger = logger;
            _mapper = mapper;
            _service = service;
            _includeProperties = includeProperties;
        }
        
       
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult<IEnumerable<TDto>> Get(PaginationDto paginationDto)
        {
            if (paginationDto == null)
            {
                var list = _service.Read(_includeProperties);

                var dtoList = _mapper.Map<List<TDto>>(list);

                return Ok(dtoList);
            }
            else
            {
                
                var list = _service.Read(_includeProperties, paginationDto.Page, paginationDto.RecordsPerPage);

                var dtoList = _mapper.Map<List<TDto>>(list);

                return Ok(dtoList);
            }
        }
        
        [ApiExplorerSettings(IgnoreApi = true)]
        public  ActionResult<TDto> Get(long id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = _service.Read(id, this._includeProperties);
            
            if (item == null)
            {
                _logger.LogWarning($"There is not record according to this id. Id is {id}");
                return NotFound();
            }
           
            var dtoItem = _mapper.Map<TDto>(item);
            
            return Ok(dtoItem);
        }
        

        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult Post(TCreationDto creationDto)
        {
            var item = _mapper.Map<TEntity>(creationDto);
                                           
            _service.Create(item);
            
            var itemDto = _mapper.Map<TCreationDto>(item);
            
            return new CustomCreatedAtRouteResult(item.Id, itemDto);
        }
        
        
        [ApiExplorerSettings(IgnoreApi = true)]
        public  ActionResult Put(int id,  TCreationDto updateDTO)
        {

            var item = _mapper.Map<TEntity>(updateDTO);
            item.Id = id;

            _service.Update(item);

            return NoContent();
        }
        
        [ApiExplorerSettings(IgnoreApi = true)]
        public  ActionResult Patch(int id, JsonPatchDocument<TPatchDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var item  = _service.Read(id, _includeProperties);

            var tPatchDto = _mapper.Map<TPatchDto>(item);

            patchDocument.ApplyTo(tPatchDto, ModelState);

            var isValid = TryValidateModel(tPatchDto);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(tPatchDto, item);

            _service.Update(item);

            return NoContent();
        }
        
        
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult Delete(long id)
        {

          _service.Delete(id);

            return NoContent();
        }
        
        
    }
}