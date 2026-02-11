namespace Project.Model.DTO
{
    public class GET_SReturnBillNoDto
    {
        public string SBNO { get; set; }
        public string SBBILLNO { get; set; }
        public string BillQuantity { get; set; }
        public string ReturnQuantity { get; set; }
        public int AVAILABLE_FOR_RETURN { get; set; }
    }
}
