using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Suitsupply.Tailoring.Core.Cqrs;
using Suitsupply.Tailoring.Services.Alterations;
using Suitsupply.Tailoring.Web.Api.Controllers.Requests;
using Suitsupply.Tailoring.Web.Api.Controllers.Responses;

namespace Suitsupply.Tailoring.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlterationsController : ControllerBase
    {
        private readonly ILogger<AlterationsController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AlterationsController(ILogger<AlterationsController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<AlterationListResponse>> Get()
        {
            try
            {
                var query = new GetAlterationListQuery();
                var result = await _mediator.ExecuteAsync(query);

                var response = _mapper.Map<AlterationListResponse>(result);
                return Ok(response);
            }
            catch (Exception err)
            {
                _logger.LogError(err, "Failed to retrieve alteration list");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlterationDto>> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide correct id value");
            }
            
            try
            {
                var query = new GetAlterationByIdQuery(id);
                var result = await _mediator.ExecuteAsync(query);

                var response = _mapper.Map<AlterationDto>(result.Data);
                return Ok(response);
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
                var command = _mapper.Map<CreateAlterationCommand>(request);
                var result = await _mediator.ExecuteAsync(command);
                return Ok(result);
            }
            catch (Exception err)
            {
                _logger.LogError(err, "Failed to create alteration");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("{id}/pay")]
        public async Task<IActionResult> PayAlteration(int id)
        {
            try
            {
                var command = new PayAlterationCommand
                {
                    AlterationId = id
                };
                var result = await _mediator.ExecuteAsync(command);
                return Ok(result);
            }
            catch (Exception err)
            {
                _logger.LogError(err, "Failed to pay alteration");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost("{id}/done")]
        public async Task<IActionResult> DoneAlteration(int id)
        {
            try
            {
                var command = new DoneAlterationCommand
                {
                    AlterationId = id
                };
                var result = await _mediator.ExecuteAsync(command);
                return Ok(result);
            }
            catch (Exception err)
            {
                _logger.LogError(err, "Failed to pay alteration");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
        }
    }
}
