namespace Project.Model.Domain
{
    public class SP_AddUpdColdStorageInward
    {
        public string CSID			 { get; set; }
        public string COLDSTORAGEID	 { get; set; }
        public string LOTNO			 { get; set; }
        public string DATE			 { get; set; }
        public string CUSTOMERNAME	 { get; set; }
        public string THIRDPARTYNAME { get; set; }
        public string ITEM	         { get; set; }
       
        public string TYPEOFVARITY   { get; set; }
        public string RACKPOSITION   { get; set; }
        public string COMPANYID		 { get; set; }
        public string DEPTID { get; set; }
        public string UAID { get; set; }
        public double GROSSWEIGHT { get; set; }
        public double NETWEIGHT { get; set; }
        public string WEIGHTUNIT { get; set; }
        public double WEIGHTINKG { get; set; }
        public double CARETWEIGHT { get; set; }
        public Boolean STATUS { get; set; }

    }
}
