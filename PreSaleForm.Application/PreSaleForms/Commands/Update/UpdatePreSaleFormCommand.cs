using MediatR;

namespace PreSaleForm.Application.PreSaleForms.Commands.Update;

public class UpdatePreSaleFormCommand : IRequest<UpdatePreSaleFormResponse>
{
    public UpdatePreSaleFormRequest Request { get; set; } = default!;
}

