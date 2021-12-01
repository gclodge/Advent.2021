using Xunit;

namespace Advent.Tests.Interfaces
{
    public interface IDailyTest
    {
        int Number { get; }

        string Input { get; }

        [Fact]
        void PartOne();

        [Fact]
        void PartTwo();
    }
}
