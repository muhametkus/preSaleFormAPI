using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PreSaleForm.Application.Common.Interfaces;
using PreSaleForm.Infrastructure.Persistence;
using PreSaleForm.Infrastructure.Services;

namespace PreSaleForm.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // PostgreSQL bağlantısı
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorCodesToAdd: null)));

        // DbContext interface binding (Application katmanı kullanacak)
        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());
        
        services.AddScoped<IPdfService, PdfService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IFileUploadService, FileUploadService>();



        return services;
    }
}