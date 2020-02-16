using BeFaster.Runner.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Solutions.CHK
{

    public class Sku
    {
        public string Item { get; set; }
        public int Price { get; set; }
        public string SpecialOffer { get; set; }
        public Offer Offer { get; set; }
    }

    public class Offer
    {
        public int Quantity { get; set; }
        public int Price { get; set; }
    }

    public static class CheckoutSolution
    {
        public static int ComputePrice(string skus)
        {
            //SplitSkus from string
            //3A2BCD2E it should produce 3A,2B.C,D,2E
            //if contains 33AB44C should have 33A,B,44C and should work for other patterns
            //var skuSplit = SplitSkus(skus);


            //skus = Newtonsoft.Json.JsonConvert.SerializeObject(new[] {
            //    new { item = "A", price = 50, specialoffer = "3A for 130, 5A for 200" },
            //    new { item = "B", price = 30, specialoffer = "2B for 45" },
            //    new { item = "C", price = 20, specialoffer = "" },
            //    new { item = "D", price = 20, specialoffer = "" },
            //    new { item = "E", price = 20, specialoffer = "2E get one B free" }
            //});

            var skuList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Sku>>(skus);

            SplitSpecialOffer(skuList);


            return 0;
        }

        private static void SplitSpecialOffer(List<Sku> skuList)
        {
            skuList.ForEach(item =>
            {
                SpecialOfferFormatter(item.SpecialOffer);


            });
        }

        private static void SpecialOfferFormatter(string specialOffer)
        {
            if (specialOffer.IndexOf(",") > 0)
            {
                var splitComma = specialOffer.Split(',').ToList();
                splitComma.ForEach(c =>
                {
                    var splitFor = c.Trim().Split(new string[] {"for"}, StringSplitOptions.None);
                });
            }
        }

        private static List<string> SplitSkus(string skus)
        {
            var result = new List<string>();
            string str = string.Empty;
            for (int i = 0; i < skus.Length; i++)
            {
                if (char.IsDigit(skus[i]))
                {
                    str = str + skus[i];
                }
                else
                {
                    var sku = str + skus[i];
                    str = string.Empty;
                    result.Add(sku);
                    // Remove the added sku form skus
                    skus = skus.Substring(sku.Length, skus.Length - sku.Length);
                    i = -1;
                }
            }

            return result;
        }
    }
}




