using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ottobo.Api.Dtos;
using Ottobo.Data.Provider.IRepository;
using Ottobo.Entities;

namespace Ottobo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderTypeController  : OttoboBaseController<OrderType, OrderTypeDto, OrderTypeCreationDto,OrderTypeFilterDto, OrderTypePatchDto>
    {

        public OrderTypeController(ILogger<OrderTypeController> logger,
            IMapper mapper, 
            IUnitOfWork unitOfWork) : base(logger, mapper, unitOfWork)
        {
            
        }

        public override async Task<ActionResult<List<OrderTypeDto>>> Filter(PaginationDto paginationDto, OrderTypeFilterDto filterDto)
        {
            return NotFound();
        }
    }
}
