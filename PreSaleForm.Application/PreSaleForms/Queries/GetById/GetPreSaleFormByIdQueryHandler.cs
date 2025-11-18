using MediatR;
using Microsoft.EntityFrameworkCore;
using PreSaleForm.Application.Common.Interfaces;
using PreSaleForm.Application.PreSaleForms.Commands.Create;

namespace PreSaleForm.Application.PreSaleForms.Queries.GetById;

public class GetPreSaleFormByIdQueryHandler :
    IRequestHandler<GetPreSaleFormByIdQuery, PreSaleFormDetailDto?>
{
    private readonly IApplicationDbContext _context;

    public GetPreSaleFormByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PreSaleFormDetailDto?> Handle(
        GetPreSaleFormByIdQuery request,
        CancellationToken cancellationToken)
    {
        // 1) Formu Products ile birlikte çek
        var form = await _context.PreSaleForms
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (form == null)
            return null;

        // 2) DTO Map – EF Core dışında, tamamen C# LINQ ile
        var dto = new PreSaleFormDetailDto
        {
            Id = form.Id,
            CustomerFullName = form.CustomerFullName,
            CustomerPhone = form.CustomerPhone,
            Note = form.Note,
            TotalAmount = form.TotalAmount,
            PaidAmount = form.PaidAmount,
            RemainingAmount = form.RemainingAmount,
            DiscountAmount = form.DiscountAmount,
            DiscountedAmount = form.DiscountedAmount,
            CreatedAt = form.CreatedAt,
            PdfUrl = form.PdfFilePath,

            Products = form.Products.Select(p => new PreSaleFormProductDto
            {
                DoorModel = p.DoorModel,
                DoorSurfaceType = p.DoorSurfaceType,
                DoorLeafWidth = p.DoorLeafWidth,
                DoorLeafHeight = p.DoorLeafHeight,
                DoorFrameWidth = p.DoorFrameWidth,
                DoorQuantity = p.DoorQuantity,
                IsWithGlass = p.IsWithGlass,
                Color = p.Color,
                Amount = p.Amount
            }).ToList()
        };

        return dto;
    }
}