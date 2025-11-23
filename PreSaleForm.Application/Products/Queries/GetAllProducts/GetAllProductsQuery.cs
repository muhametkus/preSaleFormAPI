using MediatR;
using PreSaleForm.Application.Products.Queries.Dto;

namespace PreSaleForm.Application.Products.Queries.GetAllProducts;

public record GetAllProductsQuery : IRequest<List<ProductDto>>;
