using MediatR;
using PreSaleForm.Application.Common.Interfaces;

namespace PreSaleForm.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Products.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new KeyNotFoundException($"Product with ID {request.Id} not found.");
        }

        _context.Products.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
