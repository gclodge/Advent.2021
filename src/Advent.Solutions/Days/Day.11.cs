namespace Advent.Solutions.Days
{
    using GridMap = Dictionary<int, Dictionary<int, int>>;

    public class OctopusGrid
    {
        const int FlashPointValue = 9;

        GridMap OctoMap;

        public int Flashed { get; private set; } = 0;

        public List<(int Step, int Flashed)> Counter = new();

        public OctopusGrid(IEnumerable<string> inputs)
        {
            OctoMap = new();

            foreach (int y in Enumerable.Range(0, inputs.Count()))
            {
                string item = inputs.ElementAt(y);
                foreach (int x in Enumerable.Range(0, item.Length))
                {
                    int val = int.Parse(item[x].ToString());
                    AddValue(val, x, y);
                }
            }
        }

        public (int flashCount, int synchStep) Solve()
        {
            int s100 = 0;

            int step = 1;
            while (!Synchronized())
            {
                SimulateStep();
                
                if (step == 100)
                {
                    s100 = Flashed;
                }

                step += 1;
            }

            return (s100, step - 1);
        }

        void SimulateStep()
        {
            var stack = new Stack<(int X, int Y)>();

            foreach (int y in OctoMap.Keys)
            {
                foreach (int x in OctoMap[y].Keys)
                {
                    int val = IncrementValue(x, y);
                    if (val > FlashPointValue)
                    {
                        SetValue(0, x, y);
                        Flashed++;
                        stack.Push(new(x, y));
                    }
                }
            }

            while (stack.Count > 0)
            {
                var curr = stack.Pop();
                foreach (var n in GetNeighbours(curr.X, curr.Y))
                {
                    if (OctoMap[n.Y][n.X] != 0)
                    {
                        int val = IncrementValue(n.X, n.Y);
                        if (val > FlashPointValue)
                        {
                            SetValue(0, n.X, n.Y);
                            Flashed++;
                            stack.Push(n);
                        }
                    }
                }
            }
        }

        bool Synchronized()
        {
            foreach (int y in OctoMap.Keys)
            {
                foreach (int x in OctoMap[y].Keys)
                {
                    if (OctoMap[y][x] != 0)
                        return false;
                }
            }
            return true;
        }

        void AddValue(int val, int x, int y)
        {
            if (!OctoMap.ContainsKey(y))
            {
                OctoMap.Add(y, new Dictionary<int, int>());
            }
            OctoMap[y].Add(x, val);
        }

        void SetValue(int val, int x, int y)
        {
            OctoMap[y][x] = val;
        }

        int IncrementValue(int x, int y)
        {
            if (OctoMap[y][x] >= 0)
            {
                OctoMap[y][x] += 1;
            }

            return OctoMap[y][x];
        }

        IEnumerable<(int X, int Y)> GetNeighbours(int x, int y)
        {
            var neigh = new (int X, int Y)[]
            {
                new(x - 1, y),
                new(x - 1, y + 1),
                new(x - 1, y - 1),
                new(x + 1, y),
                new(x + 1, y + 1),
                new(x + 1, y - 1),
                new(x, y - 1),
                new(x, y + 1)
            };

            return neigh.Where(n => OctoMap.ContainsKey(n.Y) && OctoMap[n.Y].ContainsKey(n.X));
        }
    }
}
