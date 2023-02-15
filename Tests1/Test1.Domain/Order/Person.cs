using Test1.Domain.Base;

namespace Test1.Domain.Order
{
    public class Person : Entity
    {
        private string? salutation;

        public DateTime BirthDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? Salutation { get => string.IsNullOrEmpty(salutation) ? string.Empty : Salutation + " "; set => salutation = value; }

        public string FullName => $"{Salutation}{FirstName} {LastName}";

        public bool IsU26 => DateTime.Now.AddYears(-26) < BirthDate;
    }
}