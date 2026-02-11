using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddUpdVyapariBillDetailAuctionRepository : ISP_AddUpdVyapariBillDetailAuctionRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdVyapariBillDetailAuctionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdVyapariBillDetailAuction> input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            foreach (var SP_AddUpdVyapariBillDetailAuction in input)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddUpdVyapariBillDetailAuction]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@VBKID";
                    parameter.Value = SP_AddUpdVyapariBillDetailAuction.VBKID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DVBID";
                    parameter.Value = SP_AddUpdVyapariBillDetailAuction.DVBID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CROPTYPE";
                    parameter.Value = SP_AddUpdVyapariBillDetailAuction.CROPTYPE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@FARMERNAME";
                    parameter.Value = SP_AddUpdVyapariBillDetailAuction.FARMERNAME;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@JAALI";
                    parameter.Value = SP_AddUpdVyapariBillDetailAuction.JAALI;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@WEIGHT";
                    parameter.Value = SP_AddUpdVyapariBillDetailAuction.WEIGHT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@WEIGHTDIFFRENCE";
                    parameter.Value = SP_AddUpdVyapariBillDetailAuction.WEIGHTDIFFRENCE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@RATE";
                    parameter.Value = SP_AddUpdVyapariBillDetailAuction.RATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@TOTALAMOUNT";
                    parameter.Value = SP_AddUpdVyapariBillDetailAuction.TOTALAMOUNT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@COMPANYID";
                    parameter.Value = SP_AddUpdVyapariBillDetailAuction.COMPANYID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEPTID";
                    parameter.Value = SP_AddUpdVyapariBillDetailAuction.DEPTID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ISDELETED";
                    parameter.Value = SP_AddUpdVyapariBillDetailAuction.ISDELETED;
                    command.Parameters.Add(parameter);




                    dbContext.Database.OpenConnection();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new ApiResponseMessage
                            {
                                IsSuccessful = reader.GetInt32("IsSuccessful"),
                                ResponseCode = reader.GetString("ResponseCode"),
                                ResponseMessage = reader.GetString("ResponseMessage"),
                                ResponseValues = reader.GetString("ResponseValues")
                            });
                        }

                    }
                }
            }
            return result;
        }
    }
}
