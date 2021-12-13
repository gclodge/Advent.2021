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
    }
}
