using MediatR;
using Microsoft.EntityFrameworkCore;
using PreSaleForm.Application.Common.Interfaces;

namespace PreSaleForm.Application.PreSaleForms.Commands.Delete;

public class DeletePreSaleFormCommandHandler
    : IRequestHandler<DeletePreSaleFormCommand, DeletePreSaleFormResponse>
{
    private readonly IApplicationDbContext _context;

    public DeletePreSaleFormCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DeletePreSaleFormResponse> Handle(
        DeletePreSaleFormCommand command,
        CancellationToken cancellationToken)
    {
        var entity = await _context.PreSaleForms
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (entity == null)
        {
            throw new KeyNotFoundException($"PreSaleForm with ID {command.Id} not found.");
        }

        _context.PreSaleForms.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new DeletePreSaleFormResponse
        {
            Success = true,
            Message = "Form başarıyla silindi."
        };
    }
}

