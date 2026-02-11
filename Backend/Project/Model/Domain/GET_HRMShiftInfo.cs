namespace Project.Model.Domain
{
    public class GET_HRMShiftInfo
    {
        public string SHID			 { get; set; }
        public string SHNAME			 { get; set; }
        public string SHFROMTIME		 { get; set; }
        public string SHTOTIME		 { get; set; }
        public string WEEKOFF			 { get; set; }
        public string SHDESCRIPTION	 { get; set; }
        public Boolean SHSTATUS		 { get; set; }
        public string MORNINGBREAKFROM { get; set; }
        public string MORNINGBREAKTO	 { get; set; }
        public string LUNCHFROM		 { get; set; }
        public string LUNCHTO			 { get; set; }
        public string EVENINGBREAKFROM { get; set; }
        public string EVENINGBREAKTO { get; set; }
        public string WEEKDAY { get; set; }


    }
}
