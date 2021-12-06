namespace Advent.Solutions.Days
{
    public class LanternFishSchool
    {
        public const int ResetValue = 6;
        public const int SplitValue = 8;

        public List<(int day, ulong count)> Counters { get; set; } = new();

        internal ulong[] FishCounter = new ulong[SplitValue + 1];

        public ulong Count => GetCount();

        public LanternFishSchool(IEnumerable<int> values)
        {
            foreach (var val in values)
            {
                FishCounter[val]++;
            }
        }

        public void SimulateDays(int days)
        {
            foreach (int i in Enumerable.Range(0, days))
            {
                FishCounter = GenerateNextHistogram(FishCounter);

                Counters.Add(new (i, GetCount()));
            }
        }

        ulong GetCount()
        {
            ulong total = 0;
            foreach (var count in FishCounter)
                total += count;

            return total;
        }

        static ulong[] GenerateNextHistogram(ulong[] curr)
        {
            ulong countNew = curr[0];
            var newHist = new ulong[SplitValue + 1];
            for (int i = 0; i < curr.Length - 1; i++)
            {
                newHist[i] = curr[i + 1];
            }
            newHist[SplitValue] = countNew;
            newHist[ResetValue] += countNew;
            return newHist;
        }
    }
}
