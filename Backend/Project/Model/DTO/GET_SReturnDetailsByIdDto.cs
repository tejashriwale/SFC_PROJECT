using System.ComponentModel.DataAnnotations;

namespace Project.Model.DTO
{
    public class GET_SReturnDetailsByIdDto
    {
        [Key]
        public string SRDAID { get; set; }
        public string SRAID { get; set; }
        public string SRDPRODUCT { get; set; }
        public string SRDUOM { get; set; }
        public string SRDQUANTITY { get; set; }
        public double SRTDAMT { get; set; }
        public double SRDIGST { get; set; }
        public double SRDSGST { get; set; }
        public double SRDCGST { get; set; }
        public string SRDQUANTITYRETURN { get; set; }
        public int HSNCODE { get; set; }
        public string BATCHNO { get; set; }
        public string EXPIRYDATE { get; set; }
        public string UOMTITLE { get; set; }
        public string PRODUCTNAME { get; set; }
        public Boolean ISDELETED { get; set; }
    }
}
