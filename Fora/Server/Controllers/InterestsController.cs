using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Fora.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public InterestsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/<UsersController>
        [HttpGet("getinterests")]
        public async Task<List<InterestModel>> GetInterest()
        {

           //var listOfInterests = _dbContext.Interests.ToList();

            return _dbContext.Interests.ToList();
        }


        // POST api/<UsersController>
        //[HttpPost("addinterest")]
        //public async Task<ActionResult<string>> CreateInterest([FromBody] InterestModel interestToAdd)
        //{

        //    _dbContext.Interests.Add(new InterestModel { Id = 1, Name = "Tv-spel", Threads = null, UserInterests = null, UserId = 999, User = null });
        //    _dbContext.SaveChanges();

        //    return BadRequest("Could not create a user");
        //}

    }
}
