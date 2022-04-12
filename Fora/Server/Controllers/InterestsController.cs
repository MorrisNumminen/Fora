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

           //var listOfInterests = _dbContext.Interests.ToList();

            return _dbContext.Interests.ToList();
        }


        // POST api/<UsersController>
        [HttpPost("createinterest")]
        public async Task<ActionResult<string>> CreateNewInterest([FromBody] InterestModel interestToCreate, [FromQuery] string token)
        {
            InterestModel newInterest = new();
            //newInterest.Id = interestToCreate.Id;
            newInterest.Name = interestToCreate.Name;
            //newInterest.Threads = interestToCreate.Threads;

            //var identityUser = _signInManager.UserManager.Users.FirstOrDefault(u => u.Token == token);
            var identityUser = _signInManager.UserManager.Users.FirstOrDefault(u => u.UserName == "Albin1337");


            if (identityUser != null)
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Username == identityUser.UserName);
                interestToCreate.User = user;

                _dbContext.Interests.Add(interestToCreate);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }


            //bool isUnique = false;

            //foreach (var interest in await GetInterests())
            //{
            //    if (newInterest.Name.ToLower() != interest.Name.ToLower())
            //    {
            //        isUnique = true;
            //    }
            //}
            //if (isUnique)
            //{
            //    newInterest.Id = Convert.ToInt32(Guid.NewGuid().ToString());
            //    _dbContext.Add<InterestModel>(newInterest);

            //    //_dbContext.Add(newInterest);
            //    _dbContext.Update(newInterest);
            //    _dbContext.SaveChanges();

            //    return Ok(newInterest.Id);
            //}



      
            return BadRequest("Could not create a user");

        }

    }
}
