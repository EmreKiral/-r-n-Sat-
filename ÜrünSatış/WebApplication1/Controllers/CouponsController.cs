using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/Coupons")]
    [ApiController]
    [Authorize]
    public class CouponsController : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        ResultDto result = new ResultDto();

        public CouponsController(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public List<CouponDto> GetList()
        {
            var Coupons = _context.Coupons.ToList();
            var CouponDtos = _mapper.Map<List<CouponDto>>(Coupons);
            return CouponDtos;
        }
        [HttpGet("{id}")]
        public CouponDto Get(int id)
        {
            var Coupon = _context.Coupons.FirstOrDefault(x => x.Id == id);
            var CouponDto = _mapper.Map<CouponDto>(Coupon);
            return CouponDto;
        }
        [HttpPost]
        public async Task<CouponAddDto> CouponAddDto(CouponAddDto CouponAddDto)
        {
            var Coupon = _mapper.Map<Coupon>(CouponAddDto);
            Coupon.Created = DateTime.Now;
            Coupon.Updated = DateTime.Now;
            _context.Coupons.Add(Coupon);
            await _context.SaveChangesAsync();
            return CouponAddDto;
        }
        [HttpPut]
        public CouponDto CouponUpdateDto(CouponDto CouponDto)
        {
            var Coupon = _mapper.Map<Coupon>(CouponDto);
            Coupon.Updated = DateTime.Now;
            _context.Coupons.Update(Coupon);
            _context.SaveChanges();
            return CouponDto;
        }
        [HttpDelete("{id}")]
        public ResultDto Delete(int id)
        {
            var Coupon = _context.Coupons.FirstOrDefault(x => x.Id == id);
            if (Coupon != null)
            {
                _context.Coupons.Remove(Coupon);
                _context.SaveChanges();
                result.Status = true;
                result.Message = "Coupon deleted successfully";
            }
            else
            {
                result.Status = false;
                result.Message = "Coupon not found";
            }
            return result;
        }
    }
}
