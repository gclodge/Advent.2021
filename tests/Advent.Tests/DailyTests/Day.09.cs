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
    public class Day09 : Interfaces.IDailyTest
    {
        public int Number => 9;

        public string Input => TestHelper.GetInputFile(this);
        public string Test => TestHelper.GetTestFile(this);

        [Fact]
        public void Test_KnownInputs()
        {
            var input = new string[]
            {
                "2199943210",
                "3987894921",
                "9856789892",
                "8767896789",
                "9899965678"
            };

            var map = new VolcanicHeightMap(input)
                            .GetLowPointRisk()
                            .GenerateBasinGroups();

            int expectedRisk = 15;
            int expectedCount = 4;

            Assert.Equal(expectedRisk, map.LowPointRisk);
            Assert.Equal(expectedCount, map.LowPointCount);

            int expectedBasins = 4;
            int expectedBasinProduct = 1134;

            Assert.Equal(expectedBasins, map.BasinCount);
            Assert.Equal(expectedBasinProduct, map.LargestBasinProduct);
        }

        [Fact]
        public void PartOne()
        {
            var map = new VolcanicHeightMap(Input.Parse())
                            .GetLowPointRisk();

            int expectedRisk = 537;
            int expectedCount = 228;

            Assert.Equal(expectedRisk, map.LowPointRisk);
            Assert.Equal(expectedCount, map.LowPointCount);
        }

        [Fact]
        public void PartTwo()
        {
            var map = new VolcanicHeightMap(Input.Parse())
                            .GetLowPointRisk()
                            .GenerateBasinGroups();

            int expectedBasins = 228;
            int expectedBasinProduct = 1142757;

            Assert.Equal(expectedBasins, map.BasinCount);
            Assert.Equal(expectedBasinProduct, map.LargestBasinProduct);
        }
    }
}
