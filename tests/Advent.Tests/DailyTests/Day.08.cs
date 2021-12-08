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
    public class Day08 : Interfaces.IDailyTest
    {
        public int Number => 8;

        public string Input => TestHelper.GetInputFile(this);
        public string Test => TestHelper.GetTestFile(this);

        [Fact]
        public void Test_KnownInputs()
        {
            var input = Test.Parse();
            var search = new SevenSegmentSearch(input);

            int unique = search.CountUniqueOutputs;
            int sum = search.SumOfSolutions;

            int expectedUnique = 26;
            int expectedSum = 61229;
            Assert.Equal(expectedUnique, unique);
            Assert.Equal(expectedSum, sum);
        }

        [Fact]
        public void PartOne()
        {
            var search = new SevenSegmentSearch(Input.Parse());

            int unique = search.CountUniqueOutputs;
            int expectedUnique = 479;

            Assert.Equal(expectedUnique, unique);
        }

        [Fact]
        public void PartTwo()
        {
            var search = new SevenSegmentSearch(Input.Parse());

            int sum = search.SumOfSolutions;
            int expectedSum = 1041746;

            Assert.Equal(expectedSum, sum);
        }
    }
}
