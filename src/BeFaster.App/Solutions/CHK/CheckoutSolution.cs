using BeFaster.Runner.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Solutions.CHK
{

    public class Sku {
        public string Item { get; set; }
        public int Price { get; set; }
        public string SpecialOffer { get; set; }
    }

    public static class CheckoutSolution
    {
        public static int ComputePrice(string skus)
        {
            //SplitSkus from string
            //3A2BCD2E it should produce 3A,2B.C,D,2E
            //if contains 33AB44C should have 33A,B,44C and should work for other patterns

            var t = (object)  new { item = "A", price = 40, specialoffer = "3A for 130, 5A for 200" }.ToString();

            var skuSplit = SplitSkus(skus);
            return 0;
        }



        private static List<string> SplitSkus(string skus)
        {
            var result = new List<string>();
            string str= string.Empty;
            for (int i = 0; i < skus.Length; i++) {
                if (char.IsDigit(skus[i]))
                {
                    str = str + skus[i];
                }
                else {
                    var sku = str + skus[i];
                    str = string.Empty;
                    result.Add(sku);
                    // Remove the added sku form skus
                    skus = skus.Substring(sku.Length, skus.Length-sku.Length);
                    i = -1;
                }
            }

            return result;
        }
    }
}






