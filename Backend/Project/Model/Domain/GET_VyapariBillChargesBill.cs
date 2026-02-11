namespace Project.Model.Domain
{
    public class GET_VyapariBillChargesBill
    {
        public string SEVAID { get; set; }
        public decimal TotalAmount { get; set; }
        public string SERVICETYPE { get; set; }
        public string RateTypeTitle { get; set; }
        public string RateTypeValue { get; set; }
    }
}
