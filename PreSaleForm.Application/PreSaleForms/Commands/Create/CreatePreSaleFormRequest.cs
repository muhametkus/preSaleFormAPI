namespace PreSaleForm.Application.PreSaleForms.Commands.Create;

public class CreatePreSaleFormRequest
{
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

    public List<PreSaleFormProductDto> Products { get; set; } = new();
}