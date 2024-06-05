using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebApplication1.Dtos;
using WebApplication1.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    [Route("api/Products")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        ResultDto result = new ResultDto();
        public ProductsController(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetList")]
        public List<ProductCategoriesProductSpecsDto> GetList()
        {
            var products = _context.Products
          .Include(p => p.Category)
          .Include(p => p.ProductSpecs)
          .ToList();
            var productDtos = _mapper.Map<List<ProductCategoriesProductSpecsDto>>(products);
            return productDtos;
        }
        [HttpGet("{id}/Products")]

        public async Task<ActionResult<IEnumerable<EmptyDto>>> GetProductsByCategory(int id)
        {
            var products = await _context.Products
                 .Include(p => p.Category)
                 .Include(m => m.ProductSpecs)
                 .Where(p => p.Id == id)
                 .ToListAsync();

            if (products == null || products.Count == 0)
            {
                return NotFound("No products found for this category.");
            }

            var productDTOs = _mapper.Map<List<ProductCategoriesProductSpecsDto>>(products);

            return Ok(productDTOs);
        }

        [HttpGet("{id}")]
        public ProductDto Get(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }
        [HttpPost]
        public ProductAddDto ProductAddDto(ProductAddDto productAddDto)
        {
            var product = _mapper.Map<Product>(productAddDto);
            product.Created = DateTime.Now;
            product.Updated = DateTime.Now;
            _context.Products.Add(product);
            _context.SaveChanges();
            return productAddDto;
        }
        [HttpPut]
        public ProductDto ProductUpdateDto(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            product.Updated = DateTime.Now;
            _context.Products.Update(product);
            _context.SaveChanges();
            return productDto;
        }
        [HttpDelete("{id}")]
        public ResultDto Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                result.Status = true;
                result.Message = "Product deleted successfully";
            }
            else
            {
                result.Status = false;
                result.Message = "Product not found";
            }
            return result;
        }
    }
}
