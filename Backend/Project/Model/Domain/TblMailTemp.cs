using System.ComponentModel.DataAnnotations;

namespace Project.Model.Domain
{
    public class TblMailTemp
    {
        [Key]
        public string MBODY { get; set; }
        public string SUBJECT { get; set; }
    }
}
