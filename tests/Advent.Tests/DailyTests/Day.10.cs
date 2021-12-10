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
    public class Day10 : Interfaces.IDailyTest
    {
        public int Number => 10;

        public string Input => TestHelper.GetInputFile(this);
        public string Test => TestHelper.GetTestFile(this);

        [Fact]
        public void Test_KnownInputs()
        {
            var input = Test.Parse();
            var solver = new SyntaxSolver(input);

            int expectedCorruption = 26397;
            int expectedCompletion = 288957;
            Assert.Equal(expectedCorruption, solver.Corruption);
            Assert.Equal(expectedCompletion, solver.Completion);
        }

        [Fact]
        public void PartOne()
        {
            var solver = new SyntaxSolver(Input.Parse());

            int expectedCorruption = 299793;
            Assert.Equal(expectedCorruption, solver.Corruption);
        }

        [Fact]
        public void PartTwo()
        {
            var solver = new SyntaxSolver(Input.Parse());

            long expectedCompletion = 3654963618;
            Assert.Equal(expectedCompletion, solver.Completion);
        }
    }
}
