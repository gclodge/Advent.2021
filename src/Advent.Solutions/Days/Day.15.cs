using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Solutions.Days
{
    using Tree = Dictionary<(int x, int y), PathNode>;

    class PathNode
    {
        public int X { get; }
        public int Y { get; }

        public int Risk { get; }
        public int Distance { get; set; } = int.MaxValue;

        public bool Visited { get; set; } = false;

        public PathNode(int x, int y, int risk)
        {
            this.X = x;
            this.Y = y;
            this.Risk = risk;
        }
    }
        
    public class ChitonPath
    {
        public (int, int) Start = (0, 0);
        public (int, int) Target;

        public int Width { get; }
        public int Height { get; }

        static readonly (int, int)[] Neighbours = new[]
        {
            (-1, 0), (1, 0), (0, -1), (0, 1)
        };

        Tree Path = new();

        public ChitonPath(IEnumerable<string> inputs)
        {
            Height = inputs.Count();
            Width = inputs.First().Length;
            Target = (Width - 1, Height - 1);

            foreach (int y in Enumerable.Range(0, inputs.Count()))
            {
                string item = inputs.ElementAt(y);
                foreach (int x in Enumerable.Range(0, item.Length))
                {
                    int risk = int.Parse(item[x].ToString());
                    Path.Add((x, y), new PathNode(x, y, risk));
                }
            }
        }

        public ChitonPath Extend(int extension)
        {
            int width = (int)Math.Sqrt(Path.Count);

            Path = Enumerable.Range(0, extension).SelectMany(i =>
                       Enumerable.Range(0, extension).SelectMany(j =>
                          Path.Select(kvp =>
                          {
                              (int x, int y) newKey = (kvp.Key.x + width * i, kvp.Key.y + width * j);
                              var newRisk = (kvp.Value.Risk + i + j - 1) % 9 + 1;
                              return (newKey, new PathNode(newKey.x, newKey.y, newRisk));
                          })
                       )
                   ).ToDictionary(tup => tup.Item1, tup => tup.Item2);

            Target = (extension * Width - 1, extension * Height - 1);

            return this;
        }

        public int FindMinimalWeightPath((int, int) target)
        {
            if (!Path.ContainsKey(target)) throw new ArgumentOutOfRangeException(nameof(target));

            return Djikstra(Path[target]);
        }

        int Djikstra(PathNode target)
        {
            var pq = new PriorityQueue<PathNode, int>();
            Path[Start].Distance = 0;
            pq.Enqueue(Path[Start], 0);

            while (pq.Count > 0)
            {
                var curr = pq.Dequeue();
                if (curr.Visited) continue;

                curr.Visited = true;

                if (curr == target) return target.Distance;

                foreach (var neigh in GetNeighbours(curr))
                {
                    var check = curr.Distance + neigh.Risk;
                    if (check < neigh.Distance) neigh.Distance = check;

                    if (neigh.Distance != int.MaxValue) pq.Enqueue(neigh, neigh.Distance);
                }
            }

            return target.Distance;
        }

        IEnumerable<PathNode> GetNeighbours(PathNode node)
        {
            foreach ((int x, int y) in Neighbours)
            {
                var pos = (node.X + x, node.Y + y);
                if (Path.ContainsKey(pos) && !Path[pos].Visited)
                {
                    yield return Path[pos];
                }
            }
        }
    }
}
