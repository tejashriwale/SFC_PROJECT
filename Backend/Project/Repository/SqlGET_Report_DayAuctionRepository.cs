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
                    command.CommandText = "SELECT * FROM [GET_Report_DayAuction](@p0 , @p1 , @p2 , @p3)";
                    { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.STARTDATE) ?? ""); command.Parameters.Add(command_p0); }
                    { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.ENDTDATE) ?? ""); command.Parameters.Add(command_p1); }
                    { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                    { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
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
