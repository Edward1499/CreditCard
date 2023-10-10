using Application.Commands;
using Application.Handlers;
using Application.Queries;
using Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreditCard_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetClientListQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientResponse>> GetById(int id)
        {
            return Ok(await _mediator.Send(new GetClientByIdQuery(id)));
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateClientCommand command) 
        {
            try
            {
                await _mediator.Send(command);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateClientCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteClientCommand(id));
            return Ok();
        }
    }
}
