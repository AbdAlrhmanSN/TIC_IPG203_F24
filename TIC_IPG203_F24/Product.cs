using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TIC_IPG203_F24
{
    // Delegate for stock alerts
    public abstract class Product : IProduct
    {
        private string _name;
        private decimal _basePrice;
        private int _stockQuantity;
        private readonly string _productId; // Cannot be modified after creation
        private static int _productCounter = 0;

        private static int _totalProductsCreated = 0;

       

        // Constructor
        protected Product(string name, decimal basePrice, int stockQuantity)
        {
            _productCounter++;
            _productId = "PROD-" + _productCounter.ToString("D4");
            _name = name;
            _basePrice = basePrice;
            _stockQuantity = stockQuantity;
            _totalProductsCreated++;
        }

        // Static property 
        public static int TotalProductsCreated
        {
            get { return _totalProductsCreated; }
            private set { _totalProductsCreated = value; }
        }

        // ENCAPSULATION: Properties with controlled access
        public string ProductId
        {
            get { return _productId; }
        }

        public string Name
        {
            get { return _name; }
            protected set { _name = value; }
        }

        public decimal BasePrice
        {
            get { return _basePrice; }
            protected set
            {
                if (value < 0)
                    throw new ArgumentException("Price cannot be negative");
                _basePrice = value;
            }
        }

        public int StockQuantity
        {
            get { return _stockQuantity; }
            private set { _stockQuantity = value; }
        }

        // Abstract method that must be implemented by derived classes
        public abstract string GetProductInfo();

        // Virtual method for polymorphism
        public virtual decimal CalculatePrice()
        {
            return _basePrice;
        }

        public virtual bool IsAvailable()
        {
            return _stockQuantity > 0;
        }

        public void UpdateStock(int quantity)
        {
            _stockQuantity += quantity;

            // DELEGATES & EVENTS: Trigger event when stock is low
            if (_stockQuantity < 5 && _stockQuantity > 0)
            {
                OnLowStockAlert();
            }
            else if (_stockQuantity <= 0)
            {
                OnOutOfStockAlert();
            }
        }

        // DELEGATES & EVENTS: Event declarations
        public event StockAlertHandler LowStockAlert;
        public event StockAlertHandler OutOfStockAlert;

        protected virtual void OnLowStockAlert()
        {
            if (LowStockAlert != null)
            {
                LowStockAlert(this, "Low stock warning for " + _name + "! Only " + _stockQuantity + " left.");
            }
        }

        protected virtual void OnOutOfStockAlert()
        {
            if (OutOfStockAlert != null)
            {
                OutOfStockAlert(this, "OUT OF STOCK: " + _name + " is no longer available!");
            }
        }

      
    }
}
