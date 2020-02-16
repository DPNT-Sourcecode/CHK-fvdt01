using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BeFaster.App.Tests.Solutions.CHK
{
    [TestFixture]
    class CheckoutSolutionTest
    {
        [TestCase("", ExpectedResult = 0)]
        public int ComputePrice(string skus)
        {

            return 0;
        }
    }
}

