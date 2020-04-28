using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ottobo.Api.Dtos;
using Ottobo.Data.Provider.IRepository;
using Ottobo.Data.Provider.PostgreSql;
using Ottobo.Entities;

namespace Ottobo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockTypeController : OttoboBaseController<StockType, StockTypeDto, StockTypeCreationDto, StockTypeFilterDto, StockTypePatchDto>
    {
        public StockTypeController(ILogger<StockTypeController> logger,
            IMapper mapper, 
            IUnitOfWork unitOfWork) : base(logger, mapper, unitOfWork)
        {
            
        }
        
        public override async Task<ActionResult<List<StockTypeDto>>> Filter(PaginationDto paginationDto, StockTypeFilterDto filterDto)
        {
            return NotFound();
        }

    }
}
