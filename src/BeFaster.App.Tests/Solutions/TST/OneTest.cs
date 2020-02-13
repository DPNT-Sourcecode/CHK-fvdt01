using BeFaster.App.Solutions.TST;
using Xunit;

namespace BeFaster.App.Tests.Solutions.TST
{
    public class OneTest {
            
        [Fact]
        public void RunApply() {
            Assert.Equal(One.apply(), (int)1);
        }
    }
}
