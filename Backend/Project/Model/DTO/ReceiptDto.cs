using Project.Model.Domain;
using Project.Repository;

namespace Project.Model.DTO
{
    public class ReceiptDto
    {
        public SP_AddUpdRECEIPTMaster ReceiptMaster { get; set; }
        public List<SP_AddUpdRECEIPTDetails> ReceiptDetails { get; set; }
        public SP_AddUpdAuctionshedMasterByBill AuctionMaster { get; set; }
        public List<SP_AddUpdAuctionShedDeatilByBill> AuctionDetails { get; set; }
        public List<SP_AddUpdVyapariBillMasterByBill> VyapariBillMasters { get; set; }
        public List<SP_AddUpdVyapariBillDetailByBill> VyapariBillDetails { get; set; }
    }
}
