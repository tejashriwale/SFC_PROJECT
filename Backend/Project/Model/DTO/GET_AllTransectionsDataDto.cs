namespace Project.Model.DTO
{
    public class GET_AllTransectionsDataDto
    {
        public string TRAID { get; set; }
        public int TRNSR { get; set; }
    
        public string REFAID { get; set; }
        public string MAID { get; set; }
        public double PAMT { get; set; }
        public double CBAL { get; set; }
        public double ADVAMT { get; set; }
        public string TRNTID { get; set; }
        public Boolean STATUS { get; set; }
    }
}
