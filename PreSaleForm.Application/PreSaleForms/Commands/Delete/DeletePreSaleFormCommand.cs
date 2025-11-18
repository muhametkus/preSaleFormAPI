using MediatR;

namespace PreSaleForm.Application.PreSaleForms.Commands.Delete;

public class DeletePreSaleFormCommand : IRequest<DeletePreSaleFormResponse>
{
    public Guid Id { get; set; }
}

