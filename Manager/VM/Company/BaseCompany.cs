using api.Domain.VM.Shared;
using Manager.VM.Person;

namespace Manager.VM.Company
{
    public abstract class BaseCompany : BaseViewModel, ICloneable
    {
        public BaseCompany()
        {
            setNumberOfPersons(Persons != null ? Persons.Count : 0);
        }

        public string Name { get; set; }

        public int NumberOfPersonsLinked { get; private set; }

        public DateTime RegistrationDate { get; set; }

        public ICollection<PersonVM>? Persons { get; set; }

        public void setNumberOfPersons(int numberOfPersons)
        {
            NumberOfPersonsLinked = numberOfPersons;
        }

        public object Clone()
        {
            return (CompanyVM)MemberwiseClone();
        }

        public object CloneWithDependency()
        {
            var company = (CompanyVM)MemberwiseClone();

            var people = new List<PersonVM>();
            company.Persons.ToList().ForEach(x => people.Add((PersonVM)x.Clone()));
            company.Persons = people;

            return company;
        }

        public CompanyVM TypedClone()
        {
            return (CompanyVM)Clone();
        }

        public CompanyVM TypedCloneDependency()
        {
            return (CompanyVM)CloneWithDependency();
        }
    }
}