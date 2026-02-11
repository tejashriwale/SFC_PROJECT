using System.Numerics;

namespace Project.Model.DTO
{
    public class SP_InsertMonthlyClosingBalDto
    {
        public string PAID { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }
        public string BATCHNO { get; set; }
        public BigInteger TotalAmount { get; set; }
        public BigInteger TotalQuantity { get; set; }
        public string PRODUCT { get; set; }
        public string UOM { get; set; }
    }
}
