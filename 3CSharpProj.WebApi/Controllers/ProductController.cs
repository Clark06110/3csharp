using _3CSharpProj.Entities.Contexts;
using _3CSharpProj.Entities.Models;
using _3CSharpProj.Entities.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _3CSharpProj.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IBayContext ctx) : ControllerBase
    {
        private readonly ProductRepository? _productRepository = new(ctx);

        [HttpGet, Route("/Products")]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return Ok(_productRepository?.GetAll());
        }

        [HttpGet, Route("/Products/{id}")]
        public ActionResult<Product> GetProduct(Guid id)
        {
            var product = _productRepository?.GetById(id);
            return product == null ? NotFound() : Ok(product);
        }
        
        [HttpPost, Route("/Products")]
        public IActionResult PostProduct(Product product)
        {
            _productRepository?.Add(product);
            _productRepository?.SaveChanges();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }
        
        [HttpPut, Route("/Products/{id}")]
        public IActionResult PutProduct(Guid id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            try
            {
                _productRepository?.Update(product);
                _productRepository?.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (_productRepository?.GetById(id) != null)
                {
                    return NotFound();
                }

                return BadRequest(e);
            }

            return NoContent();
        }
        
    }
}

