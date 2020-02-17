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
                    var freeItem = splitFor[0].Trim();
                    var quantity = SplitSkus(freeItem);
                    sku.Offers.Add(new Offer
                    {
                        Product = freeItem.Substring(quantity.ToString().Length, freeItem.Length - quantity.ToString().Length),
                        Quantity = quantity,
                        Price = int.Parse(splitFor[1].Trim()),
                        FreeItem = freeItem,
                        IsOffer = sku.Quantity >= quantity
                    });
                });
            }
            else if (sku.SpecialOffer.Contains("for"))
            {
                var splitFor = sku.SpecialOffer.Trim().Split(new string[] { "for" }, StringSplitOptions.None).ToList();
                var freeItem = splitFor[0].Trim();
                var quantity = SplitSkus(freeItem);
                sku.Offers = new List<Offer> {
                new Offer{
                        Product = freeItem.Substring(quantity.ToString().Length, freeItem.Length - quantity.ToString().Length),
                        Quantity = quantity,
                        Price = int.Parse(splitFor[1].Trim()),
                        FreeItem = freeItem,
                        IsOffer = sku.Quantity >= quantity
                }};
            }
            else if (sku.SpecialOffer.Contains("get one"))
            {
                var splitFor = sku.SpecialOffer.Trim().Split(new string[] { "get one" }, StringSplitOptions.None).ToList();
                var freeItem = splitFor[0].Trim();
                var quantity = SplitSkus(freeItem);
                var prod = splitFor[1].Trim().Split(new string[] { "free" }, StringSplitOptions.None).ToList()[0].Trim();

                sku.Offers = new List<Offer> {
                new Offer{
                    Product = prod,
                    Quantity = sku.Product.Equals(prod) ? quantity+1  : sku.Quantity/quantity, //sku.Product.Equals(prod) ? quantity+1 :quantity,
                    Price = sku.Product.Equals(prod) ? quantity * sku.Price : sku.Price,
                    FreeItem = freeItem,
                    IsOffer = prod.Equals(sku.Product)
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
            if (sku.Quantity > 0) return Item(sku);
            return 0;
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
                    else if (initialQuantity != 0 && !sku.Offers.Any(x => x.Quantity <= initialQuantity))
                    {
                        result = sku.Quantity * sku.Price;
                    }
                    //else if (initialQuantity == 1 || initialQuantity == 2)
                    //{
                    //    result = sku.Quantity * sku.Price;
                    //}
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
            skuList.ForEach(prod => {
                prod.TotalPrice = Item1(prod);
            });

            var otherOfferItems = skuList.ToList()
                .Where(x => x.Offers != null && x.Quantity > 0 && x.Offers.Any(o => !o.Product.Equals(x.Product)));
            otherOfferItems.ToList().ForEach(offer =>
            {
                var foundOffer = skuList.ToList().Find(p => offer.Offers.Any(o => o.Product == p.Product));
                var currentOffer = offer.Offers.Where(x => x.Product.Equals(foundOffer.Product)).First();
                if (currentOffer.Quantity > 0)
                {
                    if (foundOffer.Quantity == 0)
                    {
                        foundOffer.Quantity += 1;
                    }
                    else if (foundOffer.Offers.Any(x => foundOffer.Quantity >= x.Quantity))
                    {
                        var offerQty = offer.Offers.Select(x => x.Quantity).First();
                        var freeItem = foundOffer.Quantity - offerQty;
                        foundOffer.Quantity = freeItem;
                        foundOffer.TotalPrice = Item1(foundOffer);
                        foundOffer.Quantity += freeItem;
                    }
                    else if (foundOffer.Offers.Any(x => foundOffer.Quantity < x.Quantity))
                    {
                        var offerQty = offer.Offers.Select(x => x.Quantity).First();
                        foundOffer.Quantity -= 1;
                        foundOffer.TotalPrice = Item1(foundOffer);
                        foundOffer.Quantity += 1;
                    }
                }

            });
        }


        public static int Item1(Sku sku)
        {
            var result = 0;
            var initialQuantity = sku.Quantity;
            //A2BCD4E
            if (sku.Offers != null)
            {
                sku.Offers.OrderByDescending(x => x.Quantity).ToList().ForEach(offer =>
                {
                    if (sku.Quantity > 0)
                    {
                        

                        if (initialQuantity >= offer.Quantity && sku.Product.Equals(offer.Product))
                        {
                            initialQuantity = initialQuantity - offer.Quantity;

                            if (initialQuantity >= offer.Quantity)
                            {
                                result = result + offer.Price * (sku.Quantity / offer.Quantity);
                                initialQuantity = initialQuantity % offer.Quantity;
                            }
                            else {
                                result = result + offer.Price; 
                            }

                        }
                        else if (initialQuantity >= offer.Quantity && !sku.Product.Equals(offer.Product))
                        {
                            result = sku.Quantity * offer.Price;
                            initialQuantity = 0;
                        }
                    }

                });
            }

            result += initialQuantity * sku.Price;

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
        public int TotalPrice { get; set; }

        public void CalculatePriceAndOffers()
        {
            if (this.Price > 0 && this.Quantity > 0) TotalPrice = OfferPrice.Calclate(this);
            if (SpecialOffer.Length > 0)
            {
                OfferPrice.SpecialOfferFormatter(this);
            }
        }
    }

    public class Offer
    {
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string FreeItem { get; internal set; }
        public string Product { get; internal set; }
        public bool IsOffer { get; internal set; }
    }
}
