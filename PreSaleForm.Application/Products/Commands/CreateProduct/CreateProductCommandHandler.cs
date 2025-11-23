using MediatR;
using PreSaleForm.Application.Common.Interfaces;
using PreSaleForm.Domain.Entities;

namespace PreSaleForm.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            ImageUrl = request.ImageUrl,
            PriceWithAssembly = request.PriceWithAssembly,
            PriceWithoutAssembly = request.PriceWithoutAssembly,
            CategoryId = request.CategoryId
        };

        _context.Products.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
