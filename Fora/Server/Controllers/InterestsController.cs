using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fora.Client.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<List<InterestModel>>> GetAsync()
        {
            List<InterestModel> list = new();
            list = await _dbContext.Interests.ToListAsync();
            return Ok(list);
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
            

            if (identityUser != null)
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Username == identityUser.UserName);

                // interestToCreate.User = user;

                _dbContext.Interests.Add(interestToCreate);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest("Could not create a interest");

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
