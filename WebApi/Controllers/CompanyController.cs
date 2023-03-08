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

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<BaseResponse<CompanyVM>> Get([FromRoute] Guid id)
        {
            var response = await companyService.Get(id);

            if (response == null)
            {
                return new BaseResponse<CompanyVM>()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Company not found",
                    Success = false,
                    Data = null
                };
            }

            return new BaseResponse<CompanyVM>()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Succes to get company",
                Success = true,
                Data = response
            };
        }

        [HttpPost]
        [Route("")]
        public async Task<BaseResponse<CompanyVM>> Post([FromBody] CompanyVM model)
        {
            var response = await companyService.Post(model);

            if (response.Errors != null)
            {
                return new BaseResponse<CompanyVM>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Error to create company",
                    Success = false,
                    Data = model
                };
            }

            return new BaseResponse<CompanyVM>()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Success to create company",
                Success = true,
                Data = response
            };
        }

        [HttpPut]
        [Route("")]
        public async Task<BaseResponse<CompanyVM>> Put([FromBody] CompanyVM model)
        {
            var response = await companyService.Put(model);

            if (response.Errors != null)
            {
                return new BaseResponse<CompanyVM>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Error to update company",
                    Success = false,
                    Data = model
                };
            }

            return new BaseResponse<CompanyVM>()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Success to update company",
                Success = true,
                Data = model
            };
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<BaseResponse<object>> Delete([FromRoute] Guid id)
        {
            var model = await companyService.Delete(id);

            if (model == null)
            {
                return new BaseResponse<object>()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Company not found",
                    Success = false,
                    Data = null
                };
            }

            return new BaseResponse<object>()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Success to delete company",
                Success = true,
                Data = null
            };
        }
    }
}
