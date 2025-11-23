using PreSaleForm.Domain.Entities;

namespace PreSaleForm.Application.Common.Interfaces;

public interface IPdfService
{
    Task<string> GeneratePreSaleFormPdfAsync(PreSaleFormEntity form, CancellationToken cancellationToken);
    string GetPdfFilePath(Guid formId);
}