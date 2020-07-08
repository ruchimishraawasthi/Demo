using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;

namespace PromotionEngineUtility
{
    public class Promotion1 : IPromotion
    {
        public DateTime ExpirationDate { get; set; }
        public bool IsSingleUse { get; set; }
        public bool IsPromotionApplied { get; set; }
        public decimal CalculatePriceAfterPromotion(List<IProduct> _product)
        {
            decimal totalPrice = 0;
            decimal aTotalPrice = 0;
            int aQuantity = (Utility.GetProductA(_product)).Select(x => x.Quantity).FirstOrDefault();
            decimal aPrice = (Utility.GetProductA(_product)).Select(x => x.Price).FirstOrDefault();

            if (aQuantity % 3 == 0)
            {
                aTotalPrice = (aQuantity / 3)*130;
            }
            else
            {
                aTotalPrice = ((aQuantity - aQuantity % 3)/3)*130+ (aQuantity % 3)* aPrice;
            }
            
            totalPrice = aTotalPrice;
            IsPromotionApplied = true;
            return totalPrice;
        }
        public void RemovePromotion(List<IProduct> _product)
        {
            IsPromotionApplied = false;
        }
    }
    public class Promotion2 : IPromotion
    {
        public DateTime ExpirationDate { get; set; }
        public bool IsSingleUse { get; set; }
        public bool IsPromotionApplied { get; set; }
        public decimal CalculatePriceAfterPromotion(List<IProduct> _product) {
            decimal totalPrice = 0;
            decimal aTotalPrice=0;
            int atotal;
            int bQuantity = (Utility.GetProductB(_product)).Select(x => x.Quantity).FirstOrDefault() ;
            decimal aPrice = (Utility.GetProductB(_product)).Select(x => x.Price).FirstOrDefault();

            if (bQuantity % 2 == 0)
            {
                atotal = bQuantity / 2;
                aTotalPrice = atotal * 45;
            }
            else
            {
                aTotalPrice = ((bQuantity - bQuantity % 2) / 2) * 45 + (bQuantity % 2) * aPrice;
            }
            
            totalPrice = aTotalPrice;
            IsPromotionApplied = true;
            return totalPrice;
           
        }
        public void RemovePromotion(List<IProduct> _product)
        {
            IsPromotionApplied = false;
        }
    }
    public class Promotion3 : IPromotion
    {
        public DateTime ExpirationDate { get; set; }
        public bool IsSingleUse { get; set; }
        public bool IsPromotionApplied { get; set; }
        public decimal CalculatePriceAfterPromotion(List<IProduct> _product)
        {
            decimal totalPrice = 0;
            decimal atotal=0;
            int aTotalQuantity = Utility.GetProductC(_product).Select(s => s.Quantity).FirstOrDefault()+ Utility.GetProductD(_product).Select(s => s.Quantity).FirstOrDefault();//_product.AsQueryable().Intersect(Utility.GetProductD(_product)).Count();
            int cQuantity = Utility.GetProductC(_product).Select(s => s.Quantity).FirstOrDefault();
            int dQuantity = Utility.GetProductD(_product).Select(s => s.Quantity).FirstOrDefault();
            decimal cPrice= Utility.GetProductC(_product).Select(s => s.Price).FirstOrDefault();
            decimal dPrice = Utility.GetProductD(_product).Select(s => s.Price).FirstOrDefault();

            if (aTotalQuantity % 2 == 0)
            {
                atotal = (aTotalQuantity / 2) * 30;
            }
            else
            {
                if (cQuantity > dQuantity)
                {
                    atotal += (aTotalQuantity - aTotalQuantity % 2) * 30 + ((aTotalQuantity % 2) * cPrice);
                }
                else
                {
                    atotal += (aTotalQuantity - aTotalQuantity % 2) * 30 + ((aTotalQuantity % 2) * dPrice);
                }
            }
                    
            totalPrice = atotal;
            IsPromotionApplied = true;
            return totalPrice;
                           
        }
        public void RemovePromotion(List<IProduct> _product) {
            IsPromotionApplied = false;
        }
    }
    public interface IPromotion
    {
        DateTime ExpirationDate { get; set; }
        bool IsSingleUse { get; set; }
        bool IsPromotionApplied { get; set; }
        decimal CalculatePriceAfterPromotion(List<IProduct> _product);

        void RemovePromotion(List<IProduct> _product);
    }
    public class PromotionProvider
    {
        IPromotion _promotionType;
        List<IProduct> _product;
        decimal totalPrice;
        public PromotionProvider()
        {
            
        }
        private PromotionProvider(IPromotion promotionType)
        {
            _promotionType = promotionType;
        }
        public decimal ApplyPromotion(List<IProduct> product)
        {
            try
            {
                _product = Utility.GetProductA(product);
                _promotionType = new Promotion1();
                totalPrice += _promotionType.CalculatePriceAfterPromotion(_product);

                _product = Utility.GetProductB(product);
                _promotionType = new Promotion2();
                totalPrice += _promotionType.CalculatePriceAfterPromotion(_product);

                _product = Utility.GetProductCD(product);
                _promotionType = new Promotion3();
                totalPrice += _promotionType.CalculatePriceAfterPromotion(_product);
            }
            catch(Exception ex)
            {
                throw new PromotionException("error:-",ex);
            }
            return totalPrice;
        }
    }
    public interface IProduct
    {
        string SKUID { get; set; }
        int Quantity { get; set; }
        decimal Price { get; set; }        
       IPromotion PromotionType { get; set; }
       
    }
    public class Product : UnitPrice, IProduct
    {
        
        public string SKUID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public IPromotion PromotionType { get; set; }
        public Product(string _skuid, decimal _price)
        {
            SKUID = _skuid;
            Price = _price;
        }
        public Product(string _skuid)
        {
            SKUID = _skuid;
            Price = GetProductPrice(_skuid);
        }
    }
    public abstract class UnitPrice : IUnitPrice
    {
       private List<Product> units = new List<Product>();
            
       public List<Product> Units {
            get {
                units.Add(new Product("A",50));
                units.Add(new Product("B", 30));
                units.Add(new Product("C", 20));
                units.Add(new Product("D", 15));                
                return units; }
        }
        public decimal GetProductPrice(string skuid)
        {
            return Convert.ToDecimal(Units.Find(x=>x.SKUID== skuid).Price);
        }
    }
    public interface IUnitPrice
    {
        List<Product> Units { get; }
    }

    public static class Utility
    {
        public static List<IProduct> GetProductA(List<IProduct> products)
        {
            return products.FindAll(x => x.SKUID == "A");
        }
        public static List<IProduct> GetProductB(List<IProduct> products)
        {
            return products.FindAll(x => x.SKUID == "B");
        }
        public static List<IProduct> GetProductC(List<IProduct> products)
        {
            return products.FindAll(x => x.SKUID == "C");
        }
        public static List<IProduct> GetProductD(List<IProduct> products)
        {
            return products.FindAll(x => x.SKUID == "D");
        }
        public static List<IProduct> GetProductCD(List<IProduct> products)
        {
            return products.FindAll(x => x.SKUID == "D" || x.SKUID=="C");
        }
    }
}
public class PromotionException : Exception
{
    public PromotionException(string msg,Exception ex): base(String.Format("Promotion Engine Error: {0}", ex))
    { 
    }
}