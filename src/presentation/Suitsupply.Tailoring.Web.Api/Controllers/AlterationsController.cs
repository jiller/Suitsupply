using System;
using System.Collections.Generic;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Suitsupply.Tailoring.Core;
using Suitsupply.Tailoring.Services.Alterations;
using Suitsupply.Tailoring.Web.Api.Requests;

namespace Suitsupply.Tailoring.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlterationsController : ControllerBase
    {
        private readonly ILogger<AlterationsController> _logger;
        private readonly ICommandHandler<CreateAlterationCommand, NewAlteration> _createAlterationHandler;

        public AlterationsController(ILogger<AlterationsController> logger,
            ICommandHandler<CreateAlterationCommand, NewAlteration> createAlterationHandler)
        {
            _logger = logger;
            _createAlterationHandler = createAlterationHandler;
        }
        
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
        public IActionResult Post([FromBody] AlterationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid alteration");
            }

            try
            {
                var command = new CreateAlterationCommand
                {
                    Alteration = new NewAlteration
                    {
                        ShortenSleeves = request.ShortenSleeves,
                        ShortenTrousers = request.ShortenTrousers
                    }
                };

                var result = _createAlterationHandler.Handle(command);
                return Ok(result);
            }
            catch (Exception err)
            {
                _logger.LogError(err, "Failed to create alteration");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] AlterationRequest request)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
