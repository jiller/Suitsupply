using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Suitsupply.Tailoring.Web.Api.Requests;

namespace Suitsupply.Tailoring.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlterationsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void ChangeAlteration([FromBody] AlterationRequest request)
        {
        }

        [HttpPut("{id}")]
        public void CreateAlteration(int id, [FromBody] AlterationRequest request)
        {
        }

        [HttpDelete("{id}")]
        public void CancelAlteration(int id)
        {
        }
    }
}
