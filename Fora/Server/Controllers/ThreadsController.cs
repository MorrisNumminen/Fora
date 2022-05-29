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
                       
            List<MessageModel> messages = await _dbContext.Messages.Include(x => x.User).ToListAsync();
            messages = messages.Where(m => m.ThreadId == threadId).ToList();

            return messages;
        }

        //POST a new message
        [HttpPost("createmessage")]
        public async Task<ActionResult<string>> CreateMessage([FromBody] MessageModel messageToCreate, [FromQuery] string token)
        {
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

        [HttpPut("updatemessage")]
        public async Task<ActionResult> PutMessageAsync([FromBody] MessageDto message)
        {
            var dbMessage = await _dbContext.Messages.FindAsync(message.MessageId);
            if (dbMessage != null)
            {
                dbMessage.Message = message.Message;
                dbMessage.Edited = true;
                _dbContext.Messages.Update(dbMessage);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        //POST a new message
        [HttpPut("deletemarkmessage")]
        public async Task<ActionResult> MarkAsDeletedMessageAsync([FromBody] MessageDto message)
        {
            var dbMessage = await _dbContext.Messages.FindAsync(message.MessageId);
            if (dbMessage != null)
            {
                dbMessage.Deleted = true;
                _dbContext.Messages.Update(dbMessage);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        //[HttpGet]
        //public async Task<ActionResult<int>> GetThreadId()
        //{
        //    var threadID = await _dbContext.Threads.ThreadId;
        //}

    }
}
