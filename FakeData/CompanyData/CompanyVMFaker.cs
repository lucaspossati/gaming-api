using API.Domain.VM;
using Bogus;
using FakeData.PersonData;

namespace FakeData.CompanyData
{
    public class CompanyVMFaker : Faker<CompanyVM>
    {
        public CompanyVMFaker()
        {
            RuleFor(x => x.Id, prop => new Faker().Random.Guid());
            RuleFor(x => x.Name, prop => prop.Company.CompanyName());
            RuleFor(x => x.NumberOfPersonsLinked, prop => prop.Random.Number(0, 10));
            RuleFor(x => x.RegistrationDate, prop => prop.Person.DateOfBirth);
        }
    }
}
