using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fora.Client.Services;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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



        //GET messages related to thread
        [HttpGet("getthreadmessages/{threadId}")]
        public async Task<List<MessageModel>> GetThreadMessages(int threadId)
        {
            // Returnera lista med threads
            //return _dbContext.Messages.ToList();
            int CurrentThreadId = threadId;
            Console.WriteLine(CurrentThreadId);

        var messages = _dbContext.Messages.Include(m => m.User).Where(m => m.ThreadId == CurrentThreadId).Select(m => new MessageModel
            {

            Message = m.Message,
                User = new UserModel()
                {
                    Id = m.User.Id,
                    Username = m.User.Username,
                    Banned = m.User.Banned,
                    Deleted = m.User.Deleted,
                }
            }).ToList();

            return messages;
        }

        //POST a new message
        [HttpPost("createmessage")]
        public async Task<ActionResult<string>> CreateMessage([FromBody] MessageModel messageToCreate, [FromQuery] string token)
        {
            MessageModel newMessage = new();

            var identityUser = _signInManager.UserManager.Users.FirstOrDefault(u => u.Token == token);

            if (identityUser != null)
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Username == identityUser.UserName);
                messageToCreate.User = user;

                _dbContext.Messages.Add(messageToCreate);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest("Could not create message");
        }

        //POST a new message
        [HttpPost("deletemessage")]
        public async Task<ActionResult<string>> DeleteMessage([FromBody] int messageDelId, [FromQuery] string token)
        {
            Console.WriteLine("DeleteMessage() : Controller");

            MessageModel Message = new();

            var identityUser = _signInManager.UserManager.Users.FirstOrDefault(u => u.Token == token);

            if (identityUser != null)
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Username == identityUser.UserName);

                Message = _dbContext.Messages.FirstOrDefault(m => m.Id == messageDelId);

                Message.Message = null;
                

                _dbContext.Messages.Update(Message);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest("Could not create message");
        }

        //[HttpGet]
        //public async Task<ActionResult<int>> GetThreadId()
        //{
        //    var threadID = await _dbContext.Threads.ThreadId;
        //}

    }
}
