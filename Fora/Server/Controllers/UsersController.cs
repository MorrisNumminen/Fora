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
        private readonly AppDbContext _context;
        private readonly AuthDbContext _authContext;
  

        public UsersController(SignInManager<ApplicationUser> signInManager, AppDbContext context, AuthDbContext authContext)
        {
            _signInManager = signInManager;
            _context = context;
            _authContext = authContext;
           
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
                    Username = userToRegister.Username,
                });
                await _context.SaveChangesAsync();

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

        [HttpGet("check/{token}")]
        public async Task<ActionResult<LoginDto>> CheckUserLogin([FromRoute] string token)
        {
            // See what user has the specified token (in the db)



            var userWithToken = _signInManager.UserManager.Users.FirstOrDefault(u => u.Token == token);

            if (userWithToken != null)
            {
                LoginDto loginStatus = new();
                loginStatus.IsLoggedIn = true;

                // Is user Deleted?

                var roleCheckResult = await _signInManager.UserManager.IsInRoleAsync(userWithToken, "deleted");

                if (roleCheckResult)
                {
                    loginStatus.IsDeleted = true;
                }
                else if (roleCheckResult)
                {
                    loginStatus.IsBanned = true;
                }
                return Ok(loginStatus);
            }

            return BadRequest("User not found");
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {



        }

        // DELETE api/<UsersController>/5
        [HttpDelete("delete/{token}")]
        public async Task<ActionResult> DeleteUser(string token)
        {
            var userWithToken =  _signInManager.UserManager.Users.FirstOrDefault(u => u.Token == token);
            var user = _context.Users.FirstOrDefault(u => u.Username == userWithToken.UserName);
            

            if (userWithToken != null)
            {
                var deleteResult = await _signInManager.UserManager.DeleteAsync(userWithToken);
                
                if(deleteResult.Succeeded)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                    

                    return Ok();
                }
            }

            return BadRequest();
        }

        [HttpPost("change")]
        public async Task<IActionResult> ChangeUserPassword([FromBody] UserDto user, [FromQuery] string newPassword, [FromQuery] string token)
        {
            var userWithToken = _signInManager.UserManager.Users.FirstOrDefault(u => u.Token == token);

            if (userWithToken != null)
            {
                var changePasswordResult = await _signInManager.UserManager.ChangePasswordAsync(userWithToken, user.Password, newPassword);

                if (changePasswordResult.Succeeded)
                {
                    return Ok();
                }
            }

            return BadRequest();
        }
    }
}
