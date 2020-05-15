﻿using System;

namespace TinyCrm.Core.Model
{
    public class Product
    {
        public string ProductId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public Decimal? Price { get; set; }
        public ProductCategory? Category { get; set; }
    }
}