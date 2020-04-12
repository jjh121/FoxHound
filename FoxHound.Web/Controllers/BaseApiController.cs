using Microsoft.AspNetCore.Mvc;

namespace FoxHound.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
    }
}