﻿using BeFaster.Core.Data;

namespace BeFaster.Data
{
    public class Product : IProduct
    {
        public string Sku { get; set; }
        public int? Price { get; set; }
    }
}
