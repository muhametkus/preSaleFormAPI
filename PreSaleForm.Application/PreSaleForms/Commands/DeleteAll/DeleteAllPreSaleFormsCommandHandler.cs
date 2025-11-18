using MediatR;
using Microsoft.EntityFrameworkCore;
using PreSaleForm.Application.Common.Interfaces;

namespace PreSaleForm.Application.PreSaleForms.Commands.DeleteAll;

public class DeleteAllPreSaleFormsCommandHandler
    : IRequestHandler<DeleteAllPreSaleFormsCommand, DeleteAllPreSaleFormsResponse>
{
    private readonly IApplicationDbContext _context;

    public DeleteAllPreSaleFormsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DeleteAllPreSaleFormsResponse> Handle(
        DeleteAllPreSaleFormsCommand command,
        CancellationToken cancellationToken)
    {
        var forms = await _context.PreSaleForms.ToListAsync(cancellationToken);
        var count = forms.Count;

        _context.PreSaleForms.RemoveRange(forms);
        await _context.SaveChangesAsync(cancellationToken);

        return new DeleteAllPreSaleFormsResponse
        {
            Success = true,
            DeletedCount = count,
            Message = $"{count} form başarıyla silindi."
        };
    }
}

