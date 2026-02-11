using System.Numerics;

namespace Project.Model.DTO
{
    public class GET_StockCountDto
    {
        public long TotalQuantity { get; set; }
        public string PRODUCT { get; set; }
        public string PAID { get; set; }
        public string UOM { get; set; }
    }
}
