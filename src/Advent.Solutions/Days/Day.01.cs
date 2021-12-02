
namespace Advent.Solutions.Days
{
    public class SonarSweep
    {
        public List<int> Values { get; private set; }
        public List<int> SumWindows { get; private set; }

        public SonarSweep(IEnumerable<int> readings)
        {
            this.Values = readings.ToList();
            this.SumWindows = CalculateSumWindows(readings).ToList();
        }

        public int CountIncreasing()
        {
            return CountIncreasingValues(Values);
        }

        public int CountIncreasingWindows()
        {
            return CountIncreasingValues(SumWindows);
        }

        static int CountIncreasingValues(IEnumerable<int> vals)
        {
            int count = 0;
            for (int i = 1; i < vals.Count(); i++)
            {
                if (vals.ElementAt(i) > vals.ElementAt(i - 1))
                    count++;
            }
            return count;
        }

        static IEnumerable<int> CalculateSumWindows(IEnumerable<int> vals, int windowSize = 3)
        {
            var res = new List<int>();
            int count = vals.Count();

            for (int i = 0; i < count; i++)
            {
                var curr = vals.Skip(i).Take(windowSize);

                if (curr.Count() == windowSize)
                {
                    res.Add(curr.Sum());
                }
            }
            return res;
        }
    }
}
