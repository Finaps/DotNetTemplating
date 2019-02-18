using Microsoft.AspNetCore.Mvc;

namespace MicroService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class HomeController : ControllerBase
  {
    [HttpGet]
    public ActionResult<string> GetAction()
    {
      return Ok("Hell");
    }

  }
}