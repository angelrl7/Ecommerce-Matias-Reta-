using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ecomercio.Application.Feature.Products.Commands;
using Ecomercio.Application.Feature.Products.Queries;
using Ecomercio.Domain.Constants;

namespace Ecomercio.WebApi.Controllers;

[Authorize]
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
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllProductsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id));
        return Ok(result);
    }

    [Authorize(Roles = UserRoles.Admin)]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [Authorize(Roles = UserRoles.Admin)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteProductCommand(id));
        return NoContent();
    }
}