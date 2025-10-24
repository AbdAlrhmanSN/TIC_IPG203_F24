using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TIC_IPG203_F24
{
    class DecorativeCandle : Product
    {
        private string _shape;
        private string _color;
        private bool _isHandcrafted;

        public DecorativeCandle(string name, decimal basePrice, int stockQuantity,
                               string shape, string color, bool isHandcrafted)
            : base(name, basePrice, stockQuantity)
        {
            _shape = shape;
            _color = color;
            _isHandcrafted = isHandcrafted;
        }

        public string get_Shape()
        {
            return _shape;
        }

        public void set_Shape(string value)
        {
            _shape = value;
        }

        public string get_Color()
        {
            return _color;
        }

        public void set_Color(string value)
        {
            _color = value;
        }

        public bool get_IsHandcrafted()
        {
            return _isHandcrafted;
        }

        // 2. POLYMORPHISM: Override abstract method
        public override string GetProductInfo()
        {
            return "Decorative Candle: " + Name + "\n" +
                   "ID: " + ProductId.ToString() + "\n" +
                   "Shape: " + _shape + "\n" +
                   "Color: " + _color + "\n" +
                   "Handcrafted: " + (_isHandcrafted ? "Yes" : "No") + "\n" +
                   "Price: $" + CalculatePrice().ToString("F2") + "\n" +
                   "Stock: " + StockQuantity.ToString() + " units\n" +
                   "Status: " + (IsAvailable() ? "Available" : "Out of Stock");
        }

        // 2. POLYMORPHISM: Override virtual method with custom logic
        public override decimal CalculatePrice()
        {
            decimal price = BasePrice;

            // Handcrafted items cost 50% more
            if (_isHandcrafted)
            {
                price *= 1.5m;
            }

            // Complex shapes add to the price
            string lowerShape = _shape.ToLower();
            if (lowerShape == "sculpture" || lowerShape == "flower")
            {
                price += 5.0m;
            }

            return price;
        }
    }
}
