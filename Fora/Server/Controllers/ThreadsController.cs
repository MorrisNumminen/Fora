using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fora.Client.Services;
using Microsoft.AspNetCore.Identity;

namespace Fora.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThreadsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ThreadsController(AppDbContext dbContext, SignInManager<ApplicationUser> signInManager)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
        }

        //GET api/<UsersController>
        [HttpGet("getthreads")]
        public async Task<List<ThreadModel>> GetThreads()
        {
            // Returnera lista med threads
            return _dbContext.Threads.ToList();
        }


        //POST api/<UsersController>
        [HttpPost("createthread")]
        public async Task<ActionResult<string>> CreateNewThread([FromBody] ThreadModel threadToCreate, [FromQuery] string token)
        {
            ThreadModel newThread = new();
            newThread.Name = threadToCreate.Name;

            var identityUser = _signInManager.UserManager.Users.FirstOrDefault(u => u.Token == token);

            if (identityUser != null)
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Username == identityUser.UserName);
                threadToCreate.User = user;

                _dbContext.Threads.Add(threadToCreate);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest("Could not create thread");
        }

    }
}
