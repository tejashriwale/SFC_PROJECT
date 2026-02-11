namespace Project.Model.DTO
{
    public class GET_PRequisitionDetailsDto
    {
        public string PRDAID { get; set; }
        public string PRAID { get; set; }
        public string PRDPRODUCT { get; set; }
        public string PRDDESCRIPTION { get; set; }
        public string PRDQUANTITY { get; set; }
        public string PRDUOM { get; set; }
        public string PNAME { get; set; }
        public double PIGST { get; set; }
        public double PSGST { get; set; }
        public double PCGST { get; set; }
        public double PCESS { get; set; }
        public string UOMTITLE { get; set; }
        public double RATE { get; set; }
        public int HSNCODE { get; set; }
        public int PRBASEQTY { get; set; }
    }
}
