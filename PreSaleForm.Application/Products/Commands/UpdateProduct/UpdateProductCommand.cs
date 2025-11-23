using MediatR;

namespace PreSaleForm.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, string? ImageUrl, decimal PriceWithAssembly, decimal PriceWithoutAssembly, Guid CategoryId) : IRequest;
