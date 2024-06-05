using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/Categories")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        ResultDto result = new ResultDto();

        public CategoriesController(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public List<CategoryDto> GetList()
        {
            var Categories = _context.Categories.ToList();
            var CategoryDtos = _mapper.Map<List<CategoryDto>>(Categories);
            return CategoryDtos;
        }
        [HttpGet("{id}")]
        public CategoryDto Get(int id)
        {
            var Category = _context.Categories.FirstOrDefault(x => x.Id == id);
            var CategoryDto = _mapper.Map<CategoryDto>(Category);
            return CategoryDto;
        }
        [HttpGet("{id}/prodducts")]
        public async Task<ActionResult<CategoryWithProductsDto>> GetCategoriesByProduct(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound("No category found with the specified ID.");
            }
            if (category.Products == null)
            {
                return NotFound("No products found for the category.");
            }

            var categoryDto = _mapper.Map<CategoryWithProductsDto>(category);

            return Ok(categoryDto);
        }
        [HttpPost]
        public CategoryAddDto CategoryAddDto(CategoryAddDto CategoryAddDto)
        {
            var Category = _mapper.Map<Category>(CategoryAddDto);
            Category.Created = DateTime.Now;
            Category.Updated = DateTime.Now;
            _context.Categories.Add(Category);
            _context.SaveChanges();
            return CategoryAddDto;
        }
        [HttpPut]
        public CategoryDto CategoryUpdateDto(CategoryDto CategoryDto)
        {
            var Category = _mapper.Map<Category>(CategoryDto);
            Category.Updated = DateTime.Now;
            _context.Categories.Update(Category);
            _context.SaveChanges();
            return CategoryDto;
        }
        [HttpDelete("{id}")]
        public ResultDto Delete(int id)
        {
            var Category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (Category != null)
            {
                _context.Categories.Remove(Category);
                _context.SaveChanges();
                result.Status = true;
                result.Message = "Category deleted successfully";
            }
            else
            {
                result.Status = false;
                result.Message = "Category not found";
            }
            return result;
        }


    }
}
