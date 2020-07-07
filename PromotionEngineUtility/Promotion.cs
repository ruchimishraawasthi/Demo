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
        public decimal CalculatePromotion(List<IProduct> _product)
        {
            decimal totalPrice = 0;
            int atotal;
            int aCount = _product.FindAll(x => x.SKUID == "A").Count;
            if (aCount % 3 == 0)
            {
                atotal = aCount / 3;
            }
            else
            {
                atotal = aCount / 3 + aCount % 3;
            }
            decimal bTotalPrice = _product.FindAll(x => x.SKUID == "B").Sum(x=>x.Price);
            decimal cTotalPrice = _product.FindAll(x => x.SKUID == "C").Sum(x=>x.Price);
            decimal dTotalPrice = _product.FindAll(x => x.SKUID == "D").Sum(x => x.Price);
            totalPrice = bTotalPrice + cTotalPrice + dTotalPrice;
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
        public decimal CalculatePromotion(List<IProduct> _product) {
            
            IsPromotionApplied = true;
            return 0;
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
        public decimal CalculatePromotion(List<IProduct> _product)
        {
            
            IsPromotionApplied = true;
            return 0;                 
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
        decimal CalculatePromotion(List<IProduct> _product);

        void RemovePromotion(List<IProduct> _product);
    }
    public class PromotionProvider
    {
        IPromotion _promotionType;
        List<IProduct> _product;
        public PromotionProvider(IPromotion promotionType)
        {
            _promotionType = promotionType;
        }
        public decimal ApplyPromotion(List<IProduct> product)
        { 
            _product = product;
           return _promotionType.CalculatePromotion(_product);        
        }
    }
    public interface IProduct
    {
        string SKUID { get; set; }
        int Quantity { get; set; }
        decimal Price { get; set; }        
       IPromotion PromotionType { get; set; }
       
    }
    public class Product : IProduct
    {
        public string SKUID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public IPromotion PromotionType { get; set; }
        public Product(string _skuid,int _quantity,decimal _price)
        {
            SKUID = _skuid;
            Quantity = _quantity;
            Price = _price;
        }        
    }
    
}
