using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TIC_IPG203_F24
{
    public class ScentedCandle : Product
    {
        private string _scentType;
        private int _burnTimeHours;

        public ScentedCandle(string name, decimal basePrice, int stockQuantity,
                            string scentType, int burnTimeHours)
            : base(name, basePrice, stockQuantity)
        {
            _scentType = scentType;
            _burnTimeHours = burnTimeHours;
        }

        public string ScentType
        {
            get { return _scentType; }
            set { _scentType = value; }
        }

        public int BurnTimeHours
        {
            get { return _burnTimeHours; }
            set { _burnTimeHours = value > 0 ? value : _burnTimeHours; }
        }

        public override string GetProductInfo()
        {
            return "Scented Candle: " + Name + "\n" +
                   "ID: " + ProductId + "\n" +
                   "Scent: " + _scentType + "\n" +
                   "Burn Time: " + _burnTimeHours + " hours\n" +
                   "Price: $" + CalculatePrice().ToString("F2") + "\n" +
                   "Stock: " + StockQuantity + " units\n" +
                   "Status: " + (IsAvailable() ? "Available" : "Out of Stock");
        }

        // 2. POLYMORPHISM: Override virtual method with custom logic
        public override decimal CalculatePrice()
        {
            // Premium scents cost more
            decimal scentMultiplier = 1.0m;
            string lowerScent = _scentType.ToLower();

            if (lowerScent == "lavender" || lowerScent == "vanilla")
            {
                scentMultiplier = 1.2m;
            }
            else if (lowerScent == "rose" || lowerScent == "jasmine")
            {
                scentMultiplier = 1.5m;
            }
            else if (lowerScent == "sandalwood" || lowerScent == "oud")
            {
                scentMultiplier = 2.0m;
            }

            return BasePrice * scentMultiplier;
        }
    }
}
