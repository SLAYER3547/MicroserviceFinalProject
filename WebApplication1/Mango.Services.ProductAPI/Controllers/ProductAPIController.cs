﻿using AutoMapper;
using Azure;
using Mango.Services.ProductAPI.Data;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    [Authorize]
    public class ProductAPIController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private ResponseDto _response;
        private IMapper _mapper;

        public ProductAPIController(AppDbContext appDbContext,IMapper mapper)
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
                IEnumerable<Product> objList = _appDbContext.Products.ToList();
                _response.Result = _mapper.Map<IEnumerable<ProductDto>>(objList);
                
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
                Product obj= _appDbContext.Products.First(u=>u.ProductId==id);
                _response.Result= _mapper.Map<ProductDto>(obj);
                
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
        public ResponseDto Post([FromBody] ProductDto productDto)
        {
            try
            {
                Product obj = _mapper.Map<Product>(productDto);
                _appDbContext.Products.Add(obj);
                _appDbContext.SaveChanges();
                _response.Result = _mapper.Map<ProductDto>(obj);

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
        public ResponseDto put([FromBody] ProductDto productDto)
        {
            try
            {
                Product obj = _mapper.Map<Product>(productDto);
                _appDbContext.Products.Update(obj);
                _appDbContext.SaveChanges();
                _response.Result = _mapper.Map<ProductDto>(obj);

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
                Product obj = _appDbContext.Products.First(u=>u.ProductId==id);
                _appDbContext.Products.Remove(obj);
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
