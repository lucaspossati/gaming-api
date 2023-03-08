using Bogus;
using FakeData.PersonData;
using Manager.VM.Company;

namespace FakeData.CompanyData
{
    public class NewCompanyVMFaker : Faker<NewCompanyVM>
    {
        public NewCompanyVMFaker()
        {
            RuleFor(x => x.Name, prop => prop.Company.CompanyName());
            RuleFor(x => x.NumberOfPersonsLinked, prop => prop.Random.Number(0, 10));
            RuleFor(x => x.RegistrationDate, prop => prop.Person.DateOfBirth);
        }
    }
}
