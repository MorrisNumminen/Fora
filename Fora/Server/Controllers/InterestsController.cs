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

        [HttpPost("AddUserInterest")]
        public async Task<ActionResult<List<InterestModel>>> AddUserInterest([FromQuery] string token, [FromBody] InterestModel interest)
        {
            var identityUser = _signInManager.UserManager.Users.FirstOrDefault(u => u.Token == token);
            var dbUser = _dbContext.Users.FirstOrDefault(u => u.Username == identityUser.UserName);
            var dbInterest = _dbContext.Interests.FirstOrDefault(i => i.Id == interest.Id);

            if (dbUser != null && dbInterest != null)
            {
                UserInterestModel userInterest = new UserInterestModel()
                {
                    User = dbUser,
                    Interest = dbInterest
                };

                _dbContext.UserInterests.Add(userInterest);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("UserInterests")]
        public async Task<ActionResult<List<InterestModel>>> GetUserInterests([FromQuery] string token)
        {
            var identityUser = _signInManager.UserManager.Users.FirstOrDefault(u => u.Token == token);
            var user = _dbContext.Users.FirstOrDefault(u => u.Username == identityUser.UserName);

            if(user != null && identityUser != null)
            {
                var userInterests = _dbContext.Interests.Where(i => i.UserInterests.Any(ui => ui.UserId == user.Id)).ToList();

                return Ok(userInterests);
            }

            return BadRequest();
        }

        // POST api/<UsersController>
        [HttpPost("createinterest")]
        public async Task<ActionResult<string>> CreateNewInterest([FromBody] InterestModel interestToCreate, [FromQuery] string token)
        {
            

            InterestModel newInterest = new();
            newInterest.Name = interestToCreate.Name;

            var identityUser = _signInManager.UserManager.Users.FirstOrDefault(u => u.Token == token);
            var user = _dbContext.Users.FirstOrDefault(u => u.Username == identityUser.UserName);

            if (identityUser != null)
            {
                
                 interestToCreate.User = user;

                _dbContext.Interests.Add(interestToCreate);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest("Could not create a user");

        }

       


        [HttpPost("removeuserinterest")]
        public async Task RemoveUserInterest([FromBody] InterestModel interest, [FromQuery] string token)
        {

            var dbRemoveInterest = _dbContext.Interests.FirstOrDefault(i => i.Id == interest.Id);
            var authUser = _signInManager.UserManager.Users.FirstOrDefault(u => u.Token == token);
            var dbUser = _dbContext.Users.FirstOrDefault(u => u.Username == authUser.UserName);

            if (dbRemoveInterest != null && dbUser != null)
            {
                _dbContext.UserInterests.Remove(new UserInterestModel()
                {
                    User = dbUser,
                    Interest = dbRemoveInterest,
                });

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
