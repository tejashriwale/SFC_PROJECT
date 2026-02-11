using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_Report_DayAuctionRepository : IGET_Report_DayAuctionRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_Report_DayAuctionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_Report_DayAuction>> getasync(GET_Report_DayAuctionDomain input)
        {
            {
                List<GET_Report_DayAuction> result = new List<GET_Report_DayAuction>();
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [GET_Report_DayAuction]('" + input.STARTDATE + "' , '" + input.ENDTDATE + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                    dbContext.Database.OpenConnection();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new GET_Report_DayAuction
                            {

                                DATE = reader.GetString("DATE"),
                                TOTAL_FARMERS = reader.GetInt32("TOTAL_FARMERS"),
                                TOTAL_QUINTAL = reader.GetDecimal("TOTAL_QUINTAL"),
                                TOTAL_RATE = reader.GetDecimal("TOTAL_RATE"),
                                MAX_RATE = reader.GetDecimal("MAX_RATE"),
                                MIN_RATE = reader.GetDecimal("MIN_RATE"),
                                TOTAL_JALI = reader.GetInt32("TOTAL_JALI"),
                                AllCropTypes = reader.GetString("AllCropTypes"),
                                Weightless = reader.GetString("Weightless"),
                                Weight = reader.GetString("Weight"),
                                Rate = reader.GetString("Rate"),
                                CropLabels = reader.GetString("CropLabels"),
                                ASID = reader.GetString("ASID"),
                                TOTALAMOUNT = reader.GetDecimal("TOTALAMOUNT"),
                                SUPER_FEE = reader.GetDecimal("SUPER_FEE"),
                                MARKET_FEE = reader.GetDecimal("MARKET_FEE"),



                            });
                        }
                    }

                }
                return result;
            }
        }
    }
}
