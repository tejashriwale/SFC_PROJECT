namespace Project.Model.DTO
{
    public class GET_HRM_HolidayDto
    {
        public string HAID { get; set; }
        public string ADDHOLIDAY { get; set; }
        public string SDATE { get; set; }
        public string EDATE { get; set; }
        public int NODAYS { get; set; }
        public Boolean HASTATUS { get; set; }
    }
}
