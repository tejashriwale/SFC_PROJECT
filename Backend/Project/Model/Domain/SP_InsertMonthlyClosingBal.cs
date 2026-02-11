using System.Numerics;

namespace Project.Model.Domain
{
    public class SP_InsertMonthlyClosingBal
    {
        public string PAID          { get; set; }
        public string COMPANYID     { get; set; }
        public string DEPTID        { get; set; }
        public string BATCHNO       { get; set; }
        public Int64 TotalAmount   { get; set; }
        public Int64 TotalQuantity { get; set; }
        public string PRODUCT       { get; set; }
        public string UOM { get; set; }
    }
}
