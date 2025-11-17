using AutoMapper;
using MediatR;
using PreSaleForm.Application.Common.Interfaces;
using PreSaleForm.Domain.Entities;

namespace PreSaleForm.Application.PreSaleForms.Commands.Create;

public class CreatePreSaleFormCommandHandler
    : IRequestHandler<CreatePreSaleFormCommand, CreatePreSaleFormResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreatePreSaleFormCommandHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<CreatePreSaleFormResponse> Handle(
        CreatePreSaleFormCommand command,
        CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<PreSaleFormEntity>(command.Request);

        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTime.UtcNow;

        // ürünleri ekle
        foreach (var productDto in command.Request.Products)
        {
            var product = _mapper.Map<PreSaleFormProduct>(productDto);
            product.Id = Guid.NewGuid();
            product.PreSaleFormId = entity.Id;

            entity.Products.Add(product);
        }

        _context.PreSaleForms.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        var response = _mapper.Map<CreatePreSaleFormResponse>(entity);

        return response;
    }

}