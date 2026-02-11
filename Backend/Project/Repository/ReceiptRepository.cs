using Project.Data;
using Project.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace Project.Repository
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ReceiptRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddOrUpdateReceiptAsync(ReceiptRequest request)
        {
            if (request == null || request.ReceiptMaster == null)
                throw new ArgumentNullException(nameof(request));

            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                // RECEIPT MASTER
                await _dbContext.Database.ExecuteSqlInterpolatedAsync($@"
                    EXEC SP_AddUpdRECEIPTMaster 
                        @AADHARNO = {request.ReceiptMaster.AADHARNO},
                        @ACCNAME = {request.ReceiptMaster.ACCNAME},
                        @ACCNO = {request.ReceiptMaster.ACCNO},
                        @BAID = {request.ReceiptMaster.BAID},
                        @BANKNAME = {request.ReceiptMaster.BANKNAME},
                        @BILLNO = {request.ReceiptMaster.BILLNO},
                        @BTOKANNO = {request.ReceiptMaster.BTOKANNO},
                        @COMPANYID = {request.ReceiptMaster.COMPANYID},
                        @DATE = {request.ReceiptMaster.DATE},
                        @DEPTID = {request.ReceiptMaster.DEPTID},
                        @IFSCCODE = {request.ReceiptMaster.IFSCCODE},
                        @PHONENUMBER = {request.ReceiptMaster.PHONENUMBER},
                        @REMAININGAMOUNT = {request.ReceiptMaster.REMAININGAMOUNT},
                        @SEVA = {request.ReceiptMaster.SEVA},
                        @TOTALAMOUNT = {request.ReceiptMaster.TOTALAMOUNT},
                        @TOTALCARETS = {request.ReceiptMaster.TOTALCARETS},
                        @TOTALCOST = {request.ReceiptMaster.TOTALCOST},
                        @TOTALWEIGHT = {request.ReceiptMaster.TOTALWEIGHT},
                        @TRNTID = {request.ReceiptMaster.TRNTID},
                        @UAID = {request.ReceiptMaster.UAID},
                        @VEHICALRENT = {request.ReceiptMaster.VEHICALRENT},
                        @VEHICLENO = {request.ReceiptMaster.VEHICLENO},
                        @VILLAGENAME = {request.ReceiptMaster.VILLAGENAME}
                ");

                // RECEIPT DETAILS
                if (request.ReceiptDetails != null)
                {
                    foreach (var detail in request.ReceiptDetails)
                    {
                        await _dbContext.Database.ExecuteSqlInterpolatedAsync($@"
                            EXEC SP_AddUpdRECEIPTDetails 
                                @BDAID = {detail.BDAID},
                                @BAID = {detail.BAID},
                                @CROPTYPE = {detail.CROPTYPE},
                                @AMOUNT = {detail.AMOUNT},
                                @COMPANYID = {detail.COMPANYID},
                                @DEPTID = {detail.DEPTID},
                                @ISDELETED = {detail.ISDELETED},
                                @JALI = {detail.JALI},
                                @RATE = {detail.RATE},
                                @UAID = {detail.UAID},
                                @VYAPARINAME = {detail.VYAPARINAME},
                                @WEIGHT = {detail.WEIGHT},
                                @WEIGHTMINMAX = {detail.WEIGHTMINMAX}
                        ");
                    }
                }

                // AUCTION MASTER
                if (request.AuctionMaster != null)
                {
                    await _dbContext.Database.ExecuteSqlInterpolatedAsync($@"
                        EXEC SP_AddUpdAuctionshedMasterByBill 
                            @APKID = {request.AuctionMaster.APKID},
                            @TOKEN = {request.AuctionMaster.TOKEN},
                            @BILLNO = {request.AuctionMaster.BILLNO},
                            @DATE = {request.AuctionMaster.DATE},
                            @COMPANYID = {request.AuctionMaster.COMPANYID},
                            @DEPTID = {request.AuctionMaster.DEPTID},
                            @MAID = {request.AuctionMaster.MAID},
                            @REMAININGAMOUNT = {request.AuctionMaster.REMAININGAMOUNT},
                            @SEVA = {request.AuctionMaster.SEVA},
                            @TOTALAMOUNT = {request.AuctionMaster.TOTALAMOUNT},
                            @TOTALJALI = {request.AuctionMaster.TOTALJALI},
                            @TOTALWEIGHT = {request.AuctionMaster.TOTALWEIGHT},
                            @UAID = {request.AuctionMaster.UAID},
                            @VILLAGE = {request.AuctionMaster.VILLAGE}
                    ");
                }

                // AUCTION DETAILS
                if (request.AuctionDetails != null)
                {
                    foreach (var auction in request.AuctionDetails)
                    {
                        await _dbContext.Database.ExecuteSqlInterpolatedAsync($@"
                            EXEC SP_AddUpdAuctionShedDeatilByBill 
                                @DPKID = {auction.DPKID},
                                @APKID = {auction.APKID},
                                @VYAPARI = {auction.VYAPARI},
                                @COMPANYID = {auction.COMPANYID},
                                @DEPTID = {auction.DEPTID},
                                @ISDELETED = {auction.ISDELETED},
                                @JAALI = {auction.JAALI},
                                @RATE = {auction.RATE},
                                @WEIGHT = {auction.WEIGHT},
                                @WEIGHTLESS = {auction.WEIGHTLESS}
                        ");
                    }
                }

                // VYAPARI BILL MASTER
                if (request.VyapariBillMasters != null)
                {
                    foreach (var master in request.VyapariBillMasters)
                    {
                        await _dbContext.Database.ExecuteSqlInterpolatedAsync($@"
                            EXEC SP_AddUpdVyapariBillMasterByBill 
                                @VBKID = {master.VBKID},
                                @BILLNO = {master.BILLNO},
                                @APKID = {master.APKID},
                                @DATE = {master.DATE},
                                @VYAPARINAME = {master.VYAPARINAME},
                                @COMPANYID = {master.COMPANYID},
                                @DEPTID = {master.DEPTID},
                                @ISDELETED = {master.ISDELETED},
                                @REMAININGAMOUNT = {master.REMAININGAMOUNT},
                                @SEVA = {master.SEVA},
                                @TOTALAMOUNT = {master.TOTALAMOUNT},
                                @TOTALJAALI = {master.TOTALJAALI},
                                @TOTALLOSS = {master.TOTALLOSS},
                                @TOTALWEIGHT = {master.TOTALWEIGHT},
                                @UAID = {master.UAID}
                        ");
                    }
                }

                // VYAPARI BILL DETAILS
                if (request.VyapariBillDetails != null)
                {
                    foreach (var detail in request.VyapariBillDetails)
                    {
                        await _dbContext.Database.ExecuteSqlInterpolatedAsync($@"
                            EXEC SP_AddUpdVyapariBillDetailByBill 
                                @DVBID = {detail.DVBID},
                                @VBKID = {detail.VBKID},
                                @COMPANYID = {detail.COMPANYID},
                                @DEPTID = {detail.DEPTID},
                                @CROPTYPE = {detail.CROPTYPE},
                                @FARMERNAME = {detail.FARMERNAME},
                                @JAALI = {detail.JAALI},
                                @RATE = {detail.RATE},
                                @WEIGHT = {detail.WEIGHT},
                                @TOTALAMOUNT = {detail.TOTALAMOUNT},
                                @WEIGHTDIFFRENCE = {detail.WEIGHTDIFFRENCE},
                                @ISDELETED = {detail.ISDELETED}
                        ");
                    }
                }

                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}