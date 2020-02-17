using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeFaster.App.Solutions.TST
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
            else if (sku.Product.Equals("E") && sku.SpecialOffer.Contains("get one"))
            {
                var splitFor = sku.SpecialOffer.Trim().Split(new string[] { "get one" }, StringSplitOptions.None).ToList();
                sku.Offers = new List<Offer> {
                new Offer{
                    Quantity = 1, //SplitSkus(splitFor[0].Trim()),
                    Price = sku.Price,
                    FreeItem = (SplitSkus(splitFor[0].Trim())) + splitFor[1].Trim().Split(new string[] { "free" }, StringSplitOptions.None).ToList()[0].Trim()
                    }
                };
            }
            else if (sku.Product.Equals("F") && sku.SpecialOffer.Contains("get one"))
            {
                var splitFor = sku.SpecialOffer.Trim().Split(new string[] { "get one" }, StringSplitOptions.None).ToList();
                sku.Offers = new List<Offer> {
                new Offer{
                    Quantity = 1, //SplitSkus(splitFor[0].Trim()),
                    Price = sku.Price,
                    FreeItem = SplitSkus(splitFor[0].Trim()) + splitFor[1].Trim().Split(new string[] { "free" }, StringSplitOptions.None).ToList()[0].Trim()
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
                        if (sku.Quantity > 0) return Item(sku);
                        return 0;
                    }
                case "E":
                case "F":
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
                    if (offer.Quantity == initialQuantity)
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
                    else if (initialQuantity > offer.Quantity && offer.Quantity != 0)
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
                                initialQuantity = rem;
                            }
                            else
                            {
                                result = result + ((initialQuantity / offer.Quantity) * offer.Price) + (rem * sku.Price);
                                initialQuantity = 0;
                            }
                        }
                    }
                    else if (initialQuantity == 1 || initialQuantity == 2)
                    {
                        result = sku.Quantity * sku.Price;
                    }
                    else if (offer.Quantity == 0)
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
                            if (x.Quantity != 0 && x.Product == "E")
                            {
                                var discountOn = x.Quantity / OfferPrice.SplitSkus(offer.FreeItem);
                                if (discountOn == x.Quantity) return;
                                if (p.Quantity != 0)
                                    p.Quantity = discountOn > p.Quantity ? discountOn - p.Quantity : p.Quantity - discountOn;
                            }
                            if (x.Quantity != 0 && x.Product == "F")
                            {

                                var discountOn = x.Quantity - (x.Quantity / 2);
                                if (discountOn == x.Quantity) return;
                                if (p.Quantity != 0)
                                    p.Quantity = discountOn;
                            }


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
}


