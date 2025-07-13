using Microsoft.AspNetCore.Mvc;
using Doc1_WebAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace Doc1_WebAPI.Controllers
{
    [ApiController]
    [Route("Emp")]
    public class QuotesController : ControllerBase
    {
        private static List<Quote> quotes = new List<Quote>
        {
            new Quote { Id = 1, Text = "Be yourself; everyone else is already taken.", Author = "Oscar Wilde" },
            new Quote { Id = 2, Text = "In the middle of every difficulty lies opportunity.", Author = "Albert Einstein" }
        };

        // GET /Emp
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Quote>> Get()
        {
            return Ok(quotes);
        }

        // POST /Emp
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult AddQuote([FromBody] Quote newQuote)
        {
            newQuote.Id = quotes.Max(q => q.Id) + 1;
            quotes.Add(newQuote);
            return Ok(newQuote);
        }
    }
}
