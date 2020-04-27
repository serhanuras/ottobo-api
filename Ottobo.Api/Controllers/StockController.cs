using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.Extensions.Logging;
using Ottobo.Data.Provider.PostgreSql;
using Ottobo.Api.Dtos;
using Ottobo.Extensions;
using Ottobo.Entities;

namespace Ottobo.Api.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<StockController> _logger;


        public StockController(ApplicationDbContext context,
            IMapper mapper,
            ILogger<StockController> logger)
        {
            this._context = context;
            this._mapper = mapper;
            this._logger = logger;
        }



        /// <summary>
        /// Getting paged stocks.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("list")]
        [HttpGet("/allstock")]
        public async Task<ActionResult<List<StockDto>>> Get([FromQuery] PaginationDto paginationDto)
        {
            var queryable = _context.Stocks.AsQueryable().Include(x => x.StockType);
           

            await HttpContext.InsertPaginationParametersInResponse(queryable, paginationDto.RecordsPerPage);
            var stocks = await queryable.Paginate(paginationDto.Page, paginationDto.RecordsPerPage).ToListAsync();
            return _mapper.Map<List<StockDto>>(stocks);
        }

        /// <summary>
        /// Getting paged filtered stocks.
        /// </summary>
        /// <returns></returns>
        [HttpGet("filter")]
        public async Task<ActionResult<List<StockDto>>> Filter(StockFilterDto stockFilterDto)
        {
            var stocksQueryable = _context.Stocks.AsQueryable();

            if (!string.IsNullOrWhiteSpace(stockFilterDto.SkuCode))
            {
                stocksQueryable = stocksQueryable.Where(x => x.SkuCode.Contains(stockFilterDto.SkuCode));
            }

            if (!string.IsNullOrWhiteSpace(stockFilterDto.Barcode))
            {
                stocksQueryable = stocksQueryable.Where(x => x.Barcode.Contains(stockFilterDto.Barcode));
            }

            if (stockFilterDto.StockTypeId != 0)
            {
                stocksQueryable = stocksQueryable
                    .Where(x => x.StockType.Id == stockFilterDto.StockTypeId);
            }

            if (!string.IsNullOrWhiteSpace(stockFilterDto.OrderingField))
            {
                try
                {
                    stocksQueryable = stocksQueryable
                        .OrderBy($"{stockFilterDto.OrderingField} {(stockFilterDto.AscendingOrder ? "ascending" : "descending")}");
                }
                catch
                {
                    // log this
                    _logger.LogWarning("Could not order by field: " + stockFilterDto.OrderingField);
                }
            }

            await HttpContext.InsertPaginationParametersInResponse(stocksQueryable,
                stockFilterDto.RecordsPerPage);

            var stocks = await stocksQueryable.Paginate(stockFilterDto.Pagination.Page, stockFilterDto.Pagination.RecordsPerPage).ToListAsync();

            return _mapper.Map<List<StockDto>>(stocks);
        }

        /// <summary>
        /// Get Stock Type By Id
        /// </summary>
        /// <param name="id">Id of the stock to get</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "getStock")]
        public async Task<ActionResult<StockDto>> Get(int id)
        {
            var stock = await _context.Stocks
                .Include(x => x.StockType)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (stock == null)
            {
                return NotFound();
            }

            return _mapper.Map<StockDto>(stock);
        }


        /// <summary>
        /// Adding Stock
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post(StockCreationDto stockCreationDto)
        {
            var stock = _mapper.Map<Stock>(stockCreationDto);

            stock.StockType = _context.StockTypes.FirstOrDefault(x=>x.Id == stockCreationDto.StockTypeId);
          
            _context.Add(stock);
            await _context.SaveChangesAsync();
            var stockDTO = _mapper.Map<StockDto>(stock);
            return new CreatedAtRouteResult("getStock", new { id = stock.Id }, stockDTO);
        }


        /// <summary>
        /// Updating a Stock
        /// </summary>
        /// <param name="id">Id of the stock to update</param>
        /// <param name="stockCreationDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, StockCreationDto stockCreationDto)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stock == null)
            {
                return NotFound();
            }

            stock = _mapper.Map(stockCreationDto, stock);

            stock.StockType = _context.StockTypes.FirstOrDefault(x=>x.Id == stockCreationDto.StockTypeId);

            await _context.SaveChangesAsync();
            return NoContent();
        }


        /// <summary>
        /// Partial Updating a Stock
        /// </summary>
        /// <param name="id">Id of the stock to update</param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<StockPatchDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var stock = await _context.Stocks.Include(x=>x.StockType).FirstOrDefaultAsync(x => x.Id == id);

            if (stock == null)
            {
                return NotFound();
            }

            var stockPatchDTO = _mapper.Map<StockPatchDto>(stock);

            patchDocument.ApplyTo(stockPatchDTO, ModelState);

            var isValid = TryValidateModel(stockPatchDTO);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(stockPatchDTO, stock);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete a stock
        /// </summary>
        /// <param name="id">Id of the stock to delete</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await _context.Stocks.AnyAsync(x => x.Id == id);

            if (!exists)
            {
                return NotFound();
            }

            _context.Remove(new Stock() { Id = id });
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}