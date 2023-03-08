using API.Domain.VM;
using Bogus;
using FakeData.CompanyData;

namespace FakeData.PersonData
{
    public class PersonVMFaker : Faker<PersonVM>
    {
        public PersonVMFaker()
        {
            RuleFor(x => x.Id, prop => new Faker().Random.Guid());
            RuleFor(x => x.FullName, prop => prop.Person.FullName);
            RuleFor(x => x.PhoneNumber, prop => prop.Person.Phone);
            RuleFor(x => x.Address, prop => prop.Person.Address.Street);
            RuleFor(x => x.CompanyId, prop => new Faker().Random.Guid());
            RuleFor(p => p.Company, _ => new CompanyVMFaker().Generate());
        }
    }
}
