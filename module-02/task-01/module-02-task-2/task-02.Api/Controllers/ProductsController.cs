using MediatR;
using Microsoft.AspNetCore.Mvc;
using Module_02.Task_02.BLL.CQRS.ProductObject.Commands;
using Module_02.Task_02.BLL.CQRS.ProductObject.Queries;
using Module_02.Task_02.CatalogService.WebApi.MappingExtensions;
using Module_02.Task_02.CatalogService.WebApi.Models.Product;

namespace Module_02.Task_02.CatalogService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
[Consumes("application/json")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductResponse.ItemDto>))]
    public async Task<IActionResult> GetAll()
    {
        var allProducts = await _mediator.Send(new GetAllProductsQuery(true));

        return Ok(allProducts.Select(model => model.ToProductResponseItemDto()));
    }

    [HttpGet("{productId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductResponse.ItemDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductResponse.ItemDto>))]
    public async Task<IActionResult> GetById(int productId)
    {
        var foundProduct = await _mediator.Send(new GetProductByIdQuery(productId, true));

        return foundProduct == null
            ? NotFound()
            : Ok(foundProduct.ToProductResponseItemDto());
    }

    [HttpDelete("{productId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteById(int productId, [FromQuery] int amount)
    {
        var result = await _mediator.Send(new DeleteProductByIdCommand(productId, amount));
        return result
            ? NoContent()
            : NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(ProductRequest.CreateModel model)
    {
        var productId = await _mediator.Send(model.ToCreateProductCommand());
        return CreatedAtAction("GetById", new { productId }, null);
    }

    [HttpPut("{productId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int productId, ProductRequest.UpdateModel model)
    {
        var result = await _mediator.Send(model.ToUpdateProductCommand(productId));
        return result
            ? NoContent()
            : NotFound();
    }
}