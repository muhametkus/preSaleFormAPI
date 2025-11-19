using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PreSaleForm.Application.Auth.Register;

public record RegisterUserCommand(
    [Required(ErrorMessage = "Ad Soyad alanı zorunludur.")]
    [MinLength(2, ErrorMessage = "Ad Soyad en az 2 karakter olmalıdır.")]
    [MaxLength(100, ErrorMessage = "Ad Soyad en fazla 100 karakter olabilir.")]
    string FullName,

    [Required(ErrorMessage = "Email alanı zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
    string Email,

    [Required(ErrorMessage = "Şifre alanı zorunludur.")]
    [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
    [MaxLength(100, ErrorMessage = "Şifre en fazla 100 karakter olabilir.")]
    string Password
) : IRequest<string>;