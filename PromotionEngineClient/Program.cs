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
            List<IProduct> cart = new List<IProduct>();
           
            cart.Add(new Product("A") { Quantity=3});                       
            cart.Add(new Product("B") { Quantity = 5 });
            cart.Add(new Product("C") { Quantity = 1 });
            cart.Add(new Product("D") { Quantity = 1 });
                        
            PromotionProvider promotions = new PromotionProvider();
            decimal totalprice = promotions.ApplyPromotion(cart);
            Console.WriteLine("Total price after promotion applied:-"+totalprice);
            
            Console.ReadLine();
        }
    }
}
