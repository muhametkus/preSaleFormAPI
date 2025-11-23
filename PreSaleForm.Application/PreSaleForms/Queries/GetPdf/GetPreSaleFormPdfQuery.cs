using MediatR;

namespace PreSaleForm.Application.PreSaleForms.Queries.GetPdf;

public class GetPreSaleFormPdfQuery : IRequest<PreSaleFormPdfDto?>
{
    public Guid Id { get; set; }
}
