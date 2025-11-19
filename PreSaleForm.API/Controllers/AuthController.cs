using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PreSaleForm.Application.Auth.Login;
using PreSaleForm.Application.Auth.Register;
using PreSaleForm.Application.Common.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PreSaleForm.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {
        try
        {
            // Model validation
            var validationContext = new ValidationContext(command);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(command, validationContext, validationResults, true))
            {
                return BadRequest(new
                {
                    message = "Validasyon hataları var.",
                    errors = validationResults.Select(v => v.ErrorMessage)
                });
            }

            var result = await _mediator.Send(command);
            return Ok(new { message = result });
        }
        catch (AuthException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Bir hata oluştu.", error = ex.Message });
        }
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginUserCommand command)
    {
        try
        {
            // Model validation
            var validationContext = new ValidationContext(command);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(command, validationContext, validationResults, true))
            {
                return BadRequest(new
                {
                    message = "Validasyon hataları var.",
                    errors = validationResults.Select(v => v.ErrorMessage)
                });
            }

            var token = await _mediator.Send(command);
            return Ok(new { token });
        }
        catch (AuthException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Bir hata oluştu.", error = ex.Message });
        }
    }
}