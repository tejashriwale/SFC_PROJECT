namespace Project.Model.Domain
{
    public class GET_PrecoolingSlab
    {
        public string CSID { get; set; }
        public string StartDate { get; set; }
        public int DaysFrom { get; set; }
        public int DaysTo { get; set; }
        public string SlabStartDate { get; set; }
        public string SlabEndDate { get; set; }
        public double Rate { get; set; }
        public double WEIGHTINKG { get; set; }
        public int DaysInSlab { get; set; }
        public double Total { get; set; }
        public double OUTWARDKG { get; set; }
        public double REMAININGS { get; set; }
      
    }
}
