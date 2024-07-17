using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public BookController()
        {

        }

        [Route("TestMethod")]
        [HttpGet]
        public async Task<IActionResult> TestMethod()
        {
            try
            {
                return Ok(1);
            }
            catch (Exception ex)
            {
                return Ok(0);
            }

        }
    }
}

