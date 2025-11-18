using MediatR;

namespace PreSaleForm.Application.PreSaleForms.Commands.GeneratePdf;

public class GeneratePdfCommand : IRequest<GeneratePdfResponse>
{
    public Guid FormId { get; set; }
}

