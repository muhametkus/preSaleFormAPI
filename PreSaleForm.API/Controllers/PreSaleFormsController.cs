using MediatR;
using Microsoft.AspNetCore.Mvc;
using PreSaleForm.Application.PreSaleForms.Commands.Create;
using PreSaleForm.Application.PreSaleForms.Queries.GetAll;
using PreSaleForm.Application.PreSaleForms.Queries.GetById;

namespace PreSaleForm.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PreSaleFormsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PreSaleFormsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // CREATE
    [HttpPost]
    public async Task<ActionResult<CreatePreSaleFormResponse>> Create(
        [FromBody] CreatePreSaleFormRequest request,
        CancellationToken cancellationToken)
    {
        if (!request.TermsAccepted)
            return BadRequest("Formu oluşturabilmek için şartlar kabul edilmelidir.");

        var cmd = new CreatePreSaleFormCommand { Request = request };
        var result = await _mediator.Send(cmd, cancellationToken);
        return Ok(result);
    }

    // GET ALL
    [HttpGet]
    public async Task<ActionResult<List<PreSaleFormListDto>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllPreSaleFormsQuery(), cancellationToken);
        return Ok(result);
    }

    // GET BY ID
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PreSaleFormDetailDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPreSaleFormByIdQuery { Id = id }, cancellationToken);

        if (result == null)
            return NotFound("Form bulunamadı.");

        return Ok(result);
    }

    // Health Check
    [HttpGet("ping")]
    public IActionResult Ping()
    {
        return Ok("PreSaleForm API çalışıyor 🚀");
    }
}