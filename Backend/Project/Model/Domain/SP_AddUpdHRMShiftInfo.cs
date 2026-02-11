namespace Project.Model.Domain
{
    public class SP_AddUpdHRMShiftInfo
    {
        public string SHID { get; set; }
        public string SHNAME			 { get; set; }
        public TimeOnly SHFROMTIME		 { get; set; }
        public TimeOnly SHTOTIME			 { get; set; }
        public string WEEKOFF			 { get; set; }
        public string SHDESCRIPTION		 { get; set; }
        public Boolean SHSTATUS			 { get; set; }
        public string MORNINGBREAKFROM	 { get; set; }
        public string MORNINGBREAKTO	 { get; set; }
        public string LUNCHFROM			 { get; set; }
        public string LUNCHTO			 { get; set; }
        public string EVENINGBREAKFROM	 { get; set; }
        public string EVENINGBREAKTO	 { get; set; }
        public string COMPANYID			 { get; set; }
        public string DEPTID { get; set; }
        
    }
}
