using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _3CSharpProj.Entities.Contexts;
using _3CSharpProj.Entities.Models;
using _3CSharpProj.Entities.Repositories;

namespace _3CSharpProj.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IBayContext ctx) : ControllerBase
    {
        private readonly UserRepository? _userRepository = new(ctx);
        
        

        [HttpGet, Route("/Users")]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(_userRepository?.GetAll());
        }

        [HttpGet, Route("/Users/{id}")]
        public ActionResult<User> GetStudent(Guid id)
        {
            
            var user = _userRepository?.GetById(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost, Route("/Users")]
        public IActionResult PostUser(User user)
        {
            _userRepository?.Add(user);
            _userRepository?.SaveChanges();

            return CreatedAtAction("GetStudent", new { id = user.Id }, user);
        }

        [HttpDelete, Route("/Users/{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            try
            {
                _userRepository?.DeleteById(id);
                _userRepository?.SaveChanges();
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut, Route("/Users/{id}")]
        public IActionResult PutUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
                _userRepository?.Update(user);
                _userRepository?.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (_userRepository?.GetById(id) != null)
                {
                    return NotFound();
                }

                return BadRequest(e);
            }

            return NoContent();
        }
    }
}

