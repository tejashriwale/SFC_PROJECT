using System.ComponentModel.DataAnnotations;

namespace Project.Model.Domain
{
    public class _GET_RackMaster_
    {
        [Key]
        public string RAID { get; set; }
        public string RTYPE { get; set; }
        public string RDESCRIPTION { get; set; }

        public string RCODE { get; set; }
        public string RROWNUMBER { get; set; }

        public string RNUMBER { get; set; }
        public string RSHELF { get; set; }
       
    }
}
