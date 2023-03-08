using api.Domain.Services.Interfaces;
using api.Domain.VM.Shared;
using API.Domain.VM;
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
        public async Task<BaseResponse<IEnumerable<PersonVM>>> GetWithFilter([FromQuery] string? fullName = null,
            [FromQuery] string? phoneNumber = null, [FromQuery] string? address = null)
        {
            var response = await personService.GetWithFilters(fullName, phoneNumber, address);

            return new BaseResponse<IEnumerable<PersonVM>>()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Success to list people",
                Success = true,
                Data = response
            };
        }

        [HttpGet]
        [Route("wild-card")]
        public async Task<BaseResponse<PersonVM>> GetWildCard()
        {
            var response = await personService.GetWildCard();

            if (response == null)
            {
                return new BaseResponse<PersonVM>()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "No registered person",
                    Success = false,
                    Data = null
                };
            }

            return new BaseResponse<PersonVM>()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Succes getting random person",
                Success = true,
                Data = response
            };
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<BaseResponse<PersonVM>> Get([FromRoute] Guid id)
        {
            var response = await personService.Get(id);

            if (response == null)
            {
                return new BaseResponse<PersonVM>()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Person not found",
                    Success = false,
                    Data = null
                };
            }

            return new BaseResponse<PersonVM>()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Succes to get person",
                Success = true,
                Data = response
            };
        }

        [HttpPost]
        [Route("")]
        public async Task<BaseResponse<PersonVM>> Post([FromBody] PersonVM model)
        {
            var response = await personService.Post(model);

            if (response.Errors != null)
            {
                return new BaseResponse<PersonVM>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Error to create person",
                    Success = false,
                    Data = model
                };
            }

            return new BaseResponse<PersonVM>()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Success to create person",
                Success = true,
                Data = response
            };
        }

        [HttpPut]
        [Route("")]
        public async Task<BaseResponse<PersonVM>> Put([FromBody] PersonVM model)
        {
            var response = await personService.Put(model);

            if (response.Errors != null)
            {
                return new BaseResponse<PersonVM>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Error to update person",
                    Success = false,
                    Data = model
                };
            }

            return new BaseResponse<PersonVM>()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Success to update person",
                Success = true,
                Data = model
            };
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<BaseResponse<object>> Delete([FromRoute] Guid id)
        {
            var model = await personService.Delete(id);

            if (model == null)
            {
                return new BaseResponse<object>()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Person not found",
                    Success = false,
                    Data = null
                };
            }

            return new BaseResponse<object>()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Success to delete person",
                Success = true,
                Data = null
            };
        }
    }
}
