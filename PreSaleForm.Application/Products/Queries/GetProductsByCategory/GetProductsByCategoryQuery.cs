using MediatR;
using PreSaleForm.Application.Products.Queries.Dto;

namespace PreSaleForm.Application.Products.Queries.GetProductsByCategory;

public record GetProductsByCategoryQuery(Guid CategoryId) : IRequest<List<ProductDto>>;
