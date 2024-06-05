using AutoMapper;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Mapping
{
    public class Map : Profile
    {
        public Map()
        {
            CreateMap<AppUser, UserDto>().ReverseMap();// Dönüşüm işlemi yapıldı
            CreateMap<Product, ProductDto>().ReverseMap();// Dönüşüm işlemi yapıldı
            CreateMap<Product, ProductAddDto>().ReverseMap();// Dönüşüm işlemi yapıldı
            CreateMap<Category, CategoryDto>().ReverseMap();// Dönüşüm işlemi yapıldı
            CreateMap<Category, CategoryAddDto>().ReverseMap();// Dönüşüm işlemi yapıldı
            CreateMap<ProductSpecs, ProductSpecsDto>().ReverseMap();// Dönüşüm işlemi yapıldı
            CreateMap<ProductSpecs, ProductSpecsAddDto>().ReverseMap();// Dönüşüm işlemi yapıldı
            CreateMap<Coupon, CouponDto>().ReverseMap();// Dönüşüm işlemi yapıldı
            CreateMap<Coupon, CouponAddDto>().ReverseMap();// Dönüşüm işlemi yapıldı
            CreateMap<ProductDto, ProductCategoriesProductSpecsDto>().ReverseMap();
            CreateMap<CategoryDto, ProductCategoriesProductSpecsDto>().ReverseMap();
            CreateMap<ProductSpecsDto, ProductCategoriesProductSpecsDto>().ReverseMap();
            CreateMap<Product, ProductCategoriesProductSpecsDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
            .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.ProductSpecs, opt => opt.MapFrom(src => src.ProductSpecs))
            .ReverseMap();
            CreateMap<Order, OrderDetailDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.AppUser))
                .ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<ProductSpecs, ProductSpecsDto>().ReverseMap();
            CreateMap<Category, CategoryWithProductsDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<Order, OrderAddDto>();
            CreateMap<OrderAddDto, Order>()
    .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.AppUserId));
        }
    }
}
