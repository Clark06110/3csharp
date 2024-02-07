using ibayProj.Entities.Contexts;
using ibayProj.Entities.Models;
using ibayProj.Entities.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ibayProj.WebApi.Controllers;

public class ProductController(IBayContext ctx): ControllerBase
{
    private readonly ProductRepository _productRepository = new(ctx);

    [HttpGet, Route("/products")]
    public ActionResult<IEnumerable<Product>> GetProducts()
    {
        return Ok(_productRepository.GetAll());
    }

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

    [HttpPut, Route("/product/{id}")]
    public IActionResult PutProduct(Guid id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        try
        {
            _productRepository.Update(product);
            _productRepository.SaveChanges();
        }catch (DbUpdateConcurrencyException)
        {
            if (_productRepository?.GetById(id) != null)
            {
                return NotFound();
            }

            return BadRequest();
        }

        return NoContent();
    }
}