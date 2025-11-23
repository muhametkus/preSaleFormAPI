using PreSaleForm.Application.PreSaleForms.Commands.Create;

namespace PreSaleForm.Application.PreSaleForms.Queries.GetById;

public class PreSaleFormDetailDto
{
    public Guid Id { get; set; }
    public string CustomerFullName { get; set; }
    public string CustomerPhone { get; set; }
    public string? Note { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal RemainingAmount { get; set; }
    public decimal? DiscountAmount { get; set; }
    public decimal? DiscountedAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? PdfUrl { get; set; }

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

    public List<PreSaleFormProductDto> Products { get; set; }
}