using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class OrderController : BaseApiController
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);

            await _orderRepository.CreateOrderAsync(order);
            await _orderRepository.SaveAllAsync();

            return Ok(orderDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _orderRepository.GetOrdersAsync();
            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return Ok(orderDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound("Order not found");

            var orderDto = _mapper.Map<OrderDto>(order);
            return Ok(orderDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrderDto orderDto)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            _mapper.Map(orderDto, order);

            if (await _orderRepository.SaveAllAsync())
                return NoContent();

            return BadRequest("Failed to update order");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound("Order not found");

            _orderRepository.DeleteOrder(order);
            if (await _orderRepository.SaveAllAsync())
                return NoContent();

            return BadRequest("Failed to delete order");
        }
    }
}
