using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using Ottobo.Api.Dtos;
using Ottobo.Api.RouteResult;
using Ottobo.Data.Provider.IRepository;
using Ottobo.Entities;

namespace Ottobo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class OttoboBaseController<TEntity, TDto, TCreationDto, TFilterDto, TPatchDto>: ControllerBase
        where TEntity:class, IEntity
        where TDto: class, IDto
        where TCreationDto: class, ICreationDto
        where TFilterDto: class, IFilterDto
        where TPatchDto : class, IPatchDto
    
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<TEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _includeProperties;

        public OttoboBaseController(
            ILogger logger,
            IMapper mapper, 
            IUnitOfWork unitOfWork,
            string includeProperties="")
        {

            
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<TEntity>();
            _includeProperties = includeProperties;
        }
        
       
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult<IEnumerable<TDto>> Get(PaginationDto paginationDto)
        {
            if (paginationDto == null)
            {
                var list = _repository.GetAll(null,
                    null,_includeProperties);

                var dtoList = _mapper.Map<List<TDto>>(list);

                return Ok(dtoList);
            }
            else
            {
                
                var list = _repository.GetAll(null,
                    null,_includeProperties,paginationDto.Page, paginationDto.RecordsPerPage);

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

            var item = _repository.Get(id, _includeProperties);
            
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

            item = _repository.Add(item);


            _unitOfWork.Save();
            var itemDto = _mapper.Map<TCreationDto>(item);
            

            return new CustomCreatedAtRouteResult(item.Id, itemDto);
            //return  Ok(new { Id = item.Id });
        }
        
        
        [ApiExplorerSettings(IgnoreApi = true)]
        public  ActionResult Put(int id,  TCreationDto updateDTO)
        {
            if (!_repository.Exists(id))
                return NotFound();
            
            var item = _mapper.Map<TEntity>(updateDTO);
            item.Id = id;

            _repository.Update(item);
            
            _unitOfWork.Save();
            
            return NoContent();
        }
        
        [ApiExplorerSettings(IgnoreApi = true)]
        public  ActionResult Patch(int id, JsonPatchDocument<TPatchDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            

            var item  = _unitOfWork.GetRepository<TEntity>().GetFirstOrDefault(x => x.Id == id, _includeProperties);

            if (item == null)
            {
                return NotFound();
            }

            var tPatchDto = _mapper.Map<TPatchDto>(item);

            patchDocument.ApplyTo(tPatchDto, ModelState);

            var isValid = TryValidateModel(tPatchDto);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(tPatchDto, item);

            _unitOfWork.Save();

            return NoContent();
        }
        
        
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult Delete(int id)
        {

            if (!_repository.Exists(id))
                return NotFound();

            _repository.Remove(id);
            
            _unitOfWork.Save();

            return NoContent();
        }
        
        
    }
}