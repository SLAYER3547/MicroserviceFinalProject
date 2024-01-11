using AutoMapper;
using Azure;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    //[Authorize]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private ResponseDto _response;
        private IMapper _mapper;

        public CouponAPIController(AppDbContext appDbContext,IMapper mapper)
        {
            _appDbContext = appDbContext;
            _response = new ResponseDto();
            _mapper = mapper;
        }
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _appDbContext.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);
                
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Messasge=ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Coupon obj= _appDbContext.Coupons.First(u=>u.CouponId==id);
                _response.Result= _mapper.Map<CouponDto>(obj);
                
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Messasge = ex.Message;
            }
            return _response;
        }


        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                Coupon obj = _appDbContext.Coupons.First(u => u.CouponCode.ToLower() == code.ToLower());
                _response.Result = _mapper.Map<CouponDto>(obj);

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Messasge = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles ="ADMIN")]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _appDbContext.Coupons.Add(obj);
                _appDbContext.SaveChanges();
                _response.Result = _mapper.Map<CouponDto>(obj);

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Messasge = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _appDbContext.Coupons.Update(obj);
                _appDbContext.SaveChanges();
                _response.Result = _mapper.Map<CouponDto>(obj);

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Messasge = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Coupon obj = _appDbContext.Coupons.First(u=>u.CouponId==id);
                _appDbContext.Coupons.Remove(obj);
                _appDbContext.SaveChanges();

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Messasge = ex.Message;
            }
            return _response;
        }
    }
}
