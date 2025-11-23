using AutoMapper;
using PreSaleForm.Application.Categories.Queries.Dto;
using PreSaleForm.Domain.Entities;

namespace PreSaleForm.Application.Categories;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>();
    }
}
