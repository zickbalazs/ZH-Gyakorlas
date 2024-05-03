using System.Data.Common;

namespace ZH.Gyakorlas.Feladat.Classes
{
    public class BorderCrossings
    {
        DailyCrossingAttempts[] daysOfActivity;
        public BorderCrossings(DailyCrossingAttempts[] days)
        {
            daysOfActivity = days;
        }

        public int HowManyPassed()
        {
            int count = 0;

            for (int i = 0; i < daysOfActivity.Length; i++)
            {
                count += daysOfActivity[i].Count((cross)=>cross.Successful);
            }

            return count;
        }

        public int MostPassedDay(int age)
        {
            int idx = -1;

            int[] successes = new int[daysOfActivity.Length];


            for (int i = 0; i < daysOfActivity.Length; i++)
            {
                successes[i] = new DailyCrossingAttempts(daysOfActivity[i].Select((cross)=>cross.Person.Age>=age))
                                                                          .Count((cross)=>cross.Successful);
            }

            for (int i = 1; i < successes.Length; i++)
            {
                if (successes[i] > successes[i-1])
                {
                    idx = i;
                }
            }

            return idx;
        }

        public double FailedAverageAge(CountryType ct)
        {
            double total = 0;
            double count = 0;

            for (int i = 0; i < daysOfActivity.Length; i++)
            {
                CrossingAttempt[] Failed = daysOfActivity[i].Select((cross)=>cross.Person.Country == ct && !cross.Successful);
                count += Failed.Length;
                for (int g = 0; g < Failed.Length; g++)
                {
                    total += Failed[g].Person.Age;
                }
            }

            return total / count;
        }

        private CrossingAttempt[] Intersection(CrossingAttempt[] one, CrossingAttempt[] other)
        {
            CrossingAttempt[] intersection = new CrossingAttempt[one.Length < other.Length ? one.Length : other.Length];
            int i = 1,
                j = 1,
                db = 0;
            while (i < one.Length && j < other.Length)
            {
                if (one[i].CompareTo(other[j]) > 0)
                {
                    i++;
                }
                else if (one[i].CompareTo(other[j]) < 0)
                {
                    j++;
                }
                else {
                    intersection[db] = one[i];
                    i++;
                    j++;
                    db++;
                }
            }
            Array.Resize(ref intersection, db);
            return intersection;
        }

        public CrossingAttempt[] TheDefinitionOfInsanity()
        {
            int count = 0;
            for (int i = 0; i < daysOfActivity.Length; i++)
            {
                count += daysOfActivity[i].Count((p)=>!p.Successful);
            }

            CrossingAttempt[] crossings = new CrossingAttempt[count];
            int trimmedLength = 0;
            for (int i = 0; i < daysOfActivity.Length - 1; i++)
            {
                trimmedLength += Intersection(daysOfActivity[i].Select((p)=>!p.Successful), daysOfActivity[i+1].Select((p)=>!p.Successful)).Length;
                crossings = Intersection(daysOfActivity[i].Select((p)=>!p.Successful), daysOfActivity[i+1].Select((p)=>!p.Successful));
            }

            return crossings;
        }
    }
}