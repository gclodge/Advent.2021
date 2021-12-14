using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xunit;
using Assert = Xunit.Assert;

using Advent.Solutions.Utility;
using Advent.Solutions.Days;

namespace Advent.Tests.DailyTests
{
    public class Day14 : Interfaces.IDailyTest
    {
        public int Number => 14;

        public string Input => TestHelper.GetInputFile(this);
        public string Test => TestHelper.GetTestFile(this);

        [Fact]
        public void Test_KnownInputs()
        {
            var input = Test.Parse();
            var poly = new Polymerization(input);
            var res = poly.Solve();

            long expectedResult = 1588;
            Assert.Equal(expectedResult, res);
        }

        [Fact]
        public void PartOne()
        {
            var poly = new Polymerization(Input.Parse());
            var res = poly.Solve();

            long expectedResult = 3048;
            Assert.Equal(expectedResult, res);
        }

        [Fact]
        public void PartTwo()
        {
            var poly = new Polymerization(Input.Parse());
            var res = poly.Solve(40);

            long expectedResult = 3288891573057;
            Assert.Equal(expectedResult, res);
        }
    }
}
