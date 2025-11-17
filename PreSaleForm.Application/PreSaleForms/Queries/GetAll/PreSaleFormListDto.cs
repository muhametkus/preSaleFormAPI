namespace PreSaleForm.Application.PreSaleForms.Queries.GetAll;

public class PreSaleFormListDto
{
    public Guid Id { get; set; }
    public string CustomerFullName { get; set; } = default!;
    public string CustomerPhone { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public string? PdfUrl { get; set; }
}