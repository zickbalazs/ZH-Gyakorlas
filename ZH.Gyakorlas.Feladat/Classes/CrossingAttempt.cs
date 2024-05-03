namespace ZH.Gyakorlas.Feladat.Classes
{
    public class CrossingAttempt : IComparable
    {
        public PersonType Person {
            get;
            private set;
        }
        
        public bool Successful {
            get;
            private set;
        }

        public CrossingAttempt(PersonType person, bool success){
            Person = person;
            Successful = success;
        }


        //input: Jorji Costava, 123456, 55, Obristan - false
        //[] : split[0], [] : split[1]


        public static CrossingAttempt Parse(string input)
        {
            string[] split = input.Split(" - ");

            string personalInfo = split[0];
            bool success = bool.Parse(split[1]);

            return new CrossingAttempt(PersonType.Parse(personalInfo), success);
        }


        public int CompareTo(object? obj)
        {
            if (obj is string)
            {
                return this.Person.CompareTo(obj);
            }
            if (obj is CrossingAttempt)
            {
                return this.Person.CompareTo(((CrossingAttempt)obj).Person);
            }
            throw new ArgumentException("Rossz bemeneti paraméter");
        }
    }
}