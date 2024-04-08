using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class OrderController : BaseApiController
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IUserRepository userRepository,  IMapper mapper)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create(OrderDto orderDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUserName());

            orderDto.User = user;

            var order = _mapper.Map<Order>(orderDto);

            await _orderRepository.CreateOrderAsync(order);
            if(await _orderRepository.SaveAllAsync()) return Ok(order);

            return BadRequest("Failed to create order");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _orderRepository.GetOrdersAsync();
            return Ok(orders);
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
                return NotFound("Bill not found");

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
