using System.ComponentModel.DataAnnotations;

namespace Project.Model.Domain
{
    public class ApiResponseMessage2
    {
        [Key]
        public string ResponseCode { get; set; }

        public string ResponseMessage { get; set; }

        public string ResponseValues { get; set; }
        public int BillCode { get; set; }

        public int IsSuccessful { get; set; }
    }
}
