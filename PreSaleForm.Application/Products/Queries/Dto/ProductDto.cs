using PreSaleForm.Domain.Entities;

namespace PreSaleForm.Application.Products.Queries.Dto;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? ImageUrl { get; set; }
    public decimal PriceWithAssembly { get; set; }
    public decimal PriceWithoutAssembly { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = default!;
}
