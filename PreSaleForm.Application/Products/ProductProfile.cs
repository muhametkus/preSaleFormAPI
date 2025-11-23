using AutoMapper;
using PreSaleForm.Application.Products.Queries.Dto;
using PreSaleForm.Domain.Entities;

namespace PreSaleForm.Application.Products;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
    }
}
