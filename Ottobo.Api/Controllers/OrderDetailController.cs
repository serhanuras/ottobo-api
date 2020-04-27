using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ottobo.Api.Dtos;
using Ottobo.Data.Provider.PostgreSql;
using Ottobo.Entities;

namespace Ottobo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailController : ControllerBase
    {

        private readonly ILogger _logger;
        private readonly ApplicationDbContext _dbContext;

        private readonly IMapper _mapper;

        public OrderDetailController(ILogger<OrderDetailController> logger, ApplicationDbContext dbContext, IMapper mapper)
        {

            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Getting all order details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("list")]
        [HttpGet("/allorderdetail")]
        public async Task<ActionResult<List<OrderDetailDto>>> Get()
        {
            var orderDetail = await _dbContext.OrderDetails.AsNoTracking().ToListAsync();

            var orderDetailDTO = _mapper.Map<List<OrderDetailDto>>(orderDetail);

            return orderDetailDTO;
        }


        /// <summary>
        /// Get Order Detail By Id
        /// </summary>
        /// <param name="id">Id of the order detail to get</param>
        /// <returns></returns>
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(OrderDetailDto), 200)]
        [HttpGet("{Id:int}", Name = "GetOrderDetail")]
        public async Task<ActionResult<OrderDetailDto>> Get(int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var orderDetail = await _dbContext.OrderDetails.FirstOrDefaultAsync(x => x.Id == id);

            var orderDetailDTO = _mapper.Map<OrderDetailDto>(orderDetail);

            if (orderDetail == null)
            {
                _logger.LogWarning($"There is not record according to this id. Id is {id}");
                return NotFound();
            }

            return Ok(orderDetailDTO);
        }


        /// <summary>
        /// Adding Order Type
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] OrderDetailCreationDto orderDetailCreationDto)
        {

            var orderDetail = _mapper.Map<OrderDetail>(orderDetailCreationDto);

            orderDetail.Stock = await _dbContext.Stocks.FirstOrDefaultAsync(x => x.Id == orderDetailCreationDto.StockId);

            orderDetail.OrderType = await _dbContext.OrderTypes.FirstOrDefaultAsync(x => x.Id == orderDetailCreationDto.OrderTypeId);

            _dbContext.Add(orderDetail);

            await _dbContext.SaveChangesAsync();

            var orderDetailDTO = _mapper.Map<OrderDetailDto>(orderDetail);

            return new CreatedAtRouteResult("GetStockType", new { Id = orderDetailDTO.Id }, orderDetailDTO);
        }

        /// <summary>
        /// Updating a Order Detail
        /// </summary>
        /// <param name="id">Id of the stock type to update</param>
        /// <param name="orderDetailCreationDto"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] OrderDetailCreationDto orderDetailCreationDto)
        {
            var orderDetail = _mapper.Map<OrderDetail>(orderDetailCreationDto);
            orderDetail.Id = id;

            orderDetail.Stock = await _dbContext.Stocks.FirstOrDefaultAsync(x => x.Id == orderDetailCreationDto.StockId);

            orderDetail.OrderType = await _dbContext.OrderTypes.FirstOrDefaultAsync(x => x.Id == orderDetailCreationDto.OrderTypeId);

            _dbContext.Entry(orderDetail).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete a order detail
        /// </summary>
        /// <param name="id">Id of the order detail to delete</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await _dbContext.OrderDetails.AnyAsync(x => x.Id == id);

            if (!exists)
                return NotFound();

            _dbContext.Remove(new OrderDetail() { Id = id });

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
