namespace Project.Model.Domain
{
    public class GET_HRM_Empoye_Leaves
    {
        public string ELID		 { get; set; }
        public Boolean ELDAYS		 { get; set; }
        public string ELLEAVEDATE	 { get; set; }
        public string ELLEAVETYPE	 { get; set; }
        public string ELDAYTYPE	 { get; set; }
        public string ELREASON	 { get; set; }
        public string ELSDT	 { get; set; }
        public string ELEDT	 { get; set; }
        public int ELTDAYS	 { get; set; }
        public string ELMLEAVETYPE { get; set; }
        public string ELMREASON { get; set; }
        public string LeaveStatus { get; set; }

    }
}
