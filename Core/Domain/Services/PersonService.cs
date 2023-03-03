

using api.Domain.Services.Interfaces;
using API.Domain.VM;
using Application.Models;
using AutoMapper;
using Core.Validator;
using Core.Validator.User;
using Data.Repository.Interface;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace api.Domain.Services
{
    public class PersonService : IPersonService
    {
        private readonly IMapper mapper;
        private readonly IPersonRepository personRepository;

        public PersonService(IMapper mapper, IPersonRepository personRepository)
        {
            this.mapper = mapper;
            this.personRepository = personRepository;
        }

        public async Task<IEnumerable<PersonVM>> Get()
        {
            var response = await personRepository.Get();

            return mapper.Map<List<PersonVM>>(response);
        }

        public async Task<IEnumerable<PersonVM>> GetWithFilters(string? fullName = null, string? phoneNumber = null, string? address = null)
        {
            var response = await personRepository.GetWithFilters(fullName, phoneNumber, address);

            return mapper.Map<List<PersonVM>>(response);
        }

        public async Task<PersonVM> GetWildCard()
        {
            var response = (await personRepository.Get()).ToList();
            var rng = new Random();
            var randomIndex = rng.Next(0, response.Count);

            return mapper.Map<PersonVM>(response[randomIndex]);
        }

        public async Task<PersonVM> Get(Guid id)
        {
            var response = await personRepository.Get(id);

            return mapper.Map<PersonVM>(response);
        }

        public async Task<PersonVM> Post(PersonVM model)
        {
            var validator = new CreatePersonValidator();

            ValidationResult results = validator.Validate(model);

            Validation.AddErrors(model, results);

            if (model.Errors != null && model.Errors.Count > 0) return model;

            model.Id = Guid.NewGuid();

            var vmToModel = mapper.Map<Person>(model);

            await personRepository.Post(vmToModel);

            return model;
        }

        public async Task<PersonVM> Put(PersonVM model)
        {
            var validator = new UpdatePersonValidator();

            ValidationResult results = validator.Validate(model);

            Validation.AddErrors(model, results);

            if (model.Errors != null && model.Errors.Count > 0) return model;

            await personRepository.Put(mapper.Map<Person>(model));

            return model;
        }

        public async Task<PersonVM> Delete(Guid id)
        {
            var model = await personRepository.Get(id);

            if (model == null) return null;

            await personRepository.Delete(id);

            return mapper.Map<PersonVM>(model); ;
        }
    }
}
