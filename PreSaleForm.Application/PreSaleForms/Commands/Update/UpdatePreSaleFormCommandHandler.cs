using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PreSaleForm.Application.Common.Interfaces;
using PreSaleForm.Domain.Entities;

namespace PreSaleForm.Application.PreSaleForms.Commands.Update;

public class UpdatePreSaleFormCommandHandler
    : IRequestHandler<UpdatePreSaleFormCommand, UpdatePreSaleFormResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdatePreSaleFormCommandHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UpdatePreSaleFormResponse> Handle(
        UpdatePreSaleFormCommand command,
        CancellationToken cancellationToken)
    {
        var entity = await _context.PreSaleForms
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == command.Request.Id, cancellationToken);

        if (entity == null)
        {
            throw new Exception($"PreSaleForm with ID {command.Request.Id} not found.");
        }

        // Temel bilgileri güncelle
        entity.CustomerFullName = command.Request.CustomerFullName;
        entity.CustomerPhone = command.Request.CustomerPhone;
        entity.TotalAmount = command.Request.TotalAmount;
        entity.PaidAmount = command.Request.PaidAmount;
        entity.RemainingAmount = command.Request.RemainingAmount;
        entity.DiscountAmount = command.Request.DiscountAmount;
        entity.DiscountedAmount = command.Request.DiscountedAmount;
        entity.MontajDahilMi = command.Request.MontajDahilMi;
        entity.NakliyeDahilMi = command.Request.NakliyeDahilMi;
        entity.FabrikaTeslimMi = command.Request.FabrikaTeslimMi;
        entity.AksesuarDahilMi = command.Request.AksesuarDahilMi;
        entity.AksesuarUcreti = command.Request.AksesuarUcreti;
        entity.NakliyeUcreti = command.Request.NakliyeUcreti;
        entity.SecilenAksesuar = command.Request.SecilenAksesuar;
        
        // Söküm bilgilerini güncelle
        entity.OldDoorCount = command.Request.OldDoorCount;
        entity.DismantlingUnitPrice = command.Request.DismantlingUnitPrice;
        entity.TotalDismantlingPrice = command.Request.TotalDismantlingPrice;

        // Mevcut ürünleri temizle
        entity.Products.Clear();

        // Yeni ürünleri ekle
        foreach (var productDto in command.Request.Products)
        {
            var product = _mapper.Map<PreSaleFormProduct>(productDto);
            product.Id = Guid.NewGuid();
            product.PreSaleFormId = entity.Id;

            entity.Products.Add(product);
        }

        await _context.SaveChangesAsync(cancellationToken);

        var response = _mapper.Map<UpdatePreSaleFormResponse>(entity);

        return response;
    }
}

