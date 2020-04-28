using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    public class OrderTypeNewController : OttoboBaseController<OrderType, OrderTypeDto, OrderTypeCreationDto>
    {
        public OrderTypeNewController(ILogger<OrderTypeController> logger, 
            ApplicationDbContext dbContext, 
            IMapper mapper, 
            IUnitOfWork unitOfWork) : base(logger, dbContext, mapper, unitOfWork)
        {
            
        }

    }
}
