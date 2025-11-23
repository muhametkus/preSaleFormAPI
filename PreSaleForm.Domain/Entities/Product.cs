namespace PreSaleForm.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? ImageUrl { get; set; }
    public decimal PriceWithAssembly { get; set; }
    public decimal PriceWithoutAssembly { get; set; }
    
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = default!;
}
