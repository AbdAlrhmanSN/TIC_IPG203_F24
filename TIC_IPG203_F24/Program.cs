using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TIC_IPG203_F24
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║     CANDLE STORE MANAGEMENT SYSTEM     ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            // Demonstrate Static Class validation
            DemonstrateValidation();

            // Create inventory
            Inventory inventory = new Inventory();

            // Create different types of products (Polymorphism)
            Console.WriteLine("\n--- Creating Products ---\n");

            // 1. Scented Candles
            ScentedCandle candle1 = new ScentedCandle(
                "Lavender Dream", 15.99m, 10, "Lavender", 40
            );

            ScentedCandle candle2 = new ScentedCandle(
                "Vanilla Bliss", 12.99m, 3, "Vanilla", 35
            );

            // 2. Decorative Candles
            DecorativeCandle candle3 = new DecorativeCandle(
                "Rose Sculpture", 25.00m, 5, "Sculpture", "Pink", true
            );

            DecorativeCandle candle4 = new DecorativeCandle(
                "Modern Pillar", 18.50m, 8, "Pillar", "White", false
            );

            // 3. Therapeutic Candles
            TherapeuticCandle candle5 = new TherapeuticCandle(
                "Stress Relief", 22.00m, 6,
                new List<string> { "Chamomile", "Bergamot", "Ylang-Ylang" },
                "Reduces stress and promotes relaxation",
                true
            );

            TherapeuticCandle candle6 = new TherapeuticCandle(
                "Energy Boost", 20.00m, 2,
                new List<string> { "Peppermint", "Eucalyptus" },
                "Increases energy and mental clarity",
                false
            );

            // Add all products to inventory
            inventory.AddProduct(candle1);
            inventory.AddProduct(candle2);
            inventory.AddProduct(candle3);
            inventory.AddProduct(candle4);
            inventory.AddProduct(candle5);
            inventory.AddProduct(candle6);

            // Display all products (Polymorphism in action)
            inventory.DisplayAllProducts();

            // Display price summary (Polymorphism - different price calculations)
            inventory.DisplayPriceSummary();

            // Demonstrate selling products and triggering events
            Console.WriteLine("\n\n--- Simulating Sales ---\n");

            inventory.SellProduct(candle1.ProductId, 2);
            inventory.SellProduct(candle2.ProductId, 1); // Should trigger low stock alert
            inventory.SellProduct(candle6.ProductId, 2); // Should trigger out of stock alert

            // Demonstrate restocking
            Console.WriteLine("\n\n--- Restocking Products ---\n");

            inventory.RestockProduct(candle2.ProductId, 10);
            inventory.RestockProduct(candle6.ProductId, 15);

            // Display static properties
            Console.WriteLine("\n\n═══════════════════════════════════════");
            Console.WriteLine("         BUSINESS STATISTICS");
            Console.WriteLine("═══════════════════════════════════════\n");
            Console.WriteLine("Total Products Created: " + Product.TotalProductsCreated.ToString());
            Console.WriteLine("Total Revenue Generated: $" + Inventory.get_TotalRevenue().ToString("F2"));
            Console.WriteLine("Products in Inventory: " + inventory.get_ProductCount().ToString());

            // Demonstrate accessing available products
            Console.WriteLine("\n\n--- Available Products ---\n");
            List<Product> availableProducts = inventory.GetAvailableProducts();
            Console.WriteLine("Currently " + availableProducts.Count.ToString() + " products available for sale:");
            foreach (Product product in availableProducts)
            {
                Console.WriteLine("  • " + product.Name + " (" + product.ProductId + ") - $" + product.CalculatePrice().ToString("F2"));
            }

            Console.WriteLine("\n\n╔════════════════════════════════════════╗");
            Console.WriteLine("║     DEMONSTRATION COMPLETED            ║");
            Console.WriteLine("╚════════════════════════════════════════╝");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static void DemonstrateValidation()
        {
            Console.WriteLine("\n--- Validation Demonstration ---\n");

            ValidationHelper.DisplayValidationResult(
                "Product Name 'Lavender Candle'",
                ValidationHelper.IsValidProductName("Lavender Candle")
            );

            ValidationHelper.DisplayValidationResult(
                "Product Name 'LC'",
                ValidationHelper.IsValidProductName("LC")
            );

            ValidationHelper.DisplayValidationResult(
                "Price $15.99",
                ValidationHelper.IsValidPrice(15.99m)
            );

            ValidationHelper.DisplayValidationResult(
                "Price $-5.00",
                ValidationHelper.IsValidPrice(-5.00m)
            );

            ValidationHelper.DisplayValidationResult(
                "Stock Quantity 50",
                ValidationHelper.IsValidStockQuantity(50)
            );

            ValidationHelper.DisplayValidationResult(
                "Email 'SVU@example.com'",
                ValidationHelper.IsValidEmail("SVU@example.com")
            );

            ValidationHelper.DisplayValidationResult(
                "Scent Type 'lavender'",
                ValidationHelper.IsValidScentType("lavender")
            );
        }
    }
}
