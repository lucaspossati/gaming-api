

using api.Domain.Services.Interfaces;
using API.Domain.VM;
using Application.Models;
using AutoMapper;
using Core.Validator;
using Core.Validator.User;
using Data.Repository.Interface;
using FluentValidation.Results;

namespace api.Domain.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IMapper mapper;
        private readonly ICompanyRepository companyRepostiory;

        public CompanyService(IMapper mapper, ICompanyRepository companyRepostiory)
        {
            this.mapper = mapper;
            this.companyRepostiory = companyRepostiory;
        }

        public async Task<IEnumerable<CompanyVM>> Get()
        {
            var response = await companyRepostiory.Get();
            var responseVM = mapper.Map<List<CompanyVM>>(response);
            responseVM.ForEach(x => x.setNumberOfPersons(x.People != null ? x.People.Count : null));

            return responseVM;
        }

        public async Task<CompanyVM> Get(Guid id)
        {
            var response = await companyRepostiory.Get(id);

            return mapper.Map<CompanyVM>(response);
        }

        public async Task<CompanyVM> Post(CompanyVM model)
        {
            var validator = new CreateCompanyValidator();

            ValidationResult results = validator.Validate(model);

            Validation.AddErrors(model, results);

            if (!string.IsNullOrEmpty(model.Name))
            {
                var companyByName = await companyRepostiory.Get(model.Name);

                if (companyByName != null)
                {
                    model.AddError("Name already registered", "Name");
                }
            }

            if (model.Errors != null && model.Errors.Count > 0) return model;

            model.Id = Guid.NewGuid();
            model.RegistrationDate = DateTime.UtcNow;

            var vmToModel = mapper.Map<Company>(model);

            await companyRepostiory.Post(vmToModel);

            return model;
        }

        public async Task<CompanyVM> Put(CompanyVM model)
        {
            var validator = new UpdateCompanyValidator();

            ValidationResult results = validator.Validate(model);

            Validation.AddErrors(model, results);

            if (!string.IsNullOrEmpty(model.Name))
            {
                var companyByName = await companyRepostiory.Get(model.Name, model.Id);

                if (companyByName != null)
                {
                    model.AddError("Name already registered", "Name");
                }
            }

            if (model.Errors != null && model.Errors.Count > 0) return model;

            var oldModel = await companyRepostiory.Get(model.Id);
            model.RegistrationDate = oldModel.RegistrationDate;

            await companyRepostiory.Put(mapper.Map<Company>(model));

            return model;
        }

        public async Task<CompanyVM> Delete(Guid id)
        {
            var model = await companyRepostiory.Get(id);

            if (model == null) return null;

            await companyRepostiory.Delete(id);

            return mapper.Map<CompanyVM>(model);
        }
    }
}
