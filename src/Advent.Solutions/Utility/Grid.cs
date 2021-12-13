using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Solutions.Utility
{
    public class Grid<T>
    {
        Dictionary<int, Dictionary<int, T>> Map;

        public Grid()
        {
            Map = new();
        }
        
        public bool Contains(int x, int y)
        {
            if (!Map.ContainsKey(y))
                return false;

            if (!Map[y].ContainsKey(x))
                return false;

            return true;
        }

        public void AddValue(T val, int x, int y)
        {
            if (!Map.ContainsKey(y))
            {
                Map.Add(y, new Dictionary<int, T>());
            }
            Map[y].Add(x, val);
        }

        public void SetValue(T val, int x, int y)
        {
            Map[y][x] = val;
        }
    }
}
