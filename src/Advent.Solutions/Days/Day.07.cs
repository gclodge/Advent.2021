namespace Advent.Solutions.Days
{
    public class CrabAligner
    {
        public int Min => PositionMap.Keys.Min();
        public int Max => PositionMap.Keys.Max();

        Dictionary<int, int> PositionMap;   

        public CrabAligner(IEnumerable<int> crabs)
        {
            PositionMap = crabs.GroupBy(c => c)
                               .ToDictionary(c => c.Key, c => c.Count());
        }

        public int GetCheapestAlignment(bool isGauss = false)
        {
            return Enumerable.Range(Min, Max - Min + 1)
                             .Select(c => GetCost(c,isGauss))
                             .OrderBy(c => c) //< Order descending
                             .First();
        }

        int GetCost(int pos, bool isGauss = false)
        {
            return PositionMap.Select(kvp => GetPositionOffset(pos,  kvp.Key, isGauss) * kvp.Value)
                              .Sum();
        }

        int GetPositionOffset(int pos, int index, bool isGauss = false)
        {
            int offset = Math.Abs(pos - index);
            return isGauss ? offset * (offset + 1) / 2 : offset;
        }
    }
}
