namespace Project.Model.DTO
{
    public class GET_Report_DayAuctionDto
    {
        public string DATE { get; set; }
        public int TOTAL_FARMERS { get; set; }
        public decimal TOTAL_QUINTAL { get; set; }
        public decimal TOTAL_RATE { get; set; }
        public decimal MAX_RATE { get; set; }
        public decimal MIN_RATE { get; set; }
        public int TOTAL_JALI { get; set; }
        public string AllCropTypes { get; set; }
        public string Weightless { get; set; }
        public string Weight { get; set; }
        public string Rate { get; set; }
        public string CropLabels { get; set; }
        public string ASID { get; set; }
        public decimal TOTALAMOUNT { get; set; }
        public decimal SUPER_FEE { get; set; }
        public decimal MARKET_FEE { get; set; }
    }
}
