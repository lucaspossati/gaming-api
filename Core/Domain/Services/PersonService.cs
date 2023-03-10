

using api.Domain.Services.Interfaces;
using Application.Models;
using AutoMapper;
using Core.Validator;
using Core.Validator.User;
using Data.Repository.Interface;
using FluentValidation.Results;
using Manager.VM.Person;
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

        public async Task<PersonVM> GetWildCard(int? index = null)
        {
            var randomIndex = 0;
            var response = (await personRepository.Get()).ToList();

            if(index == null)
                randomIndex = await GenerateRandomIndex();

            return mapper.Map<PersonVM>(index == null ? response[randomIndex] : response[index.Value]);
        }

        public PersonVM GetWildCardOption2()
        {
            var response = personRepository.GetWildCard();

            return mapper.Map<PersonVM>(response);
        }

        public async Task<PersonVM> Get(Guid id)
        {
            var response = await personRepository.Get(id);

            return mapper.Map<PersonVM>(response);
        }

        public async Task<PersonVM> Post(NewPersonVM model)
        {
            var validator = new CreatePersonValidator();

            ValidationResult results = validator.Validate(model);

            Validation.AddErrors(model, results);

            if (model.Errors != null && model.Errors.Count > 0) return mapper.Map<PersonVM>(model);

            var vmToModel = mapper.Map<Person>(model);

            vmToModel.Id = Guid.NewGuid();

            var person = await personRepository.Post(vmToModel);

            return mapper.Map<PersonVM>(person);
        }

        public async Task<PersonVM> Put(PersonVM model)
        {
            var validator = new UpdatePersonValidator();

            ValidationResult results = validator.Validate(model);

            Validation.AddErrors(model, results);

            var modelOld = await personRepository.Get(model.Id);

            if (modelOld == null)
            {
                model.AddError("Id not found", "Id");
            }

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

        public async Task<int> GenerateRandomIndex()
        {
            var response = (await personRepository.Get()).ToList();
            var rng = new Random();
            return rng.Next(0, response.Count);
        }
    }
}
