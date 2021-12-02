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
    public class Day02 : Interfaces.IDailyTest
    {
        public int Number => 2;

        public string Input => TestHelper.GetInputFile(this);

        [Fact]
        public void Test_KnownCommands()
        {
            var cmds = new List<string>()
            {
                "forward 5",
                "down 5",
                "forward 8",
                "up 3",
                "down 8",
                "forward 2"
            };

            var sub = new Submarine(cmds);
            sub.ChartCourse();

            int expectedPos = 15;
            int expectedDepth = 10;
            int expectedValue = expectedPos * expectedDepth;

            Assert.Equal(sub.HorizontalPosition, expectedPos);
            Assert.Equal(sub.Depth, expectedDepth);
            Assert.Equal(sub.Value, expectedValue);

            var aimSub = new Submarine(cmds, useAim: true);
            aimSub.ChartCourse();

            expectedPos = 15;
            expectedDepth = 60;
            expectedValue = expectedPos * expectedDepth;

            Assert.Equal(aimSub.HorizontalPosition, expectedPos);
            Assert.Equal(aimSub.Depth, expectedDepth);
            Assert.Equal(aimSub.Value, expectedValue);
        }

        [Fact]
        public void PartOne()
        {
            var sub = new Submarine(Input.Parse());
            sub.ChartCourse();

            int expectedValue = 2091984;
            Assert.Equal(expectedValue, sub.Value);
        }

        [Fact]
        public void PartTwo()
        {
            var sub = new Submarine(Input.Parse(), useAim: true);
            sub.ChartCourse();

            int expectedValue = 2086261056;
            Assert.Equal(expectedValue, sub.Value);
        }
    }
}
