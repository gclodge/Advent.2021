using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Solutions.Days
{
    record FoldPoint(int X, int Y);

    sealed class Fold
    {
        public char Axis { get; }
        public int Coordinate { get; }

        public Fold(string input)
        {
            var arr = input.Split('=');
            this.Axis = arr[0].Last();
            this.Coordinate = int.Parse(arr[1]);
        }

        public bool NeedsFolding(FoldPoint fp)
        {
            switch (Axis)
            {
                case 'y':
                    return fp.Y > Coordinate;
                case 'x':
                    return fp.X > Coordinate;
                default:
                    throw new NotImplementedException($"What kinda axis is: {Axis}?");
            }           
        }

        public FoldPoint ApplyFold(FoldPoint fp)
        {
            int dX = Axis == 'x' ? fp.X - Coordinate : 0;
            int dY = Axis == 'y' ? fp.Y - Coordinate : 0;

            return new FoldPoint(fp.X - 2 * dX, fp.Y - 2 * dY);
        }
    }

    public class TransparentPaper
    {
        List<Fold> Folds = new();
        List<FoldPoint> Points = new();

        public int Count => Points.Count;

        public TransparentPaper(IEnumerable<string> inputs)
        {
            foreach (var input in inputs)
            {
                if (input.Contains(','))
                {
                    //< Add new point to Grid
                    var pnt = input.Split(',')
                                   .Select(int.Parse)
                                   .ToArray();

                    Points.Add(new FoldPoint(pnt[0], pnt[1]));
                }

                if (input.Contains('='))
                {
                    //< Add new Fold
                    Folds.Add(new Fold(input));
                }
            }
        }

        public void Fold(int folds = -1)
        {
            if (folds < 0)
                folds = Folds.Count;

            foreach (var fold in Folds.Take(folds))
            {
                ApplyFold(fold);
            }
        }

        public void Save(string file)
        {
            int min = Points.Min(p => p.X);
            int max = Points.Max(p => p.X);

            var pntMap = Points.GroupBy(p => p.Y)
                               .ToDictionary(p => p.Key, p => p.ToList());

            var sb = new StringBuilder();
            foreach (var y in pntMap.Keys.OrderBy(p => p))
            {
                var xs = pntMap[y].Select(p => p.X).ToHashSet();
                foreach (int x in Enumerable.Range(min, max - min + 1))
                {
                    if (xs.Contains(x))
                        sb.Append('#');
                    else
                        sb.Append('.');
                }
                sb.Append(Environment.NewLine);
            }
            File.WriteAllText(file, sb.ToString());
        }

        void ApplyFold(Fold fold)
        {
            var res = new HashSet<FoldPoint>();
            
            foreach (var pnt in Points)
            {
                if (fold.NeedsFolding(pnt))
                {
                    var fp = fold.ApplyFold(pnt);
                    res.Add(fp);
                }
                else
                    res.Add(pnt);
            }

            this.Points = res.ToList();
        }
    }
}
