namespace Project.Model.DTO
{
    public class GET_VehicalDetailsByIdDto
    {
        public string VDAID { get; set; }
        public string TAID { get; set; }
        public string VNO { get; set; }
        public string VTYPE { get; set; }
        public string VCAPACITY { get; set; }
        public Boolean ISDELETED { get; set; }
    }
}
