using PreSaleForm.Application.PreSaleForms.Commands.Create;

namespace PreSaleForm.Application.PreSaleForms.Commands.Update;

public class UpdatePreSaleFormRequest
{
    public Guid Id { get; set; }
    public string CustomerFullName { get; set; } = default!;
    public string CustomerPhone { get; set; } = default!;
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal RemainingAmount { get; set; }

    // İndirim Bilgileri (opsiyonel)
    public decimal? DiscountAmount { get; set; }
    public decimal? DiscountedAmount { get; set; }

    public bool TermsAccepted { get; set; }

    // Hizmet Bilgileri (opsiyonel)
    public bool? MontajDahilMi { get; set; }
    public bool? NakliyeDahilMi { get; set; }
    public bool? FabrikaTeslimMi { get; set; }

    // Aksesuar Bilgileri (opsiyonel)
    public bool? AksesuarDahilMi { get; set; }
    public decimal? AksesuarUcreti { get; set; }
    public decimal? NakliyeUcreti { get; set; }
    public string? SecilenAksesuar { get; set; }

    // Söküm Bilgileri (opsiyonel)
    public int? OldDoorCount { get; set; } // Sökülecek eski kapı adedi
    public decimal? DismantlingUnitPrice { get; set; } // Söküm ücreti (birim fiyat)
    public decimal? TotalDismantlingPrice { get; set; } // Toplam söküm ücreti

    public List<PreSaleFormProductDto> Products { get; set; } = new();
}

