using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Advent.Solutions.Utility
{
    public static class Extensions
    {
        public static IEnumerable<string> Parse(this string file)
        {
            var res = new List<string>();
            using (var sr = new StreamReader(file))
            {
                while (!sr.EndOfStream)
                {
                    res.Add(sr.ReadLine());
                }
            }
            return res;
        }

        public static IEnumerable<T> Parse<T>(this string file, Func<string, T> deserialize)
        {
            return Parse(file).Select(x => deserialize(x));
        }
    }
}
