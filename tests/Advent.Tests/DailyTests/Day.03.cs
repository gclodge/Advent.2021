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
    public class Day03 : Interfaces.IDailyTest
    {
        public int Number => 3;

        public string Input => TestHelper.GetInputFile(this);

        [Fact]
        public void Test_KnownDiagnostics()
        {
            var nums = new string[]
            {
                "00100",
                "11110",
                "10110",
                "10111",
                "10101",
                "01111",
                "00111",
                "11100",
                "10000",
                "11001",
                "00010",
                "01010"
            };
            var diag = new SubmarineDiagnostic(nums)
                            .GetGammaAndEpsilon()
                            .GetOxygenAndCarbonDioxide();

            int expectedGamma = 22;
            int expectedEpsilon = 9;
            int expectedPower = expectedGamma * expectedEpsilon;

            Assert.Equal(diag.Gamma, expectedGamma);
            Assert.Equal(diag.Epsilon, expectedEpsilon);
            Assert.Equal(diag.Power, expectedPower);

            int expectedOxygen = 23;
            int expectedCarbonDioxide = 10;
            int expectedLifeSupport = expectedOxygen * expectedCarbonDioxide;

            Assert.Equal(diag.Oxygen, expectedOxygen);
            Assert.Equal(diag.CarbonDioxide, expectedCarbonDioxide);
            Assert.Equal(diag.LifeSupport, expectedLifeSupport);
        }

        [Fact]
        public void PartOne()
        {
            var diag = new SubmarineDiagnostic(Input.Parse())
                            .GetGammaAndEpsilon();

            int expectedGamma = 3516;
            int expectedEpsilon = 579;
            int expectedPower = expectedGamma * expectedEpsilon;

            Assert.Equal(diag.Gamma, expectedGamma);
            Assert.Equal(diag.Epsilon, expectedEpsilon);
            Assert.Equal(diag.Power, expectedPower);
        }

        [Fact]
        public void PartTwo()
        {
            var diag = new SubmarineDiagnostic(Input.Parse())
                            .GetOxygenAndCarbonDioxide();

            int expectedOxygen = 3311;
            int expectedCarbonDioxide = 851;
            int expectedLifeSupport = expectedOxygen * expectedCarbonDioxide;

            Assert.Equal(diag.Oxygen, expectedOxygen);
            Assert.Equal(diag.CarbonDioxide, expectedCarbonDioxide);
            Assert.Equal(diag.LifeSupport, expectedLifeSupport);
        }
    }
}
