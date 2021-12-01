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
    public class Day01 : Interfaces.IDailyTest
    {
        public int Number => 1;

        public string Input => TestHelper.GetInputFile(this);

        [Fact]
        public void Test_KnownSweeps()
        {
            var values = new List<int>()
            {
                199,
                200,
                208,
                210,
                200,
                207,
                240,
                269,
                260,
                263
            };

            var sweeps = new SonarSweep(values);

            int expected = 7;
            int actual = sweeps.CountIncreasing();

            Assert.Equal(expected, actual);

            int expectedWindows = 5;
            int actualWindows = sweeps.CountIncreasingWindows();

            Assert.Equal(expectedWindows, actualWindows);
        }

        [Fact]
        public void PartOne()
        {
            var inputs = Input.Parse(int.Parse);
            var sweeps = new SonarSweep(inputs);

            int actual = sweeps.CountIncreasing();
            int expected = 1502;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PartTwo()
        {
            var inputs = Input.Parse(int.Parse);
            var sweeps = new SonarSweep(inputs);

            int actual = sweeps.CountIncreasingWindows();
            int expected = 1538;

            Assert.Equal(expected, actual);
        }
    }
}
