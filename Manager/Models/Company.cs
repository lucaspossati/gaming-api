namespace Application.Models
{
    public class Company
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime RegistrationDate { get; set; }

        public ICollection<Person> People { get; set; }
    }
}
