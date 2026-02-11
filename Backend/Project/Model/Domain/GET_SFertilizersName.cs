namespace Project.Model.Domain
{
    public class GET_SFertilizersName
    {
        public string DSFID { get; set; }
        public string SRAID	 { get; set; }
        public string FERTILIZERSNAME	 { get; set; }
        public double QUANTITY	 { get; set; }
        public double COST	 { get; set; }
        public string FTYPE	 { get; set; }

        public string FNAME { get; set; }
        public Boolean ISDELETED { get; set; }
    }
}
