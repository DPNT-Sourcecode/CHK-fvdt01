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
        private const string skus = "[{\"product\":\"A\",\"price\":50,\"quantity\":3,\"specialoffer\":\"3A for 130, 5A for 200\"},{\"product\":\"B\",\"price\":30,\"quantity\":2,\"specialoffer\":\"2B for 45\"},{\"product\":\"C\",\"price\":20,\"quantity\":1,\"specialoffer\":\"\"},{\"product\":\"D\",\"price\":20,\"quantity\":1,\"specialoffer\":\"\"},{\"product\":\"E\",\"price\":20,\"quantity\":2,\"specialoffer\":\"2E get one B free\"}]";

        //3A2BCD2E
        [TestCase("[{\"product\":\"A\",\"price\":50,\"quantity\":1,\"specialoffer\":\"3A for 130, 5A for 200\"},{\"product\":\"B\",\"price\":30,\"quantity\":2,\"specialoffer\":\"2B for 45\"},{\"product\":\"C\",\"price\":20,\"quantity\":1,\"specialoffer\":\"\"},{\"product\":\"D\",\"price\":20,\"quantity\":1,\"specialoffer\":\"\"},{\"product\":\"E\",\"price\":20,\"quantity\":2,\"specialoffer\":\"2E get one B free\"}]"
            , ExpectedResult = 50)]
        public static int ComputePrice_A1(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("[{\"product\":\"A\",\"price\":50,\"quantity\":2,\"specialoffer\":\"3A for 130, 5A for 200\"},{\"product\":\"B\",\"price\":30,\"quantity\":2,\"specialoffer\":\"2B for 45\"},{\"product\":\"C\",\"price\":20,\"quantity\":1,\"specialoffer\":\"\"},{\"product\":\"D\",\"price\":20,\"quantity\":1,\"specialoffer\":\"\"},{\"product\":\"E\",\"price\":20,\"quantity\":2,\"specialoffer\":\"2E get one B free\"}]"
            , ExpectedResult = 100)]
        public static int ComputePrice_A2(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("[{\"product\":\"A\",\"price\":50,\"quantity\":3,\"specialoffer\":\"3A for 130, 5A for 200\"},{\"product\":\"B\",\"price\":30,\"quantity\":2,\"specialoffer\":\"2B for 45\"},{\"product\":\"C\",\"price\":20,\"quantity\":1,\"specialoffer\":\"\"},{\"product\":\"D\",\"price\":20,\"quantity\":1,\"specialoffer\":\"\"},{\"product\":\"E\",\"price\":20,\"quantity\":2,\"specialoffer\":\"2E get one B free\"}]"
            , ExpectedResult = 130)]
        public static int ComputePrice_A3(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("[{\"product\":\"A\",\"price\":50,\"quantity\":4,\"specialoffer\":\"3A for 130, 5A for 200\"},{\"product\":\"B\",\"price\":30,\"quantity\":2,\"specialoffer\":\"2B for 45\"},{\"product\":\"C\",\"price\":20,\"quantity\":1,\"specialoffer\":\"\"},{\"product\":\"D\",\"price\":20,\"quantity\":1,\"specialoffer\":\"\"},{\"product\":\"E\",\"price\":20,\"quantity\":2,\"specialoffer\":\"2E get one B free\"}]"
            , ExpectedResult = 180)]
        public static int ComputePrice_A4(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("[{\"product\":\"A\",\"price\":50,\"quantity\":5,\"specialoffer\":\"3A for 130, 5A for 200\"},{\"product\":\"B\",\"price\":30,\"quantity\":2,\"specialoffer\":\"2B for 45\"},{\"product\":\"C\",\"price\":20,\"quantity\":1,\"specialoffer\":\"\"},{\"product\":\"D\",\"price\":20,\"quantity\":1,\"specialoffer\":\"\"},{\"product\":\"E\",\"price\":20,\"quantity\":2,\"specialoffer\":\"2E get one B free\"}]"
            , ExpectedResult = 200)]
        public static int ComputePrice_A5(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("[{\"product\":\"A\",\"price\":50,\"quantity\":6,\"specialoffer\":\"3A for 130, 5A for 200\"},{\"product\":\"B\",\"price\":30,\"quantity\":2,\"specialoffer\":\"2B for 45\"},{\"product\":\"C\",\"price\":20,\"quantity\":1,\"specialoffer\":\"\"},{\"product\":\"D\",\"price\":20,\"quantity\":1,\"specialoffer\":\"\"},{\"product\":\"E\",\"price\":20,\"quantity\":2,\"specialoffer\":\"2E get one B free\"}]"
            , ExpectedResult = 250)]
        public static int ComputePrice_A6(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("[{\"product\":\"A\",\"price\":50,\"quantity\":7,\"specialoffer\":\"3A for 130, 5A for 200\"},{\"product\":\"B\",\"price\":30,\"quantity\":2,\"specialoffer\":\"2B for 45\"},{\"product\":\"C\",\"price\":20,\"quantity\":1,\"specialoffer\":\"\"},{\"product\":\"D\",\"price\":20,\"quantity\":1,\"specialoffer\":\"\"},{\"product\":\"E\",\"price\":20,\"quantity\":2,\"specialoffer\":\"2E get one B free\"}]"
            , ExpectedResult = 300)]
        public static int ComputePrice_A7(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("[{\"product\":\"A\",\"price\":50,\"quantity\":8,\"specialoffer\":\"3A for 130, 5A for 200\"},{\"product\":\"B\",\"price\":30,\"quantity\":2,\"specialoffer\":\"2B for 45\"},{\"product\":\"C\",\"price\":20,\"quantity\":1,\"specialoffer\":\"\"},{\"product\":\"D\",\"price\":20,\"quantity\":1,\"specialoffer\":\"\"},{\"product\":\"E\",\"price\":20,\"quantity\":2,\"specialoffer\":\"2E get one B free\"}]"
            , ExpectedResult = 330)]
        public static int ComputePrice_A8(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("[{\"product\":\"A\",\"price\":50,\"quantity\":9,\"specialoffer\":\"3A for 130, 5A for 200\"},{\"product\":\"B\",\"price\":30,\"quantity\":2,\"specialoffer\":\"2B for 45\"},{\"product\":\"C\",\"price\":20,\"quantity\":1,\"specialoffer\":\"\"},{\"product\":\"D\",\"price\":20,\"quantity\":1,\"specialoffer\":\"\"},{\"product\":\"E\",\"price\":20,\"quantity\":2,\"specialoffer\":\"2E get one B free\"}]"
            , ExpectedResult = 380)]
        public static int ComputePrice_A9(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("[{\"product\":\"A\",\"price\":50,\"quantity\":36,\"specialoffer\":\"3A for 130, 5A for 200\"},{\"product\":\"B\",\"price\":30,\"quantity\":2,\"specialoffer\":\"2B for 45\"},{\"product\":\"C\",\"price\":20,\"quantity\":1,\"specialoffer\":\"\"},{\"product\":\"D\",\"price\":20,\"quantity\":1,\"specialoffer\":\"\"},{\"product\":\"E\",\"price\":20,\"quantity\":2,\"specialoffer\":\"2E get one B free\"}]"
            , ExpectedResult = 1450)]
        public static int ComputePrice_A36(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }
    }
}


