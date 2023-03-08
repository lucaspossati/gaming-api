﻿using Bogus;
using FakeData.CompanyData;

namespace FakeData.PersonData
{
    public class PersonFaker : Faker<Application.Models.Person>
    {
        public PersonFaker()
        {
            RuleFor(x => x.Id, prop => new Faker().Random.Guid());
            RuleFor(x => x.FullName, prop => prop.Person.FullName);
            RuleFor(x => x.PhoneNumber, prop => prop.Person.Phone);
            RuleFor(x => x.Address, prop => prop.Person.Address.Street);
            RuleFor(x => x.CompanyId, prop => new Faker().Random.Guid());
        }
    }
}
