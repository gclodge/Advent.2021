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
    public class Day11 : Interfaces.IDailyTest
    {
        public int Number => 11;

        public string Input => TestHelper.GetInputFile(this);
        public string Test => TestHelper.GetTestFile(this);

        [Fact]
        public void Test_KnownInputs()
        {
            var input = Test.Parse();
            var octo = new OctopusGrid(input);
            var res = octo.Solve();

            int expectedSync = 195;
            int expectedFlashes = 1656;
            Assert.Equal(expectedFlashes, res.flashCount);
            Assert.Equal(expectedSync, res.synchStep);
        }

        [Fact]
        public void PartOne()
        {
            var input = Input.Parse();
            var octo = new OctopusGrid(input);
            var res = octo.Solve();

            int expectedFlashes = 1562;
            Assert.Equal(expectedFlashes, res.flashCount);
        }

        [Fact]
        public void PartTwo()
        {
            var input = Input.Parse();
            var octo = new OctopusGrid(input);
            var res = octo.Solve();

            int expectedSynchStep = 268;
            Assert.Equal(expectedSynchStep, res.synchStep);
        }
    }
}
