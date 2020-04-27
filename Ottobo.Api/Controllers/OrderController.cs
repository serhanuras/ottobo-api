using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderController> _logger;


        public OrderController(ApplicationDbContext context,
            IMapper mapper,
            ILogger<OrderController> logger)
        {
            this._context = context;
            this._mapper = mapper;
            this._logger = logger;
        }

        /// <summary>
        /// Getting paged orders.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("list")]
        [HttpGet("/allorder")]
        public async Task<ActionResult<List<OrderDto>>> Get([FromQuery] PaginationDto paginationDto)
        {
            var queryable = _context.Orders.Include(x => x.OrderDetails).AsQueryable();
            await HttpContext.InsertPaginationParametersInResponse(queryable, paginationDto.RecordsPerPage);
            var people = await queryable.Paginate(paginationDto.Page, paginationDto.RecordsPerPage).ToListAsync();
            return _mapper.Map<List<OrderDto>>(people);
        }

        /// <summary>
        /// Getting paged filtered orders.
        /// </summary>
        /// <returns></returns>
        [HttpGet("filter")]
        public async Task<ActionResult<List<OrderDto>>> Filter(OrderFilterDto orderFilterDto)
        {
            var ordersQueryable = _context.Orders
                .Include(x => x.OrderDetails)
                    .ThenInclude(x => x.OrderType)
                .Include(x => x.OrderDetails)
                    .ThenInclude(x => x.Stock).AsQueryable();

            if (!string.IsNullOrWhiteSpace(orderFilterDto.Name))
            {
                ordersQueryable = ordersQueryable.Where(x => x.Name.Contains(orderFilterDto.Name));
            }

            if (orderFilterDto.CityId != 0)
            {
                ordersQueryable = ordersQueryable
                    .Where(x => x.CityId == orderFilterDto.CityId);
            }

            if (orderFilterDto.TownId != 0)
            {
                ordersQueryable = ordersQueryable
                    .Where(x => x.TownId == orderFilterDto.TownId);
            }

            if (orderFilterDto.startDate.Year != 1 && orderFilterDto.endDate.Year != 1)
            {
                ordersQueryable = ordersQueryable
                    .Where(x => (x.Date >= orderFilterDto.startDate && x.Date <= orderFilterDto.endDate));
            }

            if (!string.IsNullOrWhiteSpace(orderFilterDto.OrderingField))
            {
                try
                {
                    ordersQueryable = ordersQueryable
                        .OrderBy($"{orderFilterDto.OrderingField} {(orderFilterDto.AscendingOrder ? "ascending" : "descending")}");
                }
                catch
                {
                    // log this
                    _logger.LogWarning("Could not order by field: " + orderFilterDto.OrderingField);
                }
            }

            await HttpContext.InsertPaginationParametersInResponse(ordersQueryable,
                orderFilterDto.RecordsPerPage);

            var orders = await ordersQueryable.Paginate(orderFilterDto.Pagination.Page,orderFilterDto.Pagination.RecordsPerPage).ToListAsync();

            return _mapper.Map<List<OrderDto>>(orders);
        }

        /// <summary>
        /// Get Order Type By Id
        /// </summary>
        /// <param name="id">Id of the order to get</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "getOrder")]
        public async Task<ActionResult<OrderDto>> Get(int id)
        {
            var order = await _context.Orders
                .Include(x => x.OrderDetails)
                    .ThenInclude(x => x.OrderType)
                 .Include(x => x.OrderDetails)
                     .ThenInclude(x => x.Stock)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return _mapper.Map<OrderDto>(order);
        }


        /// <summary>
        /// Adding Order
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post(OrderCreationDto orderCreationDto)
        {
            var order = _mapper.Map<Order>(orderCreationDto);

            order.OrderDetails = await AddOrderStocks(orderCreationDto.OrderDetails);

            _context.Add(order);
            await _context.SaveChangesAsync();
            var orderDTO = _mapper.Map<OrderDto>(order);
            return new CreatedAtRouteResult("getOrder", new { id = order.Id }, orderDTO);
        }

        private async Task<List<OrderDetail>> AddOrderStocks(List<OrderDetailCreationDto> orderDetailCreationDtOs)
        {

            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (var orderDetailCreationDTO in orderDetailCreationDtOs)
            {
                var orderDetail = _mapper.Map<OrderDetail>(orderDetailCreationDTO);
                orderDetail.Stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == orderDetailCreationDTO.StockId);
                orderDetail.OrderType = await _context.OrderTypes.FirstOrDefaultAsync(x => x.Id == orderDetailCreationDTO.OrderTypeId);
                orderDetails.Add(orderDetail);
            }
            return orderDetails;
        }

        /// <summary>
        /// Updating a Order
        /// </summary>
        /// <param name="id">Id of the order to update</param>
        /// <param name="orderCreationDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, OrderCreationDto orderCreationDto)
        {
            var order = await _context.Orders.AsNoTracking()
                            .Include(x => x.OrderDetails).ThenInclude(y => y.OrderType).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);


            if (order == null)
            {
                return NotFound();
            }
            else
            {
                _context.Entry(order).State = EntityState.Detached;
            }

            var orderDetails = await UpdateOrderStocks(order.OrderDetails, orderCreationDto.OrderDetails);
            var updatedOrder = _mapper.Map(orderCreationDto, order);


            updatedOrder.OrderDetails = orderDetails;
            //updatedOrder.Id = order.Id;
            _context.Entry(updatedOrder).State = EntityState.Modified;

            //await context.Database.ExecuteSqlInterpolatedAsync($"delete from order_details where order_id = {order.Id};");

            await _context.SaveChangesAsync();
            return NoContent();
        }


        private async Task<List<OrderDetail>> UpdateOrderStocks(List<OrderDetail> orderDetails, List<OrderDetailCreationDto> orderDetailCreationDtOs)
        {
            List<OrderDetail> tempOrderDetails = new List<OrderDetail>();
            foreach (var orderDetailCreationDTO in orderDetailCreationDtOs)
            {
                var orderDetail = orderDetails.FirstOrDefault(x => x.Id == orderDetailCreationDTO.OrderId);

                if (orderDetail != null)
                {
                    //orderDetail.Id = orderDetailCreationDTO.OrderId;
                    orderDetail.Stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == orderDetailCreationDTO.StockId);
                    orderDetail.OrderType = await _context.OrderTypes.FirstOrDefaultAsync(x => x.Id == orderDetailCreationDTO.OrderTypeId);

                    var index = orderDetails.FindIndex(x => x.Id == orderDetailCreationDTO.OrderId);
                    orderDetails[index] = orderDetail;

                    tempOrderDetails.Add((OrderDetail)orderDetail.CloneObject());
                }
            }
            return tempOrderDetails;
        }


        
        /// <summary>
        /// Delete a order
        /// </summary>
        /// <param name="id">Id of the order to delete</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await _context.Orders.AnyAsync(x => x.Id == id);

            if (!exists)
            {
                return NotFound();
            }

            _context.Remove(new Order() { Id = id });
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}