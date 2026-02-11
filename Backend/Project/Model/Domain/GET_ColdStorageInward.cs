namespace Project.Model.Domain
{
    public class GET_ColdStorageInward
    {
        public string CSID		 { get; set; }
        public string COLDSTORAGEID { get; set; }
        public string LOTNO { get; set; }
        public string DATE { get; set; }
        public string CUSTOMERNAME { get; set; }
        public string THIRDPARTYNAME { get; set; }
        public string ITEM { get; set; }
      
        public string TYPEOFVARITY { get; set; }
        public string RACKPOSITION { get; set; }
        public string CNAME { get; set; }
        public string PRODUCT { get; set; }
        public string VAJAN { get; set; }
        public double GROSSWEIGHT { get; set; }
        public double NETWEIGHT { get; set; }
        public string WEIGHTUNIT { get; set; }
        public double WEIGHTINKG { get; set; }
        public double CARETWEIGHT { get; set; }
    }
}
