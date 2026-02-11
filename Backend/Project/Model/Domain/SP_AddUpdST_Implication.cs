namespace Project.Model.Domain
{
    public class SP_AddUpdST_Implication
    {
        public string ImplicationAutoID { get; set; }
        public string ImplicationTitle { get; set; }
        public string ImplicationValue { get; set; }
        public string ImplicationGroup { get; set; }
        public int ImplicationOrder { get; set; }
        public Boolean IsSystemDefined	 { get; set; }
        public Boolean IsIgnored { get; set; }
    
    }
}
