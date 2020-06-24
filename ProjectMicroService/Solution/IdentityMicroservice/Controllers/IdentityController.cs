using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Configure;
using EmailService;
using IdentityMicroservice.Model;
using IdentityMicroservice.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace IdentityMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        private readonly IJwtBuilder _jwtBuilder;
        private readonly IUserRepository _userRepository;
        private readonly IEncryptor _encryptor;
        public IdentityController(IMongoDatabase db, IEncryptor encryptor,IJwtBuilder jwtBuilder,IEmailSender emailSender)
        {
            _userRepository =new  UserRepository(db);
            _encryptor = encryptor;
            _jwtBuilder = jwtBuilder;
            _emailSender = emailSender;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody]User user)
        {
            var u = _userRepository.GetUser(user.Email);
            if (u==null)
            {
                return NotFound("User not found.");
            }
            var isValid = u.ValidatePassword(user.Password, _encryptor);

            if (!isValid)
            {
                return BadRequest("Could not authenticate user.");
            }

            var token = _jwtBuilder.GetToken(u.Id);

            return new OkObjectResult(token);
        }


        [HttpPost("Register")]
        public  IActionResult Register([FromBody]User user)
        {
            var u = _userRepository.GetUser(user.Email);
            if (u != null)
            {
                return BadRequest("User already exists.");
            }
            user.SetPassword(user.Password, _encryptor);
            _userRepository.InsertUser(user);
             var message = new Message(new string[] { "alinouriare@yahoo.com" }, "Test email async", "This is the content from our async email.", null);
             _emailSender.SendEmail(message);
            return Ok();
        }
        [HttpGet("validate")]
        public IActionResult Validate([FromQuery(Name = "email")] string email, [FromQuery(Name = "token")] string token)
        {
            var u = _userRepository.GetUser(email);

            if (u == null)
            {
                return NotFound("User not found.");
            }

            var userId = _jwtBuilder.ValidateToken(token);

            if (userId != u.Id)
            {
                return BadRequest("Invalid token.");
            }

            return new OkObjectResult(userId);
        }
    }
}
