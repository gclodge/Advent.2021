using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Solutions.Days
{
    public class PassagePath
    {
        const string Start = "start";
        const string End = "end";

        bool PartOne = true;

        Dictionary<string, HashSet<string>> Connections = new();
        Dictionary<string, List<int>> Visits = new();

        public int PathCount { get; private set; } = 0;

        public PassagePath(IEnumerable<string> inputs)
        {
            //< Initialize connectivity
            foreach (var input in inputs)
            {
                var arr = input.Split('-');
                //< Add both (A, B) and (B, A) connections
                Add(arr[0], arr[1]);
                Add(arr[1], arr[0]);
            }

            //< Initialize the past visit counter for small caves
            foreach (var cave in Connections.Keys.Where(IsSmallCave))
            {
                Visits.Add(cave, new());
            }
        }

        public PassagePath SetPartTwo()
        {
            this.PartOne = false;
            return this;
        }

        public void Solve()
        {
            //< Starting at 'start' w/ depth of 1
            Traverse(Start, depth: 1);
        }

        void Add(string a, string b)
        {
            if (b == Start) return;
            if (a == End) return;

            if (!Connections.ContainsKey(a))
                Connections.Add(a, new HashSet<string>());

            Connections[a].Add(b);
        }

        void Traverse(string cave, int depth)
        {
            //< If we're in a small cave - record the depth of our visit
            if (IsSmallCave(cave))
                Visits[cave].Add(depth);

            foreach (var next in Connections[cave])
            {
                if (next == End)
                {
                    //< We've hit the end - log as valid path and move to next cave
                    PathCount += 1;
                    continue;
                }

                if (IsSmallCave(next))
                {
                    //< Check if we've visited this small cave before
                    if (Visits[next].Any())
                    {
                        //< If part one - can only visit once, bail!
                        if (PartOne)
                            continue;

                        //< If part two - bail if visited twice or more
                        if (Visits.Any(v => v.Value.Count > 1))
                            continue;
                    }
                }

                //< Continue into this next cave and search it out
                Traverse(next, depth + 1);

                //< Now that we're back, need to backtrack
                foreach (var key in Visits.Keys)
                {
                    //< This is done by omitting visits done beyond our depth
                    Visits[key] = Visits[key].Where(s => s <= depth)
                                             .ToList();
                }
            }
        }

        static bool IsSmallCave(string cave)
        {
            return char.IsLower(cave[0]);
        }

        /*
         * 

        /// <summary>
        /// Recursively try each cave next to this one, keeping track of the step counts we
        /// are at when we visit each cave. This allows us to backtrack by forgetting any caves
        /// we "visited in the future" after returning back from a recursive call
        /// </summary>
        /// <param name="cave"></param>
        /// <param name="steps">Number of steps taken to get here</param>
        void SearchFrom(string cave, int steps)
        {
            // if we've arrived at a small cave, record when we got here
            if (IsSmallCave(cave))
                _pastVisits[cave].Add(steps);

            // explore all valid paths outwards from here one at a time
            foreach (var next in _from[cave])
            {                

                // we have permission to explore this cave, so recursively call this again
                SearchFrom(next, steps + 1);

                // having returned from an arbitrarily long path, we have to undo the effects
                // of the path-taking by forgetting the visited status of any cave that we
                // visited after this older call of SearchFrom that we're now back in
                foreach (var k in _pastVisits.Keys)
                {
                    // keep only this and prior step counts
                    _pastVisits[k] = _pastVisits[k]
                        .Where(s => s <= steps)
                        .ToList();
                }
            }
        }

         */

    }
}
