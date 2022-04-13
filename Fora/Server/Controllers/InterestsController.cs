using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fora.Client.Services;
using Microsoft.AspNetCore.Identity;

namespace Fora.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public InterestsController(AppDbContext dbContext, SignInManager<ApplicationUser> signInManager)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
        }

        // GET api/<UsersController>
        [HttpGet("getinterests")]
        public async Task<List<InterestModel>> GetInterests()
        {
            // Returnera lista med interests
            return _dbContext.Interests.ToList();
        }


        // POST api/<UsersController>
        [HttpPost("createinterest")]
        public async Task<ActionResult<string>> CreateNewInterest([FromBody] InterestModel interestToCreate, [FromQuery] string token)
        {
            InterestModel newInterest = new();
            newInterest.Name = interestToCreate.Name;

            var identityUser = _signInManager.UserManager.Users.FirstOrDefault(u => u.Token == token);

            if (identityUser != null)
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Username == identityUser.UserName);
                interestToCreate.User = user;

                _dbContext.Interests.Add(interestToCreate);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest("Could not create a user");

        }

        // POST api/<UsersController>
        [HttpPost("addinterest")]
        public async Task AddUserInterest([FromBody] InterestModel interest, [FromQuery] string token)
        {
            var interestToAdd = _dbContext.Interests.FirstOrDefault(i => i.Id == interest.Id);
            var authUser = _signInManager.UserManager.Users.FirstOrDefault(u => u.Token == token);
            var user = _dbContext.Users.FirstOrDefault(u => u.Username == authUser.UserName);

            if (interestToAdd != null && user != null)
            {
                _dbContext.UserInterests.Add(new UserInterestModel()
                {
                    User = user,
                    Interest = interestToAdd,
                });
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
