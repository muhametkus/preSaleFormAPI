namespace PreSaleForm.Application.PreSaleForms.Commands.GeneratePdf;

public class GeneratePdfResponse
{
    public bool Success { get; set; }
    public string PdfUrl { get; set; } = default!;
    public string Message { get; set; } = default!;
}

