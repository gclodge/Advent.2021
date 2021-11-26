namespace Advent.Solutions.Utility
{
    internal static class Functions
    {
        public static int? FindRecordsThatSumTo(IEnumerable<int> recs, int sumValue, int len)
        {
            foreach (var perm in GetPermutations(recs.OrderBy(x => x), len))
            {
                if (perm.Sum() == sumValue)
                {
                    int val = perm.First();
                    for (int i = 1; i < perm.Count(); i++)
                    {
                        val *= perm.ElementAt(i);
                    }
                    return val;
                }
            }

            return null;
        }

        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1).SelectMany(t => list.Where(e => !t.Contains(e)), (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> elements, int k)
        {
            return k == 0 ? new[] { new T[0] } :
              elements.SelectMany((e, i) =>
                elements.Skip(i + 1).Combinations(k - 1).Select(c => (new[] { e }).Concat(c)));
        }

        public static List<IEnumerable<T>> GetPossibleCombinations<T>(IEnumerable<T> vals)
        {
            var combos = new List<IEnumerable<T>>();

            int n = vals.Count();
            while (n > 0)
            {
                var combs = Combinations(vals, n);
                combos.AddRange(combs);
                n--;
            }

            return combos;
        }
    }
}
