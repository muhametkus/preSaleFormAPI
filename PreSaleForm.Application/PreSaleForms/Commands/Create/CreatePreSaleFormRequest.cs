public class CreatePreSaleFormRequest
{
    public string CustomerFullName { get; set; } = default!;
    public string CustomerPhone { get; set; } = default!;
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal RemainingAmount { get; set; }
    public bool TermsAccepted { get; set; }

    public List<PreSaleFormProductDto> Products { get; set; } = new();
}