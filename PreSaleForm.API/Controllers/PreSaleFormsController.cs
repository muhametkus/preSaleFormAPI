using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PreSaleForm.Application.PreSaleForms.Commands.Create;
using PreSaleForm.Application.PreSaleForms.Commands.Delete;
using PreSaleForm.Application.PreSaleForms.Commands.DeleteAll;
using PreSaleForm.Application.PreSaleForms.Commands.GeneratePdf;
using PreSaleForm.Application.PreSaleForms.Commands.Update;
using PreSaleForm.Application.PreSaleForms.Queries.GetAll;
using PreSaleForm.Application.PreSaleForms.Queries.GetById;
using PreSaleForm.Application.PreSaleForms.Queries.GetPdf;

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

    // GENERATE PDF
    [HttpPost("{id:guid}/generate-pdf")]
    public async Task<ActionResult<GeneratePdfResponse>> GeneratePdf(
        Guid id,
        CancellationToken cancellationToken)
    {
        var cmd = new GeneratePdfCommand { FormId = id };
        var result = await _mediator.Send(cmd, cancellationToken);
        return Ok(result);
    }

    // GET PDF FILE
    [HttpGet("{id:guid}/pdf")]
    public async Task<IActionResult> GetPdf(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetPreSaleFormPdfQuery { Id = id };
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null)
            return NotFound("PDF dosyası bulunamadı. Önce 'Generate PDF' işlemini yapınız.");

        return File(result.FileContent, result.ContentType, result.FileName);
    }

    // GET ALL
    
    [Authorize]
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

    // UPDATE
    [Authorize]
    [HttpPut]
    [Route("{id:guid}")]
    public async Task<ActionResult<UpdatePreSaleFormResponse>> Update(
        Guid id,
        [FromBody] UpdatePreSaleFormRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            request.Id = id;
            var cmd = new UpdatePreSaleFormCommand { Request = request };
            var result = await _mediator.Send(cmd, cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    // DELETE
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<DeletePreSaleFormResponse>> Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var cmd = new DeletePreSaleFormCommand { Id = id };
            var result = await _mediator.Send(cmd, cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Form bulunamadı.");
        }
    }

    // DELETE ALL
    [Authorize]
    [HttpDelete]
    [Route("all")]
    public async Task<ActionResult<DeleteAllPreSaleFormsResponse>> DeleteAll(CancellationToken cancellationToken)
    {
        var cmd = new DeleteAllPreSaleFormsCommand();
        var result = await _mediator.Send(cmd, cancellationToken);
        return Ok(result);
    }

    // Health Check
    [HttpGet("ping")]
    public IActionResult Ping()
    {
        return Ok("PreSaleForm API çalışıyor 🚀");
    }
}