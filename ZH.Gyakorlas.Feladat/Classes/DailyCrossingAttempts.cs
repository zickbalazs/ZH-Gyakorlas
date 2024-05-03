using System.ComponentModel.Design;
using System.Net.Mail;
using System.Threading.Tasks.Dataflow;

namespace ZH.Gyakorlas.Feladat.Classes
{
    public class DailyCrossingAttempts
    {
        CrossingAttempt[] attempts;

        private bool IsSorted()
        {
            for (int i = 1; i < attempts.Length; i++)
            {
                if (attempts[i].CompareTo(attempts[i-1]) < 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void Sort()
        {
            int i = attempts.Length - 1;
            while (i >= 1)
            {
                int idx = 0;
                for (int g = 0; g < i - 1; g++)
                {
                    if (attempts[g].CompareTo(attempts[g+1]) > 0)
                    {
                        (attempts[g], attempts[g+1]) = (attempts[g+1], attempts[g]);
                        idx = g;
                    }
                }
                i = idx;
            }             
        }
        public DailyCrossingAttempts(CrossingAttempt[] attempts)
        {
            this.attempts = attempts;
            if (!IsSorted())
            {
                Sort();
            }
        }
        public DailyCrossingAttempts(string[] attempts)
        {
            this.attempts = new CrossingAttempt[attempts.Length];

            for (int i = 0; i < attempts.Length; i++)
            {
                this.attempts[i] = CrossingAttempt.Parse(attempts[i]);
            }

            if (!IsSorted())
                Sort();
        }

        public bool PartOf(DailyCrossingAttempts other)
        {
            int i = 0,
                j = 0;
            
            while (i < this.attempts.Length && j < other.attempts.Length && attempts[i].CompareTo(other.attempts[j]) >= 0)
            {
                if (this.attempts[i].CompareTo(other.attempts[j])==0){
                    i++;
                }
                j++;
            }
            return i > attempts.Length;
        }

        public int FindPerson(string s)
        {
            int left = 0,
                right = attempts.Length - 1,
                center = (left+right) / 2;

            while (left <= right && attempts[center].CompareTo(s) != 0)
            {
                if (attempts[center].CompareTo(s) > 0){
                    right = center - 1;
                }
                else {
                    left = center + 1;
                }
                center = (left + right) / 2;
            }

            if (left <= right) return attempts[center].Person.PassportNumber;
            
            return -1;
        }

        public int Count(Predicate<CrossingAttempt> predicate)
        {
            int count = 0;

            for (int i = 0; i < attempts.Length; i++)
            {
                if (predicate(attempts[i]))
                    count++;
            }

            return count;
        }

        public CrossingAttempt[] Select(Predicate<CrossingAttempt> predicate)
        {
            CrossingAttempt[] selected = new CrossingAttempt[attempts.Length];
            int db = 0;
            for (int i = 0; i < attempts.Length; i++)
            {
                if (predicate(attempts[i]))
                {
                    selected[db] = attempts[i];
                    db++;
                }
            }

            Array.Resize(ref selected, db);

            return selected;
        }

        public double AverageAge(Predicate<CrossingAttempt> predicate)
        {
            double total = 0,
                   count = 0;

            for (int i = 0; i < attempts.Length; i++)
            {
                if (predicate(attempts[i]))
                {
                    total += attempts[i].Person.Age;
                    count++;
                }                
            }

            return total / count;
        }

        public CrossingAttempt OldestSuchPerson(Predicate<CrossingAttempt> predicate)
        {
            int age = int.MinValue;
            int idx = -1;

            for (int i = 0; i < attempts.Length; i++)
            {
                if (predicate(attempts[i]) && attempts[i].Person.Age > age){
                    age = attempts[i].Person.Age;
                    idx = i;
                }
            }

            return attempts[idx];
        }

    }
}