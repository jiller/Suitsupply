using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Suitsupply.Tailoring.Core.Cqrs;
using Suitsupply.Tailoring.Services.Alterations;
using Suitsupply.Tailoring.Web.Api.Requests;

namespace Suitsupply.Tailoring.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlterationsController : ControllerBase
    {
        private readonly ILogger<AlterationsController> _logger;
        private readonly IMediator _mediator;

        public AlterationsController(ILogger<AlterationsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide correct id value");
            }
            
            try
            {
                var query = new GetAlterationByIdQuery(id);
                var result = await _mediator.ExecuteAsync(query);

                return Ok(result.Data);
            }
            catch (Exception err)
            {
                _logger.LogError(err, $"Failed to retrieve alteration '{id}'");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AlterationRequest request)
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
                        ShortenSleevesLeft = request.ShortenSleeves.Left,
                        ShortenSleevesRight = request.ShortenSleeves.Right,
                        ShortenTrousersLeft = request.ShortenTrousers.Left,
                        ShortenTrousersRight = request.ShortenTrousers.Right,
                        CustomerId = request.CustomerId
                    }
                };

                var result = await _mediator.ExecuteAsync(command);
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
    }
}
