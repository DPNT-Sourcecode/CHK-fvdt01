using BeFaster.Runner.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Solutions.CHK
{

    public static class OfferPrice
    {
        public static void SpecialOfferFormatter(Sku sku)
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
                var splitFor = sku.SpecialOffer.Trim().Split(new string[] { "get one" }, StringSplitOptions.None).ToList();
                sku.Offers = new List<Offer> {
                new Offer{
                    Quantity = SplitSkus(splitFor[0].Trim()),
                    Price = sku.Quantity * sku.Price,
                    FreeItem = (sku.Quantity/SplitSkus(splitFor[0].Trim())) + splitFor[1].Trim().Split(new string[] { "free" }, StringSplitOptions.None).ToList()[0].Trim()
                    }
                };
            }
        }

        public static int SplitSkus(string skus)
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

        public static int Calclate(Sku sku)
        {
            switch (sku.Product)
            {
                case "A":
                case "B":
                case "C":
                case "D":
                    {
                        if(sku.Quantity>0) return Item(sku);
                        return 0;
                    }
                case "E":
                    {
                        if (sku.Quantity > 0) return Item(sku);
                        return 0;
                    }
                default:
                    return 0;
            }
        }

        public static int Item(Sku sku)
        {
            var result = 0;
            var initialQuantity = sku.Quantity;

            if (sku.Offers != null)
            {
                sku.Offers.OrderByDescending(x => x.Quantity).ToList().ForEach(offer =>
                {
                    if (initialQuantity >= offer.Quantity && offer.FreeItem == null)
                    {
                        var rem = initialQuantity % offer.Quantity;
                        if (rem == 0)
                        {
                            result = result + (initialQuantity / offer.Quantity) * offer.Price;
                            initialQuantity = 0;
                        }
                        else
                        {
                            if (sku.Offers.Select(x => x.Quantity <= rem).FirstOrDefault())
                            {
                                result = result + (initialQuantity / offer.Quantity) * offer.Price;
                            }
                            else
                            {
                                result = result + ((initialQuantity / offer.Quantity) * offer.Price) + (rem * sku.Price);
                            }
                            initialQuantity = rem;
                        }
                    }
                    else if (offer.FreeItem == null && !sku.Offers.Any(x => x.Quantity == sku.Quantity))
                    {
                        result = sku.Quantity * sku.Price;
                    }
                });
            }
            else
            {
                result = sku.Quantity * sku.Price;
            }

            return result;
        }

        public static void ProcessFreeItemOffer(List<Sku> skuList)
        {
            var freeOfferItem = skuList.ToList()
                .Where(x => x.Offers != null)
                .Where(x => x.Offers.Any(t => t.FreeItem != null)).ToList();

            freeOfferItem.ToList()
                .ForEach(x =>
                {
                    skuList.ToList().ForEach(p =>
                    {
                        var offer = x.Offers.Where(t => t.FreeItem.Contains(p.Product)).FirstOrDefault();
                        if (offer != null)
                        {

                            p.TotalPrice = OfferPrice.SplitSkus(offer.FreeItem) == 0 ? 0 : (x.Quantity % OfferPrice.SplitSkus(offer.FreeItem)) * p.Price;
                        }
                    });


                });

        }
    }

    public class Sku
    {
        private string specialOffer;
        private int quantity;
        public string Product { get; set; }
        public int Price { get; set; }
        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                this.TotalPrice = OfferPrice.Calclate(this);
            }
        }
        public string SpecialOffer
        {
            get { return specialOffer; }
            set
            {
                specialOffer = value;
                if (specialOffer.Length > 0)
                {
                    OfferPrice.SpecialOfferFormatter(this);
                }
            }
        }
        public List<Offer> Offers { get; set; }
        public int TotalPrice
        {
            get; set;
        }
    }

    public class Offer
    {
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string FreeItem { get; internal set; }
    }

    public static class CheckoutSolution
    {
        public static int ComputePrice(string skus)
        {
            //SplitSkus from string
            //3A2BCD2E it should produce 3A,2B.C,D,2E
            //if contains 33AB44C should ehave 33A,B,44C and should work for other patterns
            if (!skus.Any()) return -1;
            var skuSplit = SplitSkus(skus);

            var skuList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Sku>>(Newtonsoft.Json.JsonConvert.SerializeObject(new[] {
                new { product = "A", price = 50, quantity = 0, specialoffer = "3A for 130, 5A for 200" },
                new { product = "B", price = 30, quantity = 0, specialoffer = "2B for 45" },
                new { product = "C", price = 20, quantity = 0, specialoffer = "" },
                new { product = "D", price = 15, quantity = 0, specialoffer = "" },
                new { product = "E", price = 40, quantity = 0, specialoffer = "2E get one B free" }
            }));

            skuList.ForEach(o =>
            {
                if(skuSplit.ContainsKey(o.Product))
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
                    var prod = skus[i].ToString().ToUpper();
                    if (item.ContainsKey(prod))
                    {
                        item[prod] = item[prod] + 1;
                    }
                    else {
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




