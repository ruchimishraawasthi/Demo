using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromotionEngineUtility;
using System.Collections.Generic;

namespace PromotionEngineTest
{
    [TestClass]
    public class PromotionTest
    {
        private readonly PromotionProvider _promotionProvider;

        public PromotionTest()
        {
            _promotionProvider = new PromotionProvider();
        }

        [TestMethod]
        public void ApplyPromoForScenarioA()
        {
            List<IProduct> cart = new List<IProduct>();
            cart.Add(new Product("A") { Quantity = 1 });
            cart.Add(new Product("B") { Quantity = 1 });
            cart.Add(new Product("C") { Quantity = 1 });
            var result=_promotionProvider.ApplyPromotion(cart);
            Assert.AreEqual(result, 100);
        }
        [TestMethod]
        public void ApplyPromoForScenarioB()
        {
            List<IProduct> cart = new List<IProduct>();
            cart.Add(new Product("A") { Quantity = 5 });
            cart.Add(new Product("B") { Quantity = 5 });
            cart.Add(new Product("C") { Quantity = 1 });
            var result = _promotionProvider.ApplyPromotion(cart);
            Assert.AreEqual(result, 370);
        }
        [TestMethod]
        public void ApplyPromoForScenarioC()
        {
            List<IProduct> cart = new List<IProduct>();
            cart.Add(new Product("A") { Quantity = 3 });
            cart.Add(new Product("B") { Quantity = 5 });
            cart.Add(new Product("C") { Quantity = 1 });
            cart.Add(new Product("D") { Quantity = 1 });
            var result = _promotionProvider.ApplyPromotion(cart);
            Assert.AreEqual(result, 280);
        }
    }
}
