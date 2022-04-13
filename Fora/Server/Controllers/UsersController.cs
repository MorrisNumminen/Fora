﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fora.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;

        public UsersController(SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _signInManager = signInManager;
            _context = context;
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
                _context.Users.Add(new UserModel()
                {
                    Username = userToRegister.Username
                });
                await _context.SaveChangesAsync();

                // Token

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


        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserDto userToLogin)
        {
            // Calla Api

            var user = await _signInManager.UserManager.FindByNameAsync(userToLogin.Username);

            if (user != null && await _signInManager.UserManager.CheckPasswordAsync(user, userToLogin.Password))
            {
                //await _signInManager.UserManager.UpdateAsync(user);

                return Ok(user.Token);
            }

            return BadRequest("Could not login");
        }

        [HttpGet]
        [Route("check")]
        public async Task<LoginDto> CheckUserLogin([FromQuery] string token)
        {
            // See what user has the specified token (in the db)

            LoginDto loginStatus = new();

            var userWithToken = _signInManager.UserManager.Users.FirstOrDefault(u => u.Token == token);

            if (userWithToken != null)
            {
                loginStatus.IsLoggedIn = true;

                // Is user admin?

                var roleCheckResult = await _signInManager.UserManager.IsInRoleAsync(userWithToken, "Admin");

                if (roleCheckResult)
                {
                    loginStatus.IsAdmin = true;
                }
            }

            return loginStatus;
        }


        //DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
