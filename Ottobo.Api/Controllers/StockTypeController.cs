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
    public class StockTypeController : ControllerBase
    {

        private readonly ILogger _logger;
        private readonly ApplicationDbContext _dbContext;

        private readonly IMapper _mapper;

        public StockTypeController(ILogger<StockTypeController> logger, ApplicationDbContext dbContext, IMapper mapper)
        {

            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Getting all stock types.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("list")]
        [HttpGet("/allstocks")]
        [ResponseCache(Duration = 60)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<StockTypeDto>>> Get()
        {
            var stocks = await _dbContext.StockTypes.ToListAsync();

            var stockTypeDTOs = _mapper.Map<List<StockTypeDto>>(stocks);

            return stockTypeDTOs;
        }


        /// <summary>
        /// Get Stock Type By Id
        /// </summary>
        /// <param name="id">Id of the stock type to get</param>
        /// <returns></returns>
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(StockTypeDto), 200)]
        [HttpGet("{Id:int}", Name = "GetStockType")]
        public async Task<ActionResult<StockTypeDto>> Get(int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var stockType = await _dbContext.StockTypes.FirstOrDefaultAsync(x => x.Id == id);

            var stockTypeDTO = _mapper.Map<StockTypeDto>(stockType);

            if (stockType == null)
            {
                _logger.LogWarning($"There is not record according to this id. Id is {id}");
                return NotFound();
            }

            return Ok(stockTypeDTO);
        }


        /// <summary>
        /// Adding Stock Type
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post(StockTypeCreationDto stockTypeCreationDto)
        {

            var stockType = _mapper.Map<StockType>(stockTypeCreationDto);
            _dbContext.Add(stockType);

            await _dbContext.SaveChangesAsync();

            var stockTypeDTO = _mapper.Map<StockTypeDto>(stockType);

            return new CreatedAtRouteResult("GetStockType", new { Id = stockTypeDTO.Id }, stockTypeDTO);
        }

        /// <summary>
        /// Updating a Stock Type
        /// </summary>
        /// <param name="id">Id of the stock type to update</param>
        /// <param name="stockTypeCreationDto"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, StockTypeCreationDto stockTypeCreationDto)
        {
            var stockType = _mapper.Map<StockType>(stockTypeCreationDto);
            stockType.Id = id;

            _dbContext.Entry(stockType).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete a stock type
        /// </summary>
        /// <param name="id">Id of the stock to delete</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await _dbContext.StockTypes.AnyAsync(x => x.Id == id);

            if (!exists)
                return NotFound();

            _dbContext.Remove(new StockType() { Id = id });

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
