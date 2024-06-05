using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/ProductSpecs")]
    [ApiController]
    //[Authorize]
    public class ProductSpecsController : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly ResultDto _result;
        private readonly IWebHostEnvironment _env;

        public ProductSpecsController(Context context, IMapper mapper, ResultDto result, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _result = result;
            _env = env;
        }

        [HttpGet]
        public List<ProductSpecs> GetList()
        {
            var ProductSpecs = _context.ProductSpecs.ToList();
            var ProductSpecsDtos = _mapper.Map<List<ProductSpecs>>(ProductSpecs);
            return ProductSpecsDtos;
        }

        [HttpGet("{id}")]
        public ProductSpecsDto Get(int id)
        {
            var ProductSpecs = _context.ProductSpecs.FirstOrDefault(x => x.Id == id);
            var ProductSpecsDto = _mapper.Map<ProductSpecsDto>(ProductSpecs);
            return ProductSpecsDto;
        }
        [HttpPost]
        public async Task<ProductSpecsAddDto> ProductAdd(ProductSpecsAddDto ProductSpecsAddDto)
        {
            var ProductSpecs = _mapper.Map<ProductSpecs>(ProductSpecsAddDto);

            ProductSpecs.Created = DateTime.Now;
            ProductSpecs.Updated = DateTime.Now;

            _context.ProductSpecs.Add(ProductSpecs);
            _context.SaveChanges();

            return ProductSpecsAddDto;
        }
        [HttpPut]
        public async Task<ProductSpecsDto> Put(ProductSpecsDto ProductSpecsDto)
        {
            var ProductSpecs = _mapper.Map<ProductSpecs>(ProductSpecsDto);
            ProductSpecs.Updated = DateTime.Now;

            _context.ProductSpecs.Update(ProductSpecs);
            _context.SaveChanges();

            return ProductSpecsDto;
        }

        [HttpDelete("{id}")]
        public ResultDto Delete(int id)
        {
            ResultDto result = new ResultDto();
            var ProductSpecs = _context.ProductSpecs.FirstOrDefault(x => x.Id == id);
            if (ProductSpecs != null)
            {
                _context.ProductSpecs.Remove(ProductSpecs);
                _context.SaveChanges();
               result.Status = true;
                result.Message = "ProductSpecs silme işlemi başarılı";
            }
            else
            {
                result.Status = false;
                result.Message = "ProductSpecs silme işlemi başarısız";
            }
            return result;
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file.");

            var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadDir, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Return the relative path to the file
            return Path.Combine("uploads", fileName);
        }

    }
}
