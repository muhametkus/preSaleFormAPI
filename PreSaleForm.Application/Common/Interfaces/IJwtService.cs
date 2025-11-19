namespace PreSaleForm.Application.Common.Interfaces;

public interface IJwtService
{
    string GenerateToken(Guid userId, string email);
}