using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ottobo.Api.Dtos;
using Ottobo.Data.Provider.PostgreSql;
using Ottobo.Entities;

namespace Ottobo.Api.Contrßollers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderTypeController : ControllerBase
    {

        private readonly ILogger _logger;
        private readonly ApplicationDbContext _dbContext;

        private readonly IMapper _mapper;

        public OrderTypeController(ILogger<OrderTypeController> logger, ApplicationDbContext dbContext, IMapper mapper)
        {

            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Getting all order types.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("list")]
        [HttpGet("/allordertypes")]
        //[ResponseCache(Duration = 60)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<OrderTypeDto>>> Get()
        {
            var orderTypes = await _dbContext.OrderTypes.AsNoTracking().ToListAsync();

            var orderTypeDTOs = _mapper.Map<List<OrderTypeDto>>(orderTypes);

            return orderTypeDTOs;
        }


        /// <summary>
        /// Get Order Type By Id
        /// </summary>
        /// <param name="id">Id of the order type to get</param>
        /// <returns></returns>
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(OrderTypeDto), 200)]
        [HttpGet("{Id:int}", Name = "GetOrderType")]
        public async Task<ActionResult<OrderTypeDto>> Get(int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var orderType = await _dbContext.OrderTypes.FirstOrDefaultAsync(x => x.Id == id);

            var orderTypeDTO = _mapper.Map<OrderTypeDto>(orderType);

            if (orderType == null)
            {
                _logger.LogWarning($"There is not record according to this id. Id is {id}");
                return NotFound();
            }

            return Ok(orderTypeDTO);
        }


        /// <summary>
        /// Adding Order Type
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post(OrderTypeCreationDto orderTypeCreationDto)
        {

            var orderType = _mapper.Map<OrderType>(orderTypeCreationDto);
            _dbContext.Add(orderType);

            await _dbContext.SaveChangesAsync();

            var OrderTypeDTO = _mapper.Map<OrderTypeDto>(orderType);

            return new CreatedAtRouteResult("GetOrderType", new { Id = OrderTypeDTO.Id }, OrderTypeDTO);
        }

        /// <summary>
        /// Updating a Order Type
        /// </summary>
        /// <param name="id">Id of the order type to update</param>
        /// <param name="orderTypeCreationDto"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id,  OrderTypeCreationDto orderTypeCreationDto)
        {
            var orderType = _mapper.Map<OrderType>(orderTypeCreationDto);
            orderType.Id = id;

            _dbContext.Entry(orderType).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete a order type
        /// </summary>
        /// <param name="id">Id of the order to delete</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await _dbContext.OrderTypes.AnyAsync(x => x.Id == id);

            if (!exists)
                return NotFound();

            _dbContext.Remove(new OrderType() { Id = id });

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
