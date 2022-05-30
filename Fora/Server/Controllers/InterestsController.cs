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
            list = await _dbContext.Interests.Include(x => x.User).ToListAsync();
            return Ok(list);
        }

        [HttpPost("AddUserInterest")]
        public async Task<ActionResult> AddUserInterest([FromBody] int interestId,[FromQuery] string token)
        {
            var identityUser = _signInManager.UserManager.Users.FirstOrDefault(u => u.Token == token);
            var dbUser = _dbContext.Users.FirstOrDefault(u => u.Username == identityUser.UserName);
            var dbInterest = _dbContext.Interests.FirstOrDefault(i => i.Id == interestId);

            if (dbUser != null && dbInterest != null)
            {
                UserInterestModel userInterest = new UserInterestModel()
                {
                    UserId = dbUser.Id,
                    InterestId = dbInterest.Id
                   
                };

                _dbContext.UserInterests.Add(userInterest);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }

            return Conflict();
        }

        [HttpGet("UserInterests")]
        public async Task<ActionResult<List<UserInterestModel>>> GetUserInterests([FromQuery] string token)
        {
            var identityUser = _signInManager.UserManager.Users.FirstOrDefault(u => u.Token == token);
            var user = _dbContext.Users.FirstOrDefault(u => u.Username == identityUser.UserName);

            if(user != null && identityUser != null)
            {
                var userInterests = _dbContext.UserInterests.Where(ui => ui.UserId == user.Id).Include(x => x.Interest).ToList();

                return Ok(userInterests);
            }

            return BadRequest();
        }

        // POST api/<UsersController>
        [HttpPost("createinterest")]
        public async Task<ActionResult<string>> CreateNewInterest([FromBody] InterestModel interestToCreate, [FromQuery] string token)
        {
            var identityUser = _signInManager.UserManager.Users.FirstOrDefault(u => u.Token == token);
            
            if (identityUser != null)
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Username == identityUser.UserName);
                interestToCreate.UserId = user.Id;

                _dbContext.Interests.Add(interestToCreate);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest("Could not create a interest");

        }

        [HttpDelete("deleteinterest/{interestId}")]
        public async Task DeleteInterest(int interestId)
        {

            var dbDeleteInterest = _dbContext.Interests.FirstOrDefault(i => i.Id == interestId);

            if (dbDeleteInterest != null)
            {
                _dbContext.Interests.Remove(dbDeleteInterest);

                await _dbContext.SaveChangesAsync();
            }
        }

        [HttpPost("removeuserinterest")]
        public async Task RemoveUserInterest([FromBody] UserInterestDto userInterest, [FromQuery] string token)
        {          
            var authUser = _signInManager.UserManager.Users.FirstOrDefault(u => u.Token == token);
            var dbUser = _dbContext.Users.FirstOrDefault(u => u.Username == authUser.UserName);

            if (dbUser != null)
            {
                var interest =  await _dbContext.UserInterests.FirstOrDefaultAsync(x => x.UserId == userInterest.UserId && x.InterestId == userInterest.InterestId);
                _dbContext.UserInterests.Remove(interest);               
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
