using api.Domain.Services.Interfaces;
using api.Domain.VM.Shared;
using Manager.VM.Company;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers
{
    [ApiController]
    [EnableCors("AllowAll")]
    [Route("v1/company")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService companyService;

        public CompanyController
        (
            ICompanyService companyService
        )
        {
            this.companyService = companyService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var response = await companyService.Get();
            if (response.Any())
            {
                return Ok(response);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post([FromBody] NewCompanyVM model)
        {
            var response = await companyService.Post(model);

            if (response.Errors != null)
            {
                return BadRequest(response);
            }
            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        }
        
    }
}
