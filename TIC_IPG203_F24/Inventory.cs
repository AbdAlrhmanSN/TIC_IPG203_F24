using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TIC_IPG203_F24
{
    //  delegate 
    public delegate void StockAlertHandler(Product product, string message);
    public class Inventory 
    {

        private List<Product> _products;
        private static decimal _totalRevenue = 0;

        // 5. STATIC PROPERTY: Track total revenue
        public static decimal get_TotalRevenue()
        {
            return _totalRevenue;
        }

        private static void set_TotalRevenue(decimal value)
        {
            _totalRevenue = value;
        }

        public Inventory()
        {
            _products = new List<Product>();
        }

        public int get_ProductCount()
        {
            return _products.Count;
        }

        public void AddProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            _products.Add(product);

            // Subscribe to stock alert events
            product.LowStockAlert += new StockAlertHandler(HandleLowStockAlert);
            product.OutOfStockAlert += new StockAlertHandler(HandleOutOfStockAlert);

            Console.WriteLine(" Added " + product.Name + " to inventory.");
        }

        // 2. POLYMORPHISM: Process different product types uniformly
        public void DisplayAllProducts()
        {
            Console.WriteLine("\n═══════════════════════════════════════");
            Console.WriteLine("         INVENTORY CATALOG");
            Console.WriteLine("═══════════════════════════════════════\n");

            foreach (Product product in _products)
            {
                // Polymorphic call - each type implements differently
                Console.WriteLine(product.GetProductInfo());
                Console.WriteLine("───────────────────────────────────────");
            }

            Console.WriteLine("\nTotal Products: " + _products.Count.ToString());
            Console.WriteLine("Total Products Created: " + Product.TotalProductsCreated.ToString());
        }

        public void DisplayPriceSummary()
        {
            Console.WriteLine("\n═══════════════════════════════════════");
            Console.WriteLine("         PRICE SUMMARY");
            Console.WriteLine("═══════════════════════════════════════\n");

            decimal totalInventoryValue = 0;

            foreach (Product product in _products)
            {
                decimal price = product.CalculatePrice();
                decimal value = price * product.StockQuantity;  // Use property directly
                totalInventoryValue += value;

                Console.WriteLine(product.Name + ": $" + price.ToString("F2") + " x " +
                                product.StockQuantity.ToString() + " = $" + value.ToString("F2"));
            }

            Console.WriteLine("\nTotal Inventory Value: $" + totalInventoryValue.ToString("F2"));
        }

        public bool SellProduct(string productId, int quantity)
        {
            Product product = null;
            foreach (Product p in _products)
            {
                if (p.ProductId == productId)  // Use property directly
                {
                    product = p;
                    break;
                }
            }

            if (product == null)
            {
                Console.WriteLine(" Product " + productId + " not found.");
                return false;
            }

            if (!product.IsAvailable() || product.StockQuantity < quantity)  // Use property directly
            {
                Console.WriteLine(" Insufficient stock for " + product.Name + ".");
                return false;
            }

            product.UpdateStock(-quantity);
            decimal saleAmount = product.CalculatePrice() * quantity;
            set_TotalRevenue(get_TotalRevenue() + saleAmount);

            Console.WriteLine(" Sold " + quantity.ToString() + " x " + product.Name + " for $" + saleAmount.ToString("F2"));
            Console.WriteLine("  Total Revenue: $" + get_TotalRevenue().ToString("F2"));

            return true;
        }

        public void RestockProduct(string productId, int quantity)
        {
            Product product = null;
            foreach (Product p in _products)
            {
                if (p.ProductId == productId)  // Use property directly
                {
                    product = p;
                    break;
                }
            }

            if (product == null)
            {
                Console.WriteLine(" Product " + productId + " not found.");
                return;
            }

            product.UpdateStock(quantity);
            Console.WriteLine(" Restocked " + product.Name + " with " + quantity.ToString() +
                             " units. New stock: " + product.StockQuantity.ToString());  // Use property directly
        }

        // 4. DELEGATES & EVENTS: Event handlers
        private void HandleLowStockAlert(Product product, string message)
        {
            Console.WriteLine("\n ALERT: " + message);
        }

        private void HandleOutOfStockAlert(Product product, string message)
        {
            Console.WriteLine("\n CRITICAL: " + message);
        }

        public List<Product> GetAvailableProducts()
        {
            List<Product> availableProducts = new List<Product>();
            foreach (Product product in _products)
            {
                if (product.IsAvailable())
                {
                    availableProducts.Add(product);
                }
            }
            return availableProducts;
        }

        // Delegate declarations needed for events (assuming these are defined elsewhere)
        public delegate void LowStockEventHandler(Product product, string message);
        public delegate void OutOfStockEventHandler(Product product, string message);
    }
}
