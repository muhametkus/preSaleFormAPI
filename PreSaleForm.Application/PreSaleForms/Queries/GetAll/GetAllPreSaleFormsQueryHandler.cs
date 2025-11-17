using MediatR;
using Microsoft.EntityFrameworkCore;
using PreSaleForm.Application.Common.Interfaces;

namespace PreSaleForm.Application.PreSaleForms.Queries.GetAll;

public class GetAllPreSaleFormsQueryHandler :
    IRequestHandler<GetAllPreSaleFormsQuery, List<PreSaleFormListDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllPreSaleFormsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<PreSaleFormListDto>> Handle(
        GetAllPreSaleFormsQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.PreSaleForms
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new PreSaleFormListDto
            {
                Id = x.Id,
                CustomerFullName = x.CustomerFullName,
                CustomerPhone = x.CustomerPhone,
                TotalAmount = x.TotalAmount,
                DiscountAmount = x.DiscountAmount,
                DiscountedAmount = x.DiscountedAmount,
                CreatedAt = x.CreatedAt,
                PdfUrl = x.PdfFilePath
            })
            .ToListAsync(cancellationToken);
    }
}