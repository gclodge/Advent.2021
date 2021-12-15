namespace Advent.Solutions.Days
{
    public class Polymerization
    {
        public string Template { get; }

        Dictionary<string, string> Swaps = new();

        Dictionary<string, long> Counts = new();

        public Polymerization(IEnumerable<string> input)
        {
            Template = input.First();

            foreach (var i in Enumerable.Range(0, Template.Length - 1))
            {
                var str = Template.Substring(i, 2);
                Counts[str] = Counts.GetValueOrDefault(str) + 1;
            }

            foreach (var line in input.Skip(2))
            {
                var arr = line.Split(" -> ");
                Swaps.Add(arr[0], arr[1]);
            }
        }

        public long Solve(int steps = 10)
        {
            //< Apply all required steps
            foreach (int i in Enumerable.Range(0, steps)) Step();

            //< Generate the final histogram count
            return CalculateResult();
        }

        void Step()
        {
            this.Counts = Counts.SelectMany(GetSubstitutions)
                                .GroupBy(kvp => kvp.Key)
                                .ToDictionary(grp => grp.Key, grp => grp.Sum(g => g.Value));
        }

        Dictionary<string, long> GetSubstitutions(KeyValuePair<string, long> kvp)
        {
            return new Dictionary<string, long>()
            {
                { kvp.Key[0] + Swaps[kvp.Key], kvp.Value },
                { Swaps[kvp.Key] + kvp.Key[1], kvp.Value }
            };
        }

        long CalculateResult()
        {
            var results = Swaps.SelectMany(s => s.Key.ToCharArray())
                               .Distinct()
                               .Select(c => Counts.Sum(kvp => kvp.Key[1] == c ? kvp.Value : 0) +
                                            (c == Counts.First().Key[0] ? Counts.First().Value : 0));

            return results.Max() - results.Min();
        }
    }
}
