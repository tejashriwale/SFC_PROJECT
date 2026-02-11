namespace Project.Model.Domain
{
    public class SP_AddUpdAllTransections
    {
        public string TRAID     { get; set; }
        public int TRNSR     { get; set; }
        public string TRNSID    { get; set; }
        public string TRNTID    { get; set; }
        public string TRNDT     { get; set; }
        public string REFAID    { get; set; }
        public string MAID      { get; set; }
        public double PAMT      { get; set; }
        public double CBAL      { get; set; }
        public double ADVAMT    { get; set; }
        public string ASID      { get; set; }
        public Boolean ISDELETED { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }
        public string CROPID { get; set; }
        public string PAYMENTDT { get; set; }
    }
}
