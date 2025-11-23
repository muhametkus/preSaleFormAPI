using MediatR;

namespace PreSaleForm.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(string Name, string? ImageUrl, decimal PriceWithAssembly, decimal PriceWithoutAssembly, Guid CategoryId) : IRequest<Guid>;
