using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Advent.Solutions.Utility
{
    internal static class File
    {
        public static IEnumerable<string> Parse(string file)
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

        public static IEnumerable<T> Parse<T>(string file, Func<string, T> deserialize)
        {
            return Parse(file).Select(x => deserialize(x));
        }
    }
}
