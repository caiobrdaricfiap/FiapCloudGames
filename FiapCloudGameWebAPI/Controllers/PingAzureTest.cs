using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapCloudGameWebAPI.Application.Services
{
    internal class PingAzureTest
    {
    }
}
namespace FiapCloudGameWebAPI.Controllers
{
    [ApiController]
    [Route("ping")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Ping()
        {
            return Ok(new { message = "ping" });
        }
    }
}