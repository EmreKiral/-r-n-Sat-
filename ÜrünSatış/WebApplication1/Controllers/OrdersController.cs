using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/Orders")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        ResultDto result = new ResultDto();

        public OrdersController(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public List<OrderDetailDto> GetList()
        {
            var orderDetails = (from order in _context.Orders
                                join product in _context.Products on order.ProductId equals product.Id
                                select new OrderDetailDto
                                {
                                    productId = product.Id,
                                    ProductName = product.Name,
                                    AppUserId = order.AppUserId,
                                    Quantity = order.Quantity,
                                    Price = order.Price,
                                    Total = order.Total,
                                    UserName = order.AppUser.FullName 
                                }).ToList();

            return orderDetails;
        }
        [HttpGet("{id}")]
        public OrderDetailDto Get(int id)
        {
            var orderDetails = (from order in _context.Orders
                                join product in _context.Products on order.ProductId equals product.Id
                                where order.Id == id
                                select new OrderDetailDto
                                {
                                    productId = product.Id,
                                    ProductName = product.Name,
                                    AppUserId = order.AppUserId,
                                    Quantity = order.Quantity,
                                    Price = order.Price,
                                    Total = order.Total,
                                    UserName = order.AppUser.FullName 
                                }).FirstOrDefault();

            return orderDetails;
        }
        [HttpPost]
        public OrderAddDto OrderAddDto(OrderAddDto OrderAddDto)
        {
            var Order = _mapper.Map<Order>(OrderAddDto);
            Order.Created = DateTime.Now;
            Order.Updated = DateTime.Now;
            _context.Orders.Add(Order);
            _context.SaveChanges();
            return OrderAddDto;
        }
        [HttpDelete("{id}")]
        public ResultDto Delete(int id)
        {
            var Order = _context.Orders.FirstOrDefault(x => x.Id == id);
            if (Order != null)
            {
                _context.Orders.Remove(Order);
                _context.SaveChanges();
                result.Status = true;
                result.Message = "Order deleted successfully.";
            }
            else
            {
                result.Status = false;
                result.Message = "Order not found.";
            }
            return result;
        }

    }
}
