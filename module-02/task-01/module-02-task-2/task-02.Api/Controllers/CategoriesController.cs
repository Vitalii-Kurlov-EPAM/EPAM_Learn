using MediatR;
using Microsoft.AspNetCore.Mvc;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.CategoryObject.Commands;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.CategoryObject.Queries;
using Module_02.Task_02.CatalogService.WebApi.MappingExtensions;
using Module_02.Task_02.CatalogService.WebApi.Models.Category;

namespace Module_02.Task_02.CatalogService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
[Consumes("application/json")]
[Produces("application/json")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CategoryResponse.ItemDto>))]
    public async Task<IActionResult> GetAll()
    {
        var allCategories = await _mediator.Send(new GetAllCategoriesQuery{IncludeDeps = true});
        return Ok(allCategories.Select(model => model.ToCategoryResponseItemDto()));
    }

    [HttpGet("{categoryId:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CategoryResponse.ItemDto>))]
    public async Task<IActionResult> GetById(int categoryId)
    {
        var foundCategory = await _mediator.Send(new GetCategoryByIdQuery(categoryId, true));

        return foundCategory == null
            ? NotFound()
            : Ok(foundCategory.ToCategoryResponseItemDto());
    }

    [HttpDelete("{categoryId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteById(int categoryId)
    {
        var result =  await _mediator.Send(new DeleteCategoryByIdCommand(categoryId));
        return result
            ? NoContent()
            : NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create(CategoryRequest.CreateModel model)
    {
        var category = await _mediator.Send(model.ToCreateCategoryCommand());
    
        return CreatedAtAction("GetById", new { categoryId = category.CategoryId }, category);
    }

    [HttpPut("{categoryId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int categoryId, CategoryRequest.UpdateModel model)
    {
        var result = await _mediator.Send(model.ToUpdateCategoryCommand(categoryId));

        return result
            ? NoContent()
            : NotFound();
    }
}