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
    public class Day05 : Interfaces.IDailyTest
    {
        public int Number => 5;

        public string Input => TestHelper.GetInputFile(this);
        public string Test => TestHelper.GetTestFile(this);

        [Fact]
        public void Test_KnownInputs()
        {
            #region Test Setup
            var inputs = new string[]
            {
                "0,9 -> 5,9",
                "8,0 -> 0,8",
                "9,4 -> 3,4",
                "2,2 -> 2,1",
                "7,0 -> 7,4",
                "6,4 -> 2,0",
                "0,9 -> 2,9",
                "3,4 -> 1,4",
                "0,0 -> 8,8",
                "5,5 -> 8,2"
            };
            #endregion

            var map = new VentMap(inputs);
            int danger = map.CountDangerous(2);

            int expectedDanger = 5;
            Assert.Equal(expectedDanger, danger);

            //< Add the diagonal coverage and check the new result
            map = map.AddDiagonalCoverage();
            int diagDanger = map.CountDangerous(2);
            int expectedDiagDanger = 12;

            Assert.Equal(expectedDiagDanger, diagDanger);
        }

        [Fact]
        public void PartOne()
        {
            var map = new VentMap(Input.Parse());
            int danger = map.CountDangerous(2);

            int expectedDanger = 5197;
            Assert.Equal(expectedDanger, danger);
        }

        [Fact]
        public void PartTwo()
        {
            var map = new VentMap(Input.Parse())
                            .AddDiagonalCoverage();
            int danger = map.CountDangerous(2);

            int expectedDanger = 18605;
            Assert.Equal(expectedDanger, danger);
        }
    }
}
