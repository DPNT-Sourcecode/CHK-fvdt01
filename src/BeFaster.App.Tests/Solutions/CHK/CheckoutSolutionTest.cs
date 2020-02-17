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
            , ExpectedResult = 245)]

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
            , ExpectedResult = 325)]
        public static int ComputePrice_A3(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("4A2BCD4E"
            , ExpectedResult = 375)]
        public static int ComputePrice_A4(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("5A2BCD4E"
            , ExpectedResult = 395)]
        public static int ComputePrice_A5(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("6A2BCD4E"
            , ExpectedResult = 445)]
        public static int ComputePrice_A6(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("7A2BCD4E"
            , ExpectedResult = 495)]
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
            , ExpectedResult = 1645)]
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
            , ExpectedResult = -1)]
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

        [TestCase("ABCDE"
            , ExpectedResult = 155)]
        public static int ComputePrice_ABCDE(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("AAAA"
            , ExpectedResult = 180)]
        public static int ComputePrice_AAAA(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("AAAAA"
            , ExpectedResult = 200)]
        public static int ComputePrice_AAAAA(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("AAAAAA"
            , ExpectedResult = 250)]
        public static int ComputePrice_AAAAAA(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("AAAAAAA"
            , ExpectedResult = 300)]
        public static int ComputePrice_AAAAAAA(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("AAAAAAAA"
            , ExpectedResult = 330)]
        public static int ComputePrice_AAAAAAAA(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("AAAAAAAAA"
            , ExpectedResult = 380)]
        public static int ComputePrice_AAAAAAAAA(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("AAAAAAAAAA"
            , ExpectedResult = 400)]
        public static int ComputePrice_AAAAAAAAAA(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("AAAAAAAAAAA"
            , ExpectedResult = 450)]
        public static int ComputePrice_AAAAAAAAAAA(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("EE"
            , ExpectedResult = 80)]
        public static int ComputePrice_EE(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("EeE"
            , ExpectedResult = 120)]
        public static int ComputePrice_EeE(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("EeEB"
            , ExpectedResult = 120)]
        public static int ComputePrice_EeEB(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("EeEBB"
            , ExpectedResult = 150)]
        public static int ComputePrice_EeEBB(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("EeEBBE"
            , ExpectedResult = 160)]
        public static int ComputePrice_EeEBBE(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("BB"
            , ExpectedResult = 45)]
        public static int ComputePrice_BB(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("BBB"
            , ExpectedResult = 75)]
        public static int ComputePrice_BBB(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("BBBB"
            , ExpectedResult = 90)]
        public static int ComputePrice_BBBB(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("BBBBB"
            , ExpectedResult = 120)]
        public static int ComputePrice_BBBBB(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("ABCDEABCDE"
            , ExpectedResult = 280)]
        public static int ComputePrice_ABCDEABCDE(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }


        [TestCase("CCADDEEBBA"
            , ExpectedResult = 280)]
        public static int ComputePrice_CCADDEEBBA(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase(""
        , ExpectedResult = -1)]
        public static int ComputePrice_Empty(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("-"
        , ExpectedResult = -1)]
        public static int ComputePrice_Empty_cjar(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }

        [TestCase("AAAAAEEBAAABB"
        , ExpectedResult = 455)]
        public static int ComputePrice_AAAAAEEBAAABB(string skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }


    }
}









