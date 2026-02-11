namespace Project.Model.Domain
{
    public class SP_AddCashInvert
    {
        public string CASID { get; set; }
        public string CASDATE { get; set; }
        public string CASBANKNAME { get; set; }
        public int CASINVERTCASH { get; set; }
        public int CAS2000 { get; set; }  // ✅ सुधारलेले नाव
        public int CAS500 { get; set; }   // ✅ सुधारलेले नाव
        public int CAS200 { get; set; }   // ✅ सुधारलेले नाव
        public int CAS100 { get; set; }   // ✅ सुधारलेले नाव
        public int CAS50 { get; set; }    // ✅ सुधारलेले नाव
        public int CAS20 { get; set; }    // ✅ सुधारलेले नाव
        public int CAS10 { get; set; }    // ✅ सुधारलेले नाव
        public int CASCOINS { get; set; }
        public int CASTOTAL { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }
        public string UAID { get; set; }
    }
}
