using MediatR;

namespace PreSaleForm.Application.PreSaleForms.Queries.GetById;

public class GetPreSaleFormByIdQuery : IRequest<PreSaleFormDetailDto>
{
    public Guid Id { get; set; }
}