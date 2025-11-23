using MediatR;
using PreSaleForm.Application.Common.Interfaces;

namespace PreSaleForm.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Products.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new KeyNotFoundException($"Product with ID {request.Id} not found.");
        }

        entity.Name = request.Name;
        entity.ImageUrl = request.ImageUrl;
        entity.PriceWithAssembly = request.PriceWithAssembly;
        entity.PriceWithoutAssembly = request.PriceWithoutAssembly;
        entity.CategoryId = request.CategoryId;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
