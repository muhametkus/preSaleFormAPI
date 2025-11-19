using MediatR;
using Microsoft.EntityFrameworkCore;
using PreSaleForm.Application.Common.Exceptions;
using PreSaleForm.Application.Common.Interfaces;
using PreSaleForm.Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace PreSaleForm.Application.Auth.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
{
    private readonly IApplicationDbContext _context;

    public RegisterUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        bool exists = await _context.Users.AnyAsync(x => x.Email == request.Email);
        if (exists)
            throw new AuthException("Bu email zaten kayıtlı.");

        var hash = ComputeHash(request.Password);

        var user = new User
        {
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = hash
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return "Kayıt başarılı.";
    }

    private string ComputeHash(string input)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(bytes);
    }
}