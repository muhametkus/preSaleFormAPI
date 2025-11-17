using MediatR;

namespace PreSaleForm.Application.PreSaleForms.Commands.Create;

public class CreatePreSaleFormCommand : IRequest<CreatePreSaleFormResponse>
{
    public CreatePreSaleFormRequest Request { get; set; } = default!;
}