using MediatR;
using PreSaleForm.Application.Common.Interfaces;

namespace PreSaleForm.Application.PreSaleForms.Queries.GetPdf;

public class GetPreSaleFormPdfQueryHandler : IRequestHandler<GetPreSaleFormPdfQuery, PreSaleFormPdfDto?>
{
    private readonly IPdfService _pdfService;

    public GetPreSaleFormPdfQueryHandler(IPdfService pdfService)
    {
        _pdfService = pdfService;
    }

    public async Task<PreSaleFormPdfDto?> Handle(GetPreSaleFormPdfQuery request, CancellationToken cancellationToken)
    {
        var filePath = _pdfService.GetPdfFilePath(request.Id);

        if (!File.Exists(filePath))
        {
            return null;
        }

        var fileBytes = await File.ReadAllBytesAsync(filePath, cancellationToken);

        return new PreSaleFormPdfDto
        {
            FileContent = fileBytes,
            FileName = $"PreSale_{request.Id}.pdf",
            ContentType = "application/pdf"
        };
    }
}
