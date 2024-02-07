using ibayProj.Entities.Contexts;
using ibayProj.Entities.Models;
using ibayProj.Entities.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ibayProj.WebApi.Controllers;

public class CartController(IBayContext ctx) : ControllerBase
{

    private readonly CartRepository _cartRepository = new(ctx);
    
    [HttpGet, Route("/carts/{id}")]
    public ActionResult<Cart> GetCart(Guid id)
    {
        var cart = _cartRepository?.GetById(id);
        return cart == null ? NotFound() : Ok(cart);
    }
    
    [HttpPost, Route("/carts")]
    public IActionResult PostCart(Cart cart)
    {
        _cartRepository?.Add(cart);
        _cartRepository?.SaveChanges();

        return CreatedAtAction("GetCart", new { id = cart.Id }, cart);
    }

}