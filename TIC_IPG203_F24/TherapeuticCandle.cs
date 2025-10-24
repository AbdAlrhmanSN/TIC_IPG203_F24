using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TIC_IPG203_F24
{
    class TherapeuticCandle : Product
    {
        private List<string> _essentialOils;
        private string _therapeuticBenefit;
        private bool _isCertifiedOrganic;

        public TherapeuticCandle(string name, decimal basePrice, int stockQuantity,
                                List<string> essentialOils, string therapeuticBenefit,
                                bool isCertifiedOrganic)
            : base(name, basePrice, stockQuantity)
        {
            _essentialOils = essentialOils ?? new List<string>();
            _therapeuticBenefit = therapeuticBenefit;
            _isCertifiedOrganic = isCertifiedOrganic;
        }

        public List<string> get_EssentialOils()
        {
            return new List<string>(_essentialOils); // Return copy
        }

        public string get_TherapeuticBenefit()
        {
            return _therapeuticBenefit;
        }

        public void set_TherapeuticBenefit(string value)
        {
            _therapeuticBenefit = value;
        }

        public bool get_IsCertifiedOrganic()
        {
            return _isCertifiedOrganic; // Read-only
        }

        // 2. POLYMORPHISM: Override abstract method
        public override string GetProductInfo()
        {
            return "Therapeutic Candle: " + Name + "\n" +
                   "ID: " + ProductId.ToString() + "\n" +
                   "Essential Oils: " + string.Join(", ", _essentialOils.ToArray()) + "\n" +
                   "Therapeutic Benefit: " + _therapeuticBenefit + "\n" +
                   "Certified Organic: " + (_isCertifiedOrganic ? "Yes" : "No") + "\n" +
                   "Price: $" + CalculatePrice().ToString("F2") + "\n" +
                   "Stock: " + StockQuantity.ToString() + " units\n" +
                   "Status: " + (IsAvailable() ? "Available" : "Out of Stock");
        }

        // 2. POLYMORPHISM: Override virtual method with custom logic
        public override decimal CalculatePrice()
        {
            decimal price = BasePrice;

            // Each essential oil adds value
            price += _essentialOils.Count * 3.0m;

            // Organic certification adds premium
            if (_isCertifiedOrganic)
            {
                price *= 1.3m;
            }

            return price;
        }
    }
}
