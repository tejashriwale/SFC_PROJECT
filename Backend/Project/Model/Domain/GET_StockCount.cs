using System.Numerics;

namespace Project.Model.Domain
{
    public class GET_StockCount
    {
        public long TotalQuantity { get; set; }
        public string PRODUCT { get; set; }
        public string PAID { get; set; }
        public string UOM { get; set; }
       
    }
}
