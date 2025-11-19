using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PreSaleForm.Application.Auth.Login;

public record LoginUserCommand(
    [Required(ErrorMessage = "Email alanı zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
    string Email,

    [Required(ErrorMessage = "Şifre alanı zorunludur.")]
    [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
    string Password
) : IRequest<string>;
