namespace Project.Model.Domain
{
    public class GET_VyapariDue
    {
        public string VDAID		 { get; set; }
        public string TRNDT	 { get; set; }
        public double TAMT { get; set; }
        public double BPAMT		 { get; set; }
        public string FDUEDT		 { get; set; }
        public string SDUEDT		 { get; set; }
        public string VPAID		 { get; set; }
        public string TID			 { get; set; }
        public string STID		 { get; set; }
        public string TRNTID		 { get; set; }
        public string TRNSR		 { get; set; }
        public string REFKEY		 { get; set; }
        public string UAID		 { get; set; }
        public string VREFF		 { get; set; }
        public string REFF		 { get; set; }
        public string VTYPE		 { get; set; }
        public string VNAME { get; set; }
        public Boolean ISDELETED { get; set; }
        public Boolean ISCLOSE { get; set; }
    }
}
