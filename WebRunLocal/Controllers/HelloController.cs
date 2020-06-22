using System.Net.Http;
using System.Text;
using System.Web.Http;
using WebRunLocal.Filters;

namespace WebRunLocal.Controllers
{
    /// <summary>
    ///  Hello controller.
    /// </summary>
    [RoutePrefix("api/hello")]
    public class HelloController : ApiController
    {
        [Route("getecho")]
        [HttpGet]
        [ActionFilter]
        public IHttpActionResult GetEcho(string name)
        {
            return Json(new {Name = name, Message = $"Hello, {name}"});
        }

        [Route("postecho")]
        [HttpPost]
        [ActionFilter]
        public HttpResponseMessage SendMessage([FromBody]object message)
        {
            HttpResponseMessage resonse = new HttpResponseMessage
            {
                Content = new StringContent(message.ToString(), Encoding.UTF8, "application/json")
            };
            return resonse;
        }
    }
}
