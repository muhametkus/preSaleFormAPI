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

    // İndirim Bilgileri
    public decimal? DiscountAmount { get; set; }
    public decimal? DiscountedAmount { get; set; }

    // Ek Not
    public string? Note { get; set; }

    // PDF Dosya Yolu
    public string? PdfFilePath { get; set; }

    // Hizmet Bilgileri
    public bool? MontajDahilMi { get; set; }
    public bool? NakliyeDahilMi { get; set; }
    public bool? FabrikaTeslimMi { get; set; }

    // Aksesuar Bilgileri
    public bool? AksesuarDahilMi { get; set; }
    public decimal? AksesuarUcreti { get; set; }
    public decimal? NakliyeUcreti { get; set; }
    public string? SecilenAksesuar { get; set; }

    // Söküm Bilgileri
    public int? OldDoorCount { get; set; } // Sökülecek eski kapı adedi
    public decimal? DismantlingUnitPrice { get; set; } // Söküm ücreti (birim fiyat)
    public decimal? TotalDismantlingPrice { get; set; } // Toplam söküm ücreti

    // Oluşturulma Tarihi
    public DateTime CreatedAt { get; set; }

    // Ürünler (One → Many Relationship)
    public ICollection<PreSaleFormProduct> Products { get; set; } = new List<PreSaleFormProduct>();
}