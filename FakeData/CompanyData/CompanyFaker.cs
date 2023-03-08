using Application.Models;
using Bogus;
using FakeData.PersonData;
using Manager.VM.Company;

namespace FakeData.CompanyData
{
    public class CompanyFaker : Faker<Company>
    {
        public CompanyFaker()
        {
            RuleFor(x => x.Id, prop => new Faker().Random.Guid());
            RuleFor(x => x.Name, prop => prop.Company.CompanyName());
            RuleFor(x => x.RegistrationDate, prop => prop.Date.Past());
        }
    }
}
