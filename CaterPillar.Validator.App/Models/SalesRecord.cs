using CaterPillar.Validator.WebApp.Helpers;
using System;

namespace CaterPillar.Validator.WebApp.Models
{
    public class SalesRecord
    {
        public string Region { get; set; }
        public string Country { get; set; }
        public string ItemType { get; set; }
        public string SalesChannel { get; set; }
        public string OrderPriority { get; set; }
        public string OrderDate { get; set; }
        public string OrderId { get; set; }
        public string ShipDate { get; set; }
        public string UnitsSold { get; set; }
        public string UnitPrice { get; set; }
        public string UnitCost { get; set; }
        public string TotalRevenue { get; set; }
        public string TotalCost { get; set; }
        public string TotalProfit { get; set; }
        public string ErrorComments { get; set; }

        [SkipProperty]        
        public decimal UnitPriceDecimal
        {
            get
            {
                return Convert.ToDecimal(UnitPrice);
            }
        }

        [SkipProperty]
        public int UnitSoldInt
        {
            get
            {
                return Convert.ToInt32(UnitsSold);
            }
        }

        public static SalesRecord Create(string region, string country, string itemType, string salesChannel, string orderPriority,
            string orderDate, string orderId, string shipDate, string unitsSold, string unitPrice, string unitCost, string totalRevenue,
            string totalCost, string totalProfit)
        {
            SalesRecord salesRecord = new SalesRecord
            {
                Region = region,
                Country = country,
                ItemType = itemType,
                SalesChannel = salesChannel,
                OrderPriority = orderPriority,
                OrderDate = orderDate,
                OrderId = orderId,
                ShipDate = shipDate,
                UnitsSold = unitsSold,
                UnitPrice = unitPrice,
                UnitCost = unitCost,
                TotalRevenue = totalRevenue,
                TotalCost = totalCost,
                TotalProfit = totalProfit
            };
            return salesRecord;
        }

        private SalesRecord() { }

    }
}
