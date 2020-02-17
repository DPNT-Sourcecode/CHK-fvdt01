using BeFaster.App.Solutions.CHK;
using NUnit.Framework;

namespace BeFaster.App.Tests.Solutions.CHK
{
    [TestFixture]
    class CheckoutSolutionTest
    {
        private const string skus = "[{\"product\":\"A\",\"price\":50,\"quantity\":3,\"specialoffer\":\"3A for 130, 5A for 200\"},{\"product\":\"B\",\"price\":30,\"quantity\":2,\"specialoffer\":\"2B for 45\"},{\"product\":\"C\",\"price\":20,\"quantity\":1,\"specialoffer\":\"\"},{\"product\":\"D\",\"price\":15,\"quantity\":1,\"specialoffer\":\"\"},{\"product\":\"E\",\"price\":40,\"quantity\":2,\"specialoffer\":\"2E get one B free\"}]";

        //3A2BCD2E
        //[TestCase("[{\"product\":\"A\",\"price\":50,\"quantity\":1,\"specialoffer\":\"3A for 130, 5A for 200\"},{\"product\":\"B\",\"price\":30,\"quantity\":2,\"specialoffer\":\"2B for 45\"},{\"product\":\"C\",\"price\":20,\"quantity\":1,\"specialoffer\":\"\"},{\"product\":\"D\",\"price\":15,\"quantity\":1,\"specialoffer\":\"\"},{\"product\":\"E\",\"price\":40,\"quantity\":2,\"specialoffer\":\"2E get one B free\"}]"
        //    , ExpectedResult = 165)]
        [TestCase("A2BCD4E"
            , ExpectedResult = 130)]

        public static int ComputePrice_A1(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("2A2BCD4E"
            , ExpectedResult = 295)]
        public static int ComputePrice_A2(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("3A2BCD4E"
            , ExpectedResult = 210)]
        public static int ComputePrice_A3(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("4A2BCD4E"
            , ExpectedResult = 575)]
        public static int ComputePrice_A4(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("5A2BCD4E"
            , ExpectedResult = 445)]
        public static int ComputePrice_A5(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("6A2BCD4E"
            , ExpectedResult = 495)]
        public static int ComputePrice_A6(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("7A2BCD4E"
            , ExpectedResult = 545)]
        public static int ComputePrice_A7(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("8A2BCD4E"
            , ExpectedResult = 525)]
        public static int ComputePrice_A8(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("9A2BCD4E"
            , ExpectedResult = 575)]
        public static int ComputePrice_A9(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("36A2BCD4E"
            , ExpectedResult = 1995)]
        public static int ComputePrice_A36(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("a"
            , ExpectedResult = 50)]
        public static int ComputePrice_a(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("ABCa"
            , ExpectedResult = 150)]
        public static int ComputePrice_ABCa(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }
        [TestCase("ABaCaC"
            , ExpectedResult = 200)]
        public static int ComputePrice_ABaCaC(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }
    }
}


