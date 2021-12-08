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
    public class Day06 : Interfaces.IDailyTest
    {
        public int Number => 6;

        public string Input => TestHelper.GetInputFile(this);
        public string Test => TestHelper.GetTestFile(this);

        [Fact]
        public void Test_KnownInputs()
        {
            var vals = new int[] { 3, 4, 3, 1, 2 };
            var school = new LanternFishSchool(vals);

            school.SimulateDays(80);
            ulong expectedEnd = 5934;
            Assert.Equal(expectedEnd, school.Count);

            school.SimulateDays(256 - 80);
            ulong expectedBigg = 26984457539;
            Assert.Equal(expectedBigg, school.Count);
        }

        [Fact]
        public void PartOne()
        {
            var inputs = Input.Parse().Single().Split(',').Select(int.Parse);
            var school = new LanternFishSchool(inputs);
            school.SimulateDays(80);

            ulong expectedCount = 389726;
            Assert.Equal(expectedCount, school.Count);
        }

        [Fact]
        public void PartTwo()
        {
            var inputs = Input.Parse().Single().Split(',').Select(int.Parse);
            var school = new LanternFishSchool(inputs);
            school.SimulateDays(256);

            ulong expectedCount = 1743335992042;
            Assert.Equal(expectedCount, school.Count);
        }
    }
}
