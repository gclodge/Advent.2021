namespace Advent.Solutions.Days
{
    using GridMap = Dictionary<int, Dictionary<int, int>>;

    internal record Basin(int Index, int Size);
    internal record LowPoint(int X, int Y, int Risk);

    public class VolcanicHeightMap
    {
        GridMap HeightMap;
        GridMap BasinMap;

        const int DefaultBasinGroup = -1;
        const int NonBasinGroup = -2;

        List<LowPoint> LowPoints;
        List<Basin> Basins;

        public int LowPointCount => LowPoints.Count();
        public int LowPointRisk => LowPoints.Sum(x => x.Risk);

        public int BasinCount => Basins.Count();
        public int LargestBasinProduct => GetLargestBasinProduct();

        public VolcanicHeightMap(IEnumerable<string> inputs)
        {
            LowPoints = new List<LowPoint>();
            Basins = new List<Basin>();

            HeightMap = new GridMap();
            BasinMap = new GridMap();

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

        public VolcanicHeightMap GetLowPointRisk()
        {
            foreach (int y in HeightMap.Keys)
            {
                foreach (int x in HeightMap[y].Keys)
                {
                    if (IsLowPoint(x, y))
                    {
                        LowPoints.Add(new LowPoint(x, y, GetRisk(x, y)));
                    }
                }
            }

            return this;
        }

        public VolcanicHeightMap GenerateBasinGroups()
        {
            for (int i = 0; i < LowPoints.Count; i++)
            {
                var basin = GenerateBasin(LowPoints[i], i);
                Basins.Add(basin);
            }

            return this;
        }

        void AddValue(int val, int x, int y)
        {
            if (!HeightMap.ContainsKey(y))
            {
                HeightMap.Add(y, new Dictionary<int, int>());
                BasinMap.Add(y, new Dictionary<int, int>());
            }
                
            HeightMap[y].Add(x, val);
            BasinMap[y].Add(x, val == 9 ? NonBasinGroup : DefaultBasinGroup);
        }

        bool IsLowPoint(int x, int y)
        {
            int currVal = GetValue(x, y);
            var values = GetNeighbours(x, y)
                            .Select(n => GetValue(n.X, n.Y));

            return values.All(v => v > currVal);
        }

        IEnumerable<(int X, int Y)> GetNeighbours(int x, int y)
        {
            var neigh = new (int X, int Y)[]
            {
                new(x - 1, y),
                new(x + 1, y),
                new(x, y - 1),
                new(x, y + 1)
            };

            return neigh.Where(n => HeightMap.ContainsKey(n.Y) && HeightMap[n.Y].ContainsKey(n.X));
        }

        int GetValue(int x, int y)
        {
            return HeightMap[y][x];
        }

        int GetRisk(int x, int y)
        {
            return 1 + GetValue(x, y);
        }

        int GetBasin(int x, int y)
        {
            return BasinMap[y][x];
        }

        void SetBasin(int x, int y, int group)
        {
            BasinMap[y][x] = group;
        }

        Basin GenerateBasin(LowPoint lp, int group)
        {
            var stack = new Stack<(int X, int Y)>();
            stack.Push(new(lp.X, lp.Y));

            while(stack.Count > 0)
            {
                var pos = stack.Pop();
                SetBasin(pos.X, pos.Y, group);

                int currVal = GetValue(pos.X, pos.Y);
                var neighs = GetNeighbours(pos.X, pos.Y);
                foreach (var neigh in neighs)
                {
                    if (GetBasin(neigh.X, neigh.Y) == DefaultBasinGroup
                           && GetValue(neigh.X, neigh.Y) > currVal)
                    {
                        SetBasin(pos.X, pos.Y, group);
                        stack.Push(neigh);
                    }
                }
            }

            int count = BasinMap.Sum(y => y.Value.Count(v => v.Value == group));
            return new Basin(group, count);
        }

        int GetLargestBasinProduct()
        {
            var sizes = Basins.OrderByDescending(b => b.Size).Take(3).Select(b => b.Size);
            return sizes.Aggregate((total, next) => total * next);
        }
    }
}
