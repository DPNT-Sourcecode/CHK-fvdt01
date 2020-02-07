using BeFaster.App.Solutions.SUM;
using Xunit;


namespace BeFaster.App.Tests.Solutions.SUM
{
    public class SumSolutionTests 
    {
        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(-4, -6, -10)]
        [InlineData(-2, 2, 0)]
        [InlineData(int.MinValue, -1, int.MaxValue)]
        public async void CanAdd(int x, int y, int expected)
        {
            var result = await SumSolution.Sum(x, y);

            Assert.Equal(expected, result);
        }
    }
}
