using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MSSWebAPI.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route(template: "api/v{version:apiVersion}/[controller]")]

    public abstract class APIConnectionController : ControllerBase
    {
      
    }
}
