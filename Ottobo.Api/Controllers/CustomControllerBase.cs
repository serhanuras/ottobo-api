using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Ottobo.Api.Attributes;
using Ottobo.Api.Dtos;
using Ottobo.Api.RouteResult;
using Ottobo.Entities;
using Ottobo.Infrastructure.Extensions;
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

        
        public delegate List<TDto> FilterData(PaginationDto paginationDto, TFilterDto filterDto);


        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public ActionResult Get(PaginationDto paginationDto, FilterData filterData)
        {

            var _id = HttpContext.Request.Query["id"].ToString();
            if (_id != "")
            {
                return Get(new Guid(_id));
            }

            var _start = HttpContext.Request.Query["_start"].ToString();
            var _end = HttpContext.Request.Query["_end"].ToString();
            var _sort = HttpContext.Request.Query["_sort"].ToString();
            if (_start != "" && _end != "" && _sort != "")
            {
                paginationDto = new PaginationDto();

                paginationDto.RecordsPerPage = int.Parse(_end) - int.Parse(_start);
                paginationDto.Page = int.Parse(_end) / paginationDto.RecordsPerPage;

            }

            Response.Headers.Add("Access-Control-Expose-Headers", "X-Total-Count");

            //_end=5&_order=ASC&_sort=id&_start=0&date=2020-08-15
            var jsonFilterData = "{";
            
            foreach (var query in HttpContext.Request.Query)
            {
                if (query.Key.ToString() != "id" 
                    && query.Key.ToString() != "_end" &&
                    query.Key.ToString() != "_start")
                {
                    
                    if (query.Key.ToString() == "_order")
                    {
                        if(query.Value.ToString()=="ASC")
                            jsonFilterData += $"\"AscendingOrder\":true,";
                        else
                            jsonFilterData += $"\"AscendingOrder\":false,";
                    }
                    else if (query.Key.ToString() == "_sort")
                    {
                        jsonFilterData += $"\"OrderingField\":\"{query.Value.ToString().CapitalizeFirstLetter()}\",";
                       
                    }
                    else
                    {
                        jsonFilterData +=
                            $"\"{query.Key.ToString().CapitalizeFirstLetter()}\":\"{query.Value.ToString()}\",";
                    }

                }
            }

            if (jsonFilterData != "{")
                jsonFilterData = jsonFilterData.Substring(0, jsonFilterData.Length - 1);

            jsonFilterData += "}";

            if (jsonFilterData != "{}")
            {
                var dto = JsonConvert.DeserializeObject<TFilterDto>(jsonFilterData);
                paginationDto.Page = paginationDto.Page;
                var list = filterData(paginationDto, dto);

                var page = list.Count / paginationDto.RecordsPerPage;
                Response.Headers.Add("total", list.Count.ToString());
                Response.Headers.Add("X-Total-Count",
                    (page + 1).ToString());

                return Ok(list);
            }
            else
            {
                if (paginationDto == null)
                {
                    var list = _service.Read();

                    var dtoList = _mapper.Map<List<TDto>>(list);

                    Response.Headers.Add("total", _service.Count().ToString());

                    return Ok(dtoList);
                }
                else
                {

                    var list = _service.Read(paginationDto.Page, paginationDto.RecordsPerPage);

                    var dtoList = _mapper.Map<List<TDto>>(list);

                    Response.Headers.Add("total", _service.Count().ToString());
                    Response.Headers.Add("X-Total-Count",
                        ((int) _service.Count() / paginationDto.RecordsPerPage).ToString());


                    return Ok(dtoList);
                }

            }

        }
        
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public  ActionResult Get(Guid id)
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
        [NonAction]
        public ActionResult Post(TCreationDto creationDto)
        {

            TEntity item = _service.Create(_mapper.Map<TEntity>(creationDto));
            
            var itemDto = _mapper.Map<TDto>(item);
            
            return new CustomCreatedAtRouteResult(item.Id, itemDto);
        }
        
        
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public  ActionResult Put(Guid id,  TCreationDto updateDTO)
        {

            var item = _mapper.Map<TEntity>(updateDTO);
            item.Id = id;

            _service.Update(item);

            return NoContent();
        }
        
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
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
        [NonAction]
        public ActionResult Delete(Guid id)
        {

           var entity = _service.Read(id);
           
          _service.Delete(id);

          return Ok(entity);
        }
        
        
    }
}