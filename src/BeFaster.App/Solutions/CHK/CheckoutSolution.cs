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
            var arraySku = new Dictionary<string, int>();

            var skuList = SplitSkus(skus);
            skuList.ForEach(sku =>
            {
                GeneratePrice(sku, arraySku);
            });

            return arraySku.Select(x => x.Value).Sum();
        }

        public static int ItemA(int num)
        { 

                if (num == 1 || num == 2)
                {
                    return num * 50;
                }

                var rem = num >= 5 ? num % 5 : num % 3;
                if (num >= 5 && rem == 0)
                {
                    return (num / 5) * 200;
                }
                else if (num >= 5 && rem > 0)
                {
                   return (((num / 5) * 200) + rem * 50);
                }

                if (num < 5 && rem == 0)
                {
                    return (num / 3) * 130;
                }
                else if (num < 5 && rem > 0)
                {
                    return (((num / 3) * 130) + rem * 50);
                }

            return 0;
        }

        public static int ItemB(int num)
        {
            var rem = num >= 2 ? num % 2 : num;

            if (rem == 0)
            {
               return (num / 2) * 45;
            }
            else
            {
                return ((num / 2) * 45) + rem * 30;
            }
        }

        public static int ItemC(int num)
        {
            return num * 20;
        }

        public static int ItemD(int num)
        {
            return num * 15;
        }

        public static int ItemE(int num)
        {
            return num * 40;
        }

        public static void GeneratePrice(string sku, Dictionary<string, int> arraySku)
        {
            var keyB = "B";

            for (int i = 0; i < sku.Length; i++)
            {
                if (Char.IsDigit(sku[i]))
                {
                    switch (sku[i + 1])
                    {
                        case 'A':
                            {
                                arraySku.Add(sku[i] + "A", CheckoutSolution.ItemA(int.Parse(sku[i].ToString())));
                                break;
                            }

                        case 'B':
                            {
                                keyB = sku[i] + "B";
                                arraySku.Add(sku[i] + "B", CheckoutSolution.ItemB(int.Parse(sku[i].ToString())));
                                break;
                            }

                        case 'C':
                            {
                                arraySku.Add(sku[i] + "C", CheckoutSolution.ItemC(int.Parse(sku[i].ToString())));
                                break;
                            }
                        case 'D':
                            {
                                arraySku.Add(sku[i] + "D", CheckoutSolution.ItemD(int.Parse(sku[i].ToString())));
                                break;
                            }

                        case 'E':
                            {
                                arraySku.Add(sku[i] + "E", CheckoutSolution.ItemE(int.Parse(sku[i].ToString())));

                                if (arraySku.Keys.Contains("B"))
                                {
                                    int value = 0;
                                    if (keyB.Length > 1)
                                    {
                                        var num = int.Parse(keyB[0].ToString()) - 1;
                                        value = CheckoutSolution.ItemB(num);
                                    }

                                    arraySku[keyB] = value;
                                }


                                break;
                            }

                        default:
                            break;
                    }

                    i++;
                }
                else
                {


                    switch (sku[i])
                    {
                        case 'A':
                            {
                                arraySku.Add("A", CheckoutSolution.ItemA(1));
                                break;
                            }

                        case 'B':
                            {
                                arraySku.Add("B", CheckoutSolution.ItemB(1));

                                break;
                            }

                        case 'C':
                            {
                                arraySku.Add("C", CheckoutSolution.ItemC(1));
                                break;
                            }
                        case 'D':
                            {
                                arraySku.Add("D", CheckoutSolution.ItemD(1));
                                break;
                            }

                        case 'E':
                            {
                                arraySku.Add("E", CheckoutSolution.ItemE(1));
                                break;
                            }

                        default:
                            break;
                    }

                }
            }

        }

        private static List<string> SplitSkus(string skus)
        {
            var skuList = new List<string>();
            var item = string.Empty;
            for (int i = 0; i < skus.Length; i++)
            {
                if (char.IsDigit(skus[i]))
                {
                    item = item + skus[i].ToString();
                }
                else
                {
                    var key = item + skus[i].ToString();
                    item = string.Empty;
                    skuList.Add(key);
                    skus = skus.Substring(key.Length, skus.Length - key.Length);
                    i = -1;
                }
            }

            return skuList;

        }
    }
}
