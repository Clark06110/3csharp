using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ibayProj.Entities.Contexts;
using ibayProj.Entities.Models;
using ibayProj.Entities.Models.Request;
using ibayProj.Entities.Repositories;
using ibayProj.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ibayProj.WebApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IBayContext ctx) : ControllerBase
    {

        private readonly UserRepository _userRepository = new(ctx);

        [HttpGet, Route("/users")]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(_userRepository.GetAll());
        }
        
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterModel registerModel)
        {
            var dbUser = await _userRepository.GetByEmail(registerModel.Email);
            if (dbUser != null)
            {
                throw new Exception("User already registered");
            }

            var userId = new Guid();
            
            var user = new User
            {
                Id = userId,
                Email = registerModel.Email,
                Pseudo = registerModel.Pseudo,
                Password = registerModel.Password
            };
            
            var token = JwtService.GenerateToken(user.Email);
            
            try
            {
                _userRepository.Add(user);
                _userRepository.SaveChanges();

                var response = new
                {
                    User = user,
                    Token = token
                };

                return Ok(response);
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (_userRepository?.GetById(userId) != null)
                {
                    return NotFound();
                }

                return BadRequest(e);
            }
        }
        
       
    }
}

