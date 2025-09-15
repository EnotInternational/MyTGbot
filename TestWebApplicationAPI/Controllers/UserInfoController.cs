using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TgBotClassLibrary;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestWebApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [HttpGet("id={id}")]
        public ActionResult<UserInfo> Get(long id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                IQueryable<UserInfo> set = context.Users.Where(user => user.Id == id);
                if (set.Count() <= 0)
                    return new NotFoundObjectResult($"User with id {id} not found");

                UserInfo user = set.First();

                if (user is not { })
                    return new NotFoundObjectResult($"User with id {id} not found");

                return user;
            }
        }
        [HttpGet("name={name}")]
        public ActionResult<UserInfo> Get(string name)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                IQueryable<UserInfo> set = context.Users.Where(user => user.Name == name);
                if (set.Count() <= 0)
                    return new NotFoundObjectResult($"User with name {name} not found");

                UserInfo user = set.First();

                if (user is not { })
                    return new NotFoundObjectResult($"User with name {name} not found");

                return user;
            }
        }
        [HttpPost]
        public void Post([FromBody] UserInfo value)
        {
            using (ApplicationContext context = new ApplicationContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Users.Add(value);
                    int saved = context.SaveChanges();
                    transaction.Commit();
                    Console.WriteLine("Saved sets:" + saved);
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
