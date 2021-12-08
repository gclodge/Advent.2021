using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Solutions.Days
{
    internal class SevenSegmentValue
    {
        static readonly HashSet<int> UniqueSegmentCounts = new() { 2, 3, 4, 7 };

        public IEnumerable<string> Signals { get; set; }
        public IEnumerable<string> Outputs { get; set; }

        public Dictionary<string, int> SolutionMap { get; set; }

        public SevenSegmentValue(string str)
        {
            var arr = str.Split('|').Select(x => x.Split(' ')).ToArray();
            Signals = arr[0].Where(x => !string.IsNullOrEmpty(x)).ToList();
            Outputs = arr[1].Where(x => !string.IsNullOrEmpty(x)).ToList();
            SolutionMap = GetSolutionMap();
        }

        public int CountUniqueOutputValues()
        {
            int count = 0;
            foreach (var output in Outputs)
            {
                var hs = output.ToHashSet().Count();
                if (UniqueSegmentCounts.Contains(hs))
                {
                    count++;
                }
            }
            return count;
        }

        public int Solve()
        {
            string result = "";
            foreach (var output in Outputs)
            {
                var set = output.ToHashSet();
                var key = SolutionMap.Keys.Single(x => x.Count() == set.Count && x.All(c => set.Contains(c)));
                result += SolutionMap[key];
            }

            return int.Parse(result);            
        }

        //< Gross..
        Dictionary<string, int> GetSolutionMap()
        {
            var resMap = new Dictionary<string, int>();

            var map = Signals.GroupBy(sig => sig.Count())
                             .ToDictionary(x => x.Key, x => x.ToList());

            //< Can get the 1/4/7/8 from these - and from that solve the rest
            var seven = map[3].Single();

            resMap.Add(map[2].Single(), 1);
            resMap.Add(map[4].Single(), 4);
            resMap.Add(seven, 7);
            //< Only want the characters from 1,4,7 - not 8
            var knownChars = resMap.Keys.SelectMany(x => x.ToCharArray())
                                        .ToHashSet();

            resMap.Add(map[7].Single(), 8);

            //< Know 9 is a 6-segment number that has 1 left after removing the 'known' chars
            //< Know 2 is a 5-segment number that has 2 left after removing the 'known' chars
            var nine = Solve(map[6], knownChars, targetCount: 1);
            resMap.Add(nine, 9);
            var two = Solve(map[5], knownChars, targetCount: 2);
            resMap.Add(two, 2);

            //< Once that's done - we know we have 0,6 left in 6G, and 5,3 left in 5G
            var six = Solve(map[6], seven.ToHashSet(), targetCount: 4);
            resMap.Add(six, 6);
            var three = Solve(map[5], seven.ToHashSet(), targetCount: 2);
            resMap.Add(three, 3);

            resMap.Add(map[6].Single(s => !resMap.ContainsKey(s)), 0);
            resMap.Add(map[5].Single(s => !resMap.ContainsKey(s)), 5);

            return resMap;
        }

        string Solve(IEnumerable<string> vals, HashSet<char> known, int targetCount)
        {
            var curr = vals.Where(v => v.Count(c => !known.Contains(c)) == targetCount);
            return curr.Single();
        }
    }

    public class SevenSegmentSearch
    {
        IEnumerable<SevenSegmentValue> Segments { get; }

        public int CountUniqueOutputs => Segments.Sum(x => x.CountUniqueOutputValues());
        public int SumOfSolutions => Segments.Sum(x => x.Solve());

        public SevenSegmentSearch(IEnumerable<string> inputs)
        {
            Segments = inputs.Select(x => new SevenSegmentValue(x))
                             .ToList();
        }

    }
}
