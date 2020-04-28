using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ottobo.Api.Dtos;
using Ottobo.Data.Provider.IRepository;
using Ottobo.Data.Provider.PostgreSql;
using Ottobo.Entities;

namespace Ottobo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailController :OttoboBaseController<OrderDetail, OrderDetailDto, OrderDetailCreationDto, OrderDetailFilterDto, OrderDetailPatchDto>
    {

        private readonly ILogger _logger;

        private readonly IMapper _mapper;

        public OrderDetailController(ILogger<OrderTypeController> logger, 
            ApplicationDbContext dbContext, 
            IMapper mapper, 
            IUnitOfWork unitOfWork) : base(logger, mapper, unitOfWork, "Stock>StockType,OrderType")
        {
            
        }
        
        public override async Task<ActionResult<List<OrderDetailDto>>> Filter(PaginationDto paginationDto, OrderDetailFilterDto filterDto)
        {
            return NotFound();
        }

        
    }
}
