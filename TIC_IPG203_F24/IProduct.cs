using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TIC_IPG203_F24
{
    public interface IProduct
    {
        //abstract methods
        string GetProductInfo();
        decimal CalculatePrice();
        bool IsAvailable();
        void UpdateStock(int quantity);

        // Properties in interfaces
        string ProductId { get; }
        string Name { get; }
        decimal BasePrice { get; }
        int StockQuantity { get; }

        // Events in interfaces
        event StockAlertHandler LowStockAlert;
        event StockAlertHandler OutOfStockAlert;
    }
}
