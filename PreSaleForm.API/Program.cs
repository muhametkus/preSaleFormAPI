using PreSaleForm.Infrastructure.DependencyInjection;
using AutoMapper;
using MediatR;
using QuestPDF.Infrastructure;
QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args);

// Application & MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(PreSaleForm.Application.PreSaleForms.Commands.Create.CreatePreSaleFormCommand).Assembly);
});



builder.Services.AddAutoMapper(typeof(PreSaleForm.Application.PreSaleForms.PreSaleFormProfile));

// Infrastructure
builder.Services.AddInfrastructureServices(builder.Configuration);

// Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");
app.MapControllers();
app.UseStaticFiles();

app.Run();