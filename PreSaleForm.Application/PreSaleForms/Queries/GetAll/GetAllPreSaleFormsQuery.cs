using MediatR;

namespace PreSaleForm.Application.PreSaleForms.Queries.GetAll;

public class GetAllPreSaleFormsQuery : IRequest<List<PreSaleFormListDto>>
{
}