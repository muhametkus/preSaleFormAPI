using MediatR;
using PreSaleForm.Application.Categories.Queries.Dto;

namespace PreSaleForm.Application.Categories.Queries.GetAllCategories;

public record GetAllCategoriesQuery : IRequest<List<CategoryDto>>;
