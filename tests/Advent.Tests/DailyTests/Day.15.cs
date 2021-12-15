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
    public class Day15 : Interfaces.IDailyTest
    {
        public int Number => 15;

        public string Input => TestHelper.GetInputFile(this);
        public string Test => TestHelper.GetTestFile(this);

        [Fact]
        public void Test_KnownInputs()
        {
            var input = Test.Parse();
            var path = new ChitonPath(input);
            var risk = path.FindMinimalWeightPath(path.Target);

            int expectedRisk = 40;
            Assert.Equal(expectedRisk, risk);

            path = path.Extend(5);
            risk = path.FindMinimalWeightPath(path.Target);
            expectedRisk = 315;
            Assert.Equal(expectedRisk, risk);
        }

        [Fact]
        public void PartOne()
        {
            var path = new ChitonPath(Input.Parse());
            var risk = path.FindMinimalWeightPath(path.Target);

            int expectedRisk = 390;
            Assert.Equal(expectedRisk, risk);
        }

        [Fact]
        public void PartTwo()
        {
            var path = new ChitonPath(Input.Parse())
                            .Extend(5);
            var risk = path.FindMinimalWeightPath(path.Target);       

            int expectedRisk = 2814;
            Assert.Equal(expectedRisk, risk);
        }
    }
}
