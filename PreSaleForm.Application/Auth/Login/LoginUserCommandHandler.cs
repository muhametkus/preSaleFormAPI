using MediatR;
using Microsoft.EntityFrameworkCore;
using PreSaleForm.Application.Common.Exceptions;
using PreSaleForm.Application.Common.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace PreSaleForm.Application.Auth.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtService _jwt;

    public LoginUserCommandHandler(IApplicationDbContext context, IJwtService jwt)
    {
        _context = context;
        _jwt = jwt;
    }

    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

        if (user == null)
            throw new AuthException("Email veya şifre hatalı.");

        var hash = ComputeHash(request.Password);

        if (user.PasswordHash != hash)
            throw new AuthException("Email veya şifre hatalı.");

        return _jwt.GenerateToken(user.Id, user.Email);
    }

    private string ComputeHash(string input)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(bytes);
    }
}