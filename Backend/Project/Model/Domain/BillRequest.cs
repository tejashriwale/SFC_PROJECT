namespace Project.Model.Domain
{
    public class BillRequest
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string Crates { get; set; }
        public string Weight { get; set; }
        public string Amount { get; set; }
    }
}
