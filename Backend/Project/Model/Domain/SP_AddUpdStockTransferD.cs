namespace Project.Model.Domain
{
    public class SP_AddUpdStockTransferD
    {
        public string STDID			 { get; set; }
        public string STMID			 { get; set; }
        public string STDPRODUCT		 { get; set; }
        public string STDHSNCODE		 { get; set; }
        public int STDREMS			 { get; set; }
        public string STDUOM			 { get; set; }
        public int STDQTY			 { get; set; }
        public double STDRATE			 { get; set; }
        public double STDTOTAL		 { get; set; }
        public string STDBATCHNO		 { get; set; }
        public string STDBATACHDATE	 { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }

    }
}
