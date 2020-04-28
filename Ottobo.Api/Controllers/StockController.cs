using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.Extensions.Logging;
using Ottobo.Api.Dtos;
using Ottobo.Data.Provider.IRepository;
using Ottobo.Entities;

namespace Ottobo.Api.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockController :  OttoboBaseController<Stock, StockDto, StockCreationDto, StockFilterDto, StockPatchDto>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        
        public StockController(ILogger<StockController> logger,
            IMapper mapper, 
            IUnitOfWork unitOfWork) : base(logger, mapper, unitOfWork, "StockType")
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        
        public override async Task<ActionResult<List<StockDto>>> Filter(PaginationDto paginationDto, StockFilterDto stockFilterDto)
        {
            
            IQueryable<Stock> stocksQueryable =_unitOfWork.GetRepository<Stock>().Queryable();

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

            
            var stocks = _unitOfWork.GetRepository<Stock>().GetAll(stocksQueryable,"", paginationDto.Page, paginationDto.RecordsPerPage);

            return Ok(stocks);
        }

    }
}