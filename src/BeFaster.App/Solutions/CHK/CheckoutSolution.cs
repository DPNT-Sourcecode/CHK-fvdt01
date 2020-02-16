using BeFaster.Runner.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Solutions.CHK
{

    public static class OfferPrice
    {

        public static int Calclate(Sku sku)
        {
            switch (sku.Product)
            {
                case "A":
                    {
                        return ItemA(sku);
                        break;
                    }


            }
            return 0;
        }

        public static int ItemA(Sku sku)
        {
            var result = 0;
            var offer = sku.Offers.FirstOrDefault(x => sku.Quantity % x.Quantity == 0);

            if (offer != null)
            {
                var rem = sku.Quantity % offer.Quantity;
                if (rem == 0)
                {
                    result = (sku.Quantity / offer.Quantity) * offer.Price;
                }
                else
                {
                    result = ((sku.Quantity / offer.Quantity) * offer.Price) + (rem * sku.Price);
                }

            }else
            {
                result = sku.Quantity * sku.Price;
            }



            return result;
        }

    }

    public class Sku
    {
        public string Product { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string SpecialOffer { get; set; }
        public List<Offer> Offers { get; set; }

        public int TotalPrice
        {
            set
            {
            }
            get
            {
                return OfferPrice.Calclate(this);
            }
        }


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


            skus = Newtonsoft.Json.JsonConvert.SerializeObject(new[] {
                new { product = "A", price = 50, quantity = 4, specialoffer = "3A for 130, 5A for 200" },
                new { product = "B", price = 30, quantity = 2, specialoffer = "2B for 45" },
                new { product = "C", price = 20, quantity = 1, specialoffer = "" },
                new { product = "D", price = 20, quantity = 1, specialoffer = "" },
                new { product = "E", price = 20, quantity = 2, specialoffer = "2E get one B free" }
            });

            var skuList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Sku>>(skus);

            SplitSpecialOffer(skuList);

            var x = skuList[0].TotalPrice;

            return 0;
        }

        private static void SplitSpecialOffer(List<Sku> skuList)
        {
            skuList.ForEach(item =>
            {
                if (item.SpecialOffer.Length > 0) SpecialOfferFormatter(item);
            });
        }

        private static void SpecialOfferFormatter(Sku sku)
        {
            if (sku.SpecialOffer.IndexOf(",") > 0 && sku.SpecialOffer.Contains("for"))
            {
                sku.Offers = new List<Offer>();
                var splitComma = sku.SpecialOffer.Split(',').ToList();
                splitComma.ForEach(c =>
                {
                    var splitFor = c.Trim().Split(new string[] { "for" }, StringSplitOptions.None).ToList();
                    sku.Offers.Add(new Offer
                    {
                        Quantity = SplitSkus(splitFor[0].Trim()),
                        Price = int.Parse(splitFor[1].Trim())
                    });
                });
            }
            else if (sku.SpecialOffer.Contains("for"))
            {
                var splitFor = sku.SpecialOffer.Trim().Split(new string[] { "for" }, StringSplitOptions.None).ToList();
                sku.Offers = new List<Offer> {
                new Offer{
                    Quantity = SplitSkus(splitFor[0].Trim()),
                    Price = int.Parse(splitFor[1].Trim())
                }};
            }
            else if (sku.SpecialOffer.Contains("get one"))
            {
                //var splitFor = sku.SpecialOffer.Trim().Split(new string[] { "get one" }, StringSplitOptions.None).ToList();
                //sku.Offer = new List<Offer> {
                //new Offer{
                //    Quantity = SplitSkus(splitFor[0].Trim()),
                //    Price = int.Parse(splitFor[1].Trim())
                //}};
            }
        }

        private static int SplitSkus(string skus)
        {
            string str = string.Empty;
            for (int i = 0; i < skus.Length; i++)
            {
                if (char.IsDigit(skus[i]))
                {
                    str = str + skus[i];
                }
            }

            return int.Parse(str);
        }
    }
}



