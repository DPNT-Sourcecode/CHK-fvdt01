using BeFaster.App.Solutions.TST;
using BeFaster.Runner.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Solutions.CHK
{

    public static class CheckoutSolution
    {
        public static int ComputePrice(string skus)
        {
            //SplitSkus from string
            //3A2BCD2E it should produce 3A,2B.C,D,2E
            //if contains 33AB44C should ehave 33A,B,44C and should work for other patterns
            if (skus.Contains('-') || skus.Any(x => Char.IsLower(x))) return -1;

            if (!skus.Any()) return 0;

            var skuSplit = SplitSkus(skus);


            var skuList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Sku>>(Newtonsoft.Json.JsonConvert.SerializeObject(new[] {
                new { product = "A", price = 50, quantity = 0, specialoffer = "3A for 130, 5A for 200" },
                new { product = "B", price = 30, quantity = 0, specialoffer = "2B for 45" },
                new { product = "C", price = 20, quantity = 0, specialoffer = "" },
                new { product = "D", price = 15, quantity = 0, specialoffer = "" },
                new { product = "E", price = 40, quantity = 0, specialoffer = "2E get one B free" },
                new { product = "F", price = 10, quantity = 0, specialoffer = "2F get one F free" },
                new { product = "G", price = 20, quantity = 0, specialoffer = "" },
                new { product = "H", price = 10, quantity = 0, specialoffer = "5H for 45, 10H for 80" },
                new { product = "I", price = 35, quantity = 0, specialoffer = "" },
                new { product = "J", price = 60, quantity = 0, specialoffer = "" },
            }));

            skuList.ForEach(o =>
            {
                if (skuSplit.ContainsKey(o.Product))
                    o.Quantity = skuSplit[o.Product];
            });

            OfferPrice.ProcessFreeItemOffer(skuList);

            var ItemA = skuList[0].TotalPrice;
            var ItemB = skuList[1].TotalPrice;
            var ItemC = skuList[2].TotalPrice;
            var ItemD = skuList[3].TotalPrice;
            var ItemE = skuList[4].TotalPrice;

            return skuList.Sum(x => x.TotalPrice);
        }

        private static Dictionary<string, int> SplitSkus(string skus)
        {

            string quantity = string.Empty;
            var item = new Dictionary<string, int>();
            for (int i = 0; i < skus.Length; i++)
            {
                if (char.IsDigit(skus[i]))
                {
                    quantity = quantity + skus[i];
                }
                else
                {
                    var prod = skus[i].ToString();
                    if (item.ContainsKey(prod))
                    {
                        item[prod] = item[prod] + 1;
                    }
                    else
                    {
                        item.Add(prod, quantity == string.Empty ? 1 : int.Parse(quantity));
                    }
                    quantity = string.Empty;

                    skus = skus.Substring(i + 1, skus.Length - (i + 1));
                    i = -1;
                }
            }

            return item;
        }
    }
}


