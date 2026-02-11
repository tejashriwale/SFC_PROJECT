namespace Project.Model.Domain
{
    public class GET_SQuatationDetails
    {
        public string SQAMDAID { get; set; }
        public string SQAMAID { get; set; }
        public string SQPRODUCT { get; set; }
        public string SQQUANTITY { get; set; }
        public string SQUOM { get; set; }
        public double SQRATE { get; set; }
        public double SQTAMT { get; set; }
        public double SQIGST { get; set; }
        public double SQSGST { get; set; }
        public double SQCGST { get; set; }
        public double SQCESS { get; set; }
        public int HSNCODE { get; set; }
        public double SQTAXBALEAMOUNT { get; set; }
        public Boolean IsDeleted { get; set; }

    }
}
