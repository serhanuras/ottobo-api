using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using Ottobo.Api.Attributes;
using Ottobo.Api.Dtos;
using Ottobo.Api.RouteResult;
using Ottobo.Entities;
using Ottobo.Services;

namespace Ottobo.Api.Controllers
{
    [ApiController]
    [LowerCaseRoute()]
    public abstract class CustomControllerBase<TEntity, TDto, TCreationDto, TFilterDto, TPatchDto>: ControllerBase
        where TEntity:class, IEntityBase
        where TDto: class, IDto
        where TCreationDto: class, ICreationDto
        where TFilterDto: class, IFilterDto
        where TPatchDto : class, IPatchDto
    
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IServiceBase<TEntity> _service;

        public CustomControllerBase(
            ILogger logger,
            IMapper mapper, 
            IServiceBase<TEntity> service)
        {
            _logger = logger;
            _mapper = mapper;
            _service = service;
        }
        
       
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult<IEnumerable<TDto>> Get(PaginationDto paginationDto)
        {
            if (paginationDto == null)
            {
                var list = _service.Read();

                var dtoList = _mapper.Map<List<TDto>>(list);

                return Ok(dtoList);
            }
            else
            {
                
                var list = _service.Read(paginationDto.Page, paginationDto.RecordsPerPage);

                var dtoList = _mapper.Map<List<TDto>>(list);

                return Ok(dtoList);
            }
        }
        
        [ApiExplorerSettings(IgnoreApi = true)]
        public  ActionResult<TDto> Get(Guid id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = _service.Read(id);
            
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

            TEntity item = _service.Create(_mapper.Map<TEntity>(creationDto));
            
            var itemDto = _mapper.Map<TDto>(item);
            
            return new CustomCreatedAtRouteResult(item.Id, itemDto);
        }
        
        
        [ApiExplorerSettings(IgnoreApi = true)]
        public  ActionResult Put(Guid id,  TCreationDto updateDTO)
        {

            var item = _mapper.Map<TEntity>(updateDTO);
            item.Id = id;

            _service.Update(item);

            return NoContent();
        }
        
        [ApiExplorerSettings(IgnoreApi = true)]
        public  ActionResult Patch(Guid id, JsonPatchDocument<TPatchDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var item  = _service.Read(id);

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
        public ActionResult Delete(Guid id)
        {

          _service.Delete(id);

            return NoContent();
        }
        
        
    }
}