namespace Project.Model.Domain
{
    public class SP_AddUpdVyapariApproval
    {
        public string VAID			 { get; set; }
        public string FARMERNAME	 { get; set; }
        public string TOKENNUMBER	 { get; set; }
        public Boolean ISAPPORVED	 { get; set; }
        public int JAALI			 { get; set; }
        public double RATE			 { get; set; }
        public double WEIGHT		 { get; set; }
        public double WEIGHTLESS	 { get; set; }
        public string COMPANYID		 { get; set; }
        public string DEPTID { get; set; }
        public string REASONS { get; set; }
        public string DATE { get; set; }
    }
}
