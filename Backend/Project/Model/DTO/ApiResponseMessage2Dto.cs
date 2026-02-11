using System.ComponentModel.DataAnnotations;

namespace Project.Model.DTO
{
    public class ApiResponseMessage2Dto
    {
        [Key]
        public string ResponseCode { get; set; }

        public string ResponseMessage { get; set; }

        public string ResponseValues { get; set; }
        public int BillCode { get; set; }

        public int IsSuccessful { get; set; }
    }
}
