using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PreSaleForm.Application.Products.Commands.CreateProduct;
using PreSaleForm.Application.Products.Commands.DeleteProduct;
using PreSaleForm.Application.Products.Commands.UpdateProduct;
using PreSaleForm.Application.Products.Queries.Dto;
using PreSaleForm.Application.Products.Queries.GetAllProducts;
using PreSaleForm.Application.Products.Queries.GetProductsByCategory;

namespace PreSaleForm.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllProductsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("category/{categoryId:guid}")]
    public async Task<ActionResult<List<ProductDto>>> GetByCategory(Guid categoryId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetProductsByCategoryQuery(categoryId), cancellationToken);
        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest("ID mismatch");
        }

        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteProductCommand(id), cancellationToken);
        return NoContent();
    }
}
