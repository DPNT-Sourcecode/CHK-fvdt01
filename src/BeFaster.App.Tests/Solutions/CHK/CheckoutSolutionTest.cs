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
        //3A2BCD2E
        [TestCase("3AB2E", ExpectedResult = 210)]
        public int ComputePrice(string skus)
        {

            return CheckoutSolution.ComputePrice(skus);
        }

    //public class Item
    //{
    //    public string name { get; set; }

    //    public int Price { get; set; }

    //    public string SpecialOffer { get; set; }
    //}



}



