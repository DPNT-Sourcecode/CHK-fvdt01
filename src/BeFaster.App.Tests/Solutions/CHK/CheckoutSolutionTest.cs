using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeFaster.App.Solutions.CHK;
using NUnit.Framework;

namespace BeFaster.App.Tests.Solutions.CHK
{
    [TestFixture]
    class CheckoutSolutionTest
    {
        private const string skus = "[{\"item\":\"A\",\"price\":50,\"specialoffer\":\"3A for 130, 5A for 200\"},{\"item\":\"B\",\"price\":30,\"specialoffer\":\"2B for 45\"},{\"item\":\"C\",\"price\":20,\"specialoffer\":\"\"},{\"item\":\"D\",\"price\":20,\"specialoffer\":\"\"},{\"item\":\"E\",\"price\":20,\"specialoffer\":\"2E get one B free\"}]";

        //3A2BCD2E
        [TestCase(skus, ExpectedResult = 210)]
        public static int ComputePrice(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }


    }
}



