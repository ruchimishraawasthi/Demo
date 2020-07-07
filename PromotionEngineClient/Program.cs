using System;
using PromotionEngineUtility;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngineClient
{
    class Program
    {
        static void Main(string[] args)
        {
            IPromotion promotion1 = new Promotion1();
            List<IProduct> cart = new List<IProduct>();
            //cart.Add(new Product("A", 1, 50) { PromotionType=promotion1});
            //cart.Add(new Product("B", 1, 30) { PromotionType = promotion1 });
            //cart.Add(new Product("C", 1, 20) { PromotionType = promotion1 });
            //cart.Add(new Product("D", 1, 15) { PromotionType = promotion1 });

            cart.Add(new Product("A", 1, 50) { PromotionType = promotion1 });
            cart.Add(new Product("B", 1, 30) { PromotionType = promotion1 });
            cart.Add(new Product("C", 1, 20) { PromotionType = promotion1 });
            //cart.Add(new Product("D", 1, 15) { PromotionType = promotion1 });

            PromotionProvider promotions = new PromotionProvider(promotion1);
            decimal totalprice = promotions.ApplyPromotion(cart);
            Console.WriteLine(totalprice);
            Console.ReadLine();
        }
    }
}
