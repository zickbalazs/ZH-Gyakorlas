using ZH.Gyakorlas.Feladat.Exceptions;

namespace ZH.Gyakorlas.Feladat.Classes
{
    public class PersonType : IComparable
    {
        int passportNumber;
        string name;
        int age;
        CountryType country;

        public int PassportNumber {
            get => passportNumber;
            set {
                if (value <= 999999 && value >= 100000){
                    passportNumber = value;
                }
                else throw new PersonalInfoException("Rossz igazolványszám");
            }
        }
        public string Name {
            get => name;
            set => name = value;
        }        
        public int Age {
            get => age;
            set => age = value;
        }
        public CountryType Country {
            get => country;
            set => country = value;
        }

        public PersonType(int passportNumber, string name, int age, CountryType country){
            PassportNumber = passportNumber;
            Name = name;
            Age = age;
            Country = country;
        }
        
        public PersonType(int passportNumber, int age, CountryType country) : this(passportNumber, "[REDACTED]", age, country)
        {}
        
        
        public int CompareTo(object? obj)
        {
            //  obj <- b; this <- a
            //  
            //  b == a; 0
            //  a > b; 1
            //  a < b; -1

            if (obj is string)
            {
                string b = (string)obj;
                return this.Name.CompareTo(b);
            }
            if (obj is PersonType)
            {
                PersonType other = (PersonType)obj;
                int nameOrder = this.Name.CompareTo(other.Name);
                if (nameOrder == 0)
                {
                    return this.PassportNumber.CompareTo(other.PassportNumber);
                }
                return nameOrder;
            }
            throw new ArgumentException("Rossz bemeneti paraméter");
        }

        public override string ToString()
        {
            return $"{Name}, {PassportNumber}, {Age}, {Country}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is PersonType)
            {
                return ((PersonType)obj).PassportNumber == PassportNumber;
            }
            return false;
        }

        public static PersonType Parse(string input)
        {
            if (input.Split(", ").Length != 4)
                throw new PersonalInfoException("Rossz bemeneti paraméter");

            PersonType type;
            string[] cols = input.Split(", ");
            CountryType country = Enum.Parse<CountryType>(cols[3]);
            type = new PersonType(int.Parse(cols[1]), cols[0], int.Parse(cols[2]), country);
            return type;
        }

    }
}