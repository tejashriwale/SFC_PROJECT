namespace Project.Model.Domain
{
    public class SP_AddUpdHRMPayroll
    {
        public string PAID { get; set; }
        public string PEMPLOYEE			 { get; set; }
        public double PBASICSALARY		 { get; set; }
        public Boolean PSTATUS			 { get; set; }
        public double PHRAALLOWANCE		 { get; set; }
        public double PCONVEYANCE		 { get; set; }
        public double PMEDICALALLOWANCE	 { get; set; }
        public double PBONUS			 { get; set; }
        public double PALLOWANCESOTHER	 { get; set; }
        public double PPF				 { get; set; }
        public double PPROFESSIONALTAX	 { get; set; }
        public double PTDS				 { get; set; }
        public double PLOANANDOTHER		 { get; set; }
        public double PDEDUCTIONOTHER	 { get; set; }
        public double PTOTALALLOWANCE	 { get; set; }
        public double PTOTALDEDUCTION	 { get; set; }
        public double PNETSALAREY		 { get; set; }
        public string COMPANYID			 { get; set; }
        public string DEPTID { get; set; }
    }
}
