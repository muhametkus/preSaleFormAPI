namespace PreSaleForm.Domain.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    
    // Navigation property
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
