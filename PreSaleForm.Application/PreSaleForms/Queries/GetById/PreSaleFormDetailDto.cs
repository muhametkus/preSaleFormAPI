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

    public List<PreSaleFormProductDto> Products { get; set; }
}