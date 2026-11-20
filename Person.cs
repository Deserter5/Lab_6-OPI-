using System;

namespace Lab7_Variant2
{
    public class Person
    {
        private string firstName;
        private string lastName;
        private DateTime birthDate;

        public Person(string firstName, string lastName, DateTime birthDate)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.birthDate = birthDate;
        }

        public Person() : this("Іван", "Іваненко", new DateTime(2000, 1, 1))
        {
        }

        public string FirstName
        {
            get => firstName;
            set => firstName = value;
        }

        public string LastName
        {
            get => lastName;
            set => lastName = value;
        }

        public DateTime BirthDate
        {
            get => birthDate;
            set => birthDate = value;
        }

        public int BirthYear
        {
            get => birthDate.Year;
            set => birthDate = new DateTime(value, birthDate.Month, birthDate.Day);
        }

        public override string ToString()
        {
            return $"{firstName} {lastName}, нар. {birthDate:dd.MM.yyyy}";
        }

        public virtual string ToShortString()
        {
            return $"{firstName} {lastName}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Person other)
            {
                return firstName == other.firstName &&
                       lastName == other.lastName &&
                       birthDate == other.birthDate;
            }
            return false;
        }

        public static bool operator ==(Person p1, Person p2)
        {
            if (ReferenceEquals(p1, p2)) return true;
            if (p1 is null || p2 is null) return false;
            return p1.Equals(p2);
        }

        public static bool operator !=(Person p1, Person p2)
        {
            return !(p1 == p2);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (firstName?.GetHashCode() ?? 0);
                hash = hash * 23 + (lastName?.GetHashCode() ?? 0);
                hash = hash * 23 + birthDate.GetHashCode();
                return hash;
            }
        }

        public virtual object DeepCopy()
        {
            return new Person(firstName, lastName, birthDate);
        }
    }
}