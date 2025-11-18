using MediatR;
using Microsoft.EntityFrameworkCore;
using PreSaleForm.Application.Common.Interfaces;

namespace PreSaleForm.Application.PreSaleForms.Commands.GeneratePdf;

public class GeneratePdfCommandHandler
    : IRequestHandler<GeneratePdfCommand, GeneratePdfResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IPdfService _pdfService;

    public GeneratePdfCommandHandler(
        IApplicationDbContext context,
        IPdfService pdfService)
    {
        _context = context;
        _pdfService = pdfService;
    }

    public async Task<GeneratePdfResponse> Handle(
        GeneratePdfCommand command,
        CancellationToken cancellationToken)
    {
        var form = await _context.PreSaleForms
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == command.FormId, cancellationToken);

        if (form == null)
        {
            throw new KeyNotFoundException($"PreSaleForm with ID {command.FormId} not found.");
        }

        // PDF oluştur
        var pdfUrl = await _pdfService.GeneratePreSaleFormPdfAsync(form, cancellationToken);
        
        // PDF yolunu veritabanına kaydet
        form.PdfFilePath = pdfUrl;
        await _context.SaveChangesAsync(cancellationToken);

        return new GeneratePdfResponse
        {
            Success = true,
            PdfUrl = pdfUrl,
            Message = "PDF başarıyla oluşturuldu."
        };
    }
}

