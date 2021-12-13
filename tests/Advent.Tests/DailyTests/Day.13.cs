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
    public class Day13 : Interfaces.IDailyTest
    {
        public int Number => 13;

        public string Input => TestHelper.GetInputFile(this);
        public string Test => TestHelper.GetTestFile(this);

        [Fact]
        public void Test_KnownInputs()
        {
            var input = Test.Parse();
            var paper = new TransparentPaper(input);
            paper.Fold(1);

            int expectedCount = 17;
            Assert.Equal(expectedCount, paper.Count);
        }

        [Fact]
        public void PartOne()
        {
            var input = Input.Parse();
            var paper = new TransparentPaper(input);
            paper.Fold(1);

            int expectedCount = 682;
            Assert.Equal(expectedCount, paper.Count);
        }

        [Fact]
        public void PartTwo()
        {
            var input = Input.Parse();
            var paper = new TransparentPaper(input);
            paper.Fold();

            int expectedCount = 104;
            Assert.Equal(expectedCount, paper.Count);

            //< Need to print the image and find out what it is
            string imgPath = Path.ChangeExtension(Input, ".Result.txt");
            paper.Save(imgPath);

            Assert.True(File.Exists(imgPath));
        }
    }
}
