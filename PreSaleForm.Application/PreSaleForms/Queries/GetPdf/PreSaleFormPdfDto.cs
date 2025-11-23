namespace PreSaleForm.Application.PreSaleForms.Queries.GetPdf;

public class PreSaleFormPdfDto
{
    public byte[] FileContent { get; set; } = Array.Empty<byte>();
    public string FileName { get; set; } = default!;
    public string ContentType { get; set; } = "application/pdf";
}
