using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RokuWebRemote.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RokuController : ControllerBase
    {
        private static List<string> _rokuDevices = new List<string>();

        public static void AddRokuDevice(string device)
        {
            if (!_rokuDevices.Contains(device))
            {
                _rokuDevices.Add(device);
            }
        }

        [HttpGet]
        public IActionResult GetRokuDevices()
        {
            return Ok(_rokuDevices);
        }
    }
}
