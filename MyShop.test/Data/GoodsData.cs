﻿namespace MyShop.test.Data
{
    using System.Linq;
    using MyShop.Data.Models;
    using System.Collections.Generic;

    public static class GoodsData
    {
        public static IEnumerable<Goods> TenMockGoods
            => Enumerable.Range(0, 10).Select(c => new Goods()); 
    }
}
