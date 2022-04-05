using Fora.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fora.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<string>> SignUpAsync([FromBody] UserDto userToRegister)
        {
            ApplicationUser newUser = new();
            newUser.UserName = userToRegister.Username;

            var createUserResult = await _signInManager.UserManager.CreateAsync(newUser, userToRegister.Password);

            // Check createUserResult
            if (createUserResult.Succeeded)
            {
                string token = Guid.NewGuid().ToString();

                // Add the new token to the user in the identity db

                newUser.Token = token;

                await _signInManager.UserManager.UpdateAsync(newUser);

                return Ok(token);

            }
            return BadRequest("Could not create a user");
            // Create token
            // Add that token to the Identity Db
            // Add the new user to the other db too
            // Send token back


        }
            public void Post([FromBody] UserDto userToRegister)
        {
            // Calla Api





           
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
