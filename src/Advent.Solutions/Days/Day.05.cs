namespace Advent.Solutions.Days
{
    internal record VentPosition(int X, int Y);
    internal record Vent(VentPosition A, VentPosition B);

    public class VentMap
    {
        IEnumerable<Vent> Vents { get; }

        Dictionary<VentPosition, int> CoverageMap { get; }

        public VentMap(IEnumerable<string> lines)
        {
            Vents = lines.Select(ParseVents).ToList();
            CoverageMap = GenerateCoverageMap(Vents);
        }

        public int CountDangerous(int threshold)
        {
            return CoverageMap.Count(kvp => kvp.Value >= threshold);
        }

        public VentMap AddDiagonalCoverage()
        {
            foreach (var vent in Vents.Where(v => !IsCardinal(v)))
            {
                var positions = GetPositions(vent);
                foreach (var pos in positions)
                {
                    if (!CoverageMap.ContainsKey(pos))
                    {
                        CoverageMap.Add(pos, 0);
                    }
                    CoverageMap[pos]++;
                }
            }

            return this;
        }

        static Vent ParseVents(string line)
        {
            var arr = line.Split(" -> ")
                          .Select(x => x.Trim().Split(',').Select(int.Parse))
                          .ToArray();

            return new Vent(GeneratePosition(arr.First()), GeneratePosition(arr.Last()));
        }

        static Dictionary<VentPosition, int> GenerateCoverageMap(IEnumerable<Vent> vents)
        {
            var posMap = new Dictionary<VentPosition, int>();
            foreach (var vent in vents.Where(v => IsCardinal(v)))
            {
                var positions = GetPositions(vent);
                foreach (var pos in positions)
                {
                    if (!posMap.ContainsKey(pos))
                    {
                        posMap.Add(pos, 0);
                    }
                    posMap[pos]++;
                }
            }
            return posMap;
        }

        static VentPosition GeneratePosition(IEnumerable<int> values)
        {
            return new VentPosition(values.First(), values.Last());
        }

        static bool IsCardinal(Vent vent)
        {
            return (vent.A.X == vent.B.X || vent.A.Y == vent.B.Y);
        }

        static IEnumerable<VentPosition> GetPositions(Vent v)
        {
            int lenX = (v.B.X - v.A.X);
            int lenY = (v.B.Y - v.A.Y);

            int dX = (v.A.X == v.B.X) ? 0 : lenX / Math.Abs(lenX);
            int dY = (v.A.Y == v.B.Y) ? 0 : lenY / Math.Abs(lenY);

            int len = Math.Max(Math.Abs(lenX), Math.Abs(lenY)) + 1;

            return Enumerable.Range(0, len)
                             .Select(i => new VentPosition(v.A.X + dX * i, v.A.Y + dY * i))
                             .ToArray();
        }
    }
}
