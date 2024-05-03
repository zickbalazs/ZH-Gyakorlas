using ZH.Gyakorlas.Feladat.Classes;

namespace ZH.Gyakorlas.Feladat
{
    class Program 
    {
        static void Main(string[] args)
        {
            Random rngEsus = new Random();

            BorderCrossings crossings = GenerateData(7, ref rngEsus);

            crossings.TheDefinitionOfInsanity();
        }



        static BorderCrossings GenerateData(int days, ref Random rnd)
        {   
            DailyCrossingAttempts[] daily = new DailyCrossingAttempts[days];
            for (int i = 0; i < daily.Length; i++)
            {
                daily[i] = GenerateDataForOneDay(rnd.Next(10,50), ref rnd);
            }
            return new BorderCrossings(daily);
        }

        static DailyCrossingAttempts GenerateDataForOneDay(int numberOfAttempts, ref Random rnd)
        {
            CrossingAttempt[] attempts = new CrossingAttempt[numberOfAttempts];
            for (int i = 0; i < numberOfAttempts; i++)
            {
                attempts[i] = new CrossingAttempt(new PersonType(rnd.Next(100000, 1000000), rnd.Next(14, 92), (CountryType)rnd.Next(0,6)), rnd.Next(0,2) == 1);
            }
            return new DailyCrossingAttempts(attempts);
        }
    }
}