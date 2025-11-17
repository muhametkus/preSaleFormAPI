using Microsoft.EntityFrameworkCore;
using PreSaleForm.Domain.Entities;

namespace PreSaleForm.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<PreSaleFormEntity> PreSaleForms { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}