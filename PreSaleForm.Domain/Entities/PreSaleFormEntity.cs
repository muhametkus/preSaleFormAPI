namespace PreSaleForm.Domain.Entities;

public class PreSaleFormEntity
{
    public Guid Id { get; set; }

    // Müşteri Bilgileri
    public string CustomerFullName { get; set; } = default!;
    public string CustomerPhone { get; set; } = default!;

    // Fiyat Bilgileri
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal RemainingAmount { get; set; }

    // Ek Not
    public string? Note { get; set; }

    // PDF Dosya Yolu
    public string? PdfFilePath { get; set; }

    // Oluşturulma Tarihi
    public DateTime CreatedAt { get; set; }

    // Ürünler (One → Many Relationship)
    public ICollection<PreSaleFormProduct> Products { get; set; } = new List<PreSaleFormProduct>();
}