using api.Domain.Services.Interfaces;
using api.Domain.VM.Shared;
using Manager.VM.Person;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers
{
    [ApiController]
    [EnableCors("AllowAll")]
    [Route("v1/person")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService personService;

        public PersonController
        (
            IPersonService personService
        )
        {
            this.personService = personService;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> Get()
        {
            var response = await personService.Get();

            if (response.Any())
            {
                return Ok(response);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetWithFilters([FromQuery] string? fullName = null,
            [FromQuery] string? phoneNumber = null, [FromQuery] string? address = null)
        {
            var response = await personService.GetWithFilters(fullName, phoneNumber, address);

            if (response.Any())
            {
                return Ok(response);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("wild-card")]
        public async Task<IActionResult> GetWildCard()
        {
            var response = await personService.GetWildCard();

            if (response != null)
            {
                return Ok(response);
            }
            return NoContent();
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post([FromBody] NewPersonVM model)
        {
            var response = await personService.Post(model);

            if (response.Errors != null)
            {
                return BadRequest(response);
            }
            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Put([FromBody] PersonVM model)
        {
            var response = await personService.Put(model);

            if (response == null)
            {
                return NotFound();
            }
            return Ok(model);
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await personService.Delete(id);

            if (response == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
