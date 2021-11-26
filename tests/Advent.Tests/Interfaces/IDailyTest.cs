using Xunit;

namespace Advent.Tests.Interfaces
{
    public interface IDailyTest
    {
        int Number { get; }

        [Fact]
        void PartOne();

        [Fact]
        void PartTwo();
    }
}
