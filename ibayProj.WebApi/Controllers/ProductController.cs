using ibayProj.Entities.Contexts;
using ibayProj.Entities.Models;
using ibayProj.Entities.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ibayProj.WebApi.Controllers;

public class ProductController(IBayContext ctx): ControllerBase
{
    private readonly ProductRepository _productRepository = new(ctx);

    [HttpGet, Route("/products/{id}")]
    public ActionResult<Product> GetProduct(Guid id)
    {
        var product = _productRepository?.GetById(id);
        return product == null ? NotFound() : Ok(product);
    }
    
    [HttpPost, Route("/products")]
    public IActionResult PostProduct(Product product)
    {
        _productRepository?.Add(product);
        _productRepository?.SaveChanges();

        return CreatedAtAction("GetProduct", new { id = product.Id }, product);
    }

    [HttpDelete, Route("/products/{id}")]
    public IActionResult DeleteProducts(Guid id)
    {
        try
        {
            _productRepository.DeleteById(id);
            _productRepository.SaveChanges();
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}