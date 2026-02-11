using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddUpdVyapariBillMasterSaveAuctionRepository : ISP_AddUpdVyapariBillMasterSaveAuctionRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdVyapariBillMasterSaveAuctionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdVyapariBillMasterSaveAuction> input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            foreach (var SP_AddUpdVyapariBillMasterSaveAuction in input)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddUpdVyapariBillMasterSaveAuction]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@VBKID";
                    parameter.Value = SP_AddUpdVyapariBillMasterSaveAuction.VBKID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@BILLNO";
                    parameter.Value = SP_AddUpdVyapariBillMasterSaveAuction.BILLNO;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DATE";
                    parameter.Value = SP_AddUpdVyapariBillMasterSaveAuction.DATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VYAPARINAME";
                    parameter.Value = SP_AddUpdVyapariBillMasterSaveAuction.VYAPARINAME;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SHORTNAME";
                    parameter.Value = SP_AddUpdVyapariBillMasterSaveAuction.SHORTNAME;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@COMPANYNAME";
                    parameter.Value = SP_AddUpdVyapariBillMasterSaveAuction.COMPANYNAME;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PHONE";
                    parameter.Value = SP_AddUpdVyapariBillMasterSaveAuction.PHONE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@AADHAR";
                    parameter.Value = SP_AddUpdVyapariBillMasterSaveAuction.AADHAR;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@BANKNAME";
                    parameter.Value = SP_AddUpdVyapariBillMasterSaveAuction.BANKNAME;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ACCOUNTNUMBER";
                    parameter.Value = SP_AddUpdVyapariBillMasterSaveAuction.ACCOUNTNUMBER;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@IFSCCODE";
                    parameter.Value = SP_AddUpdVyapariBillMasterSaveAuction.IFSCCODE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ACCOUNTNAME";
                    parameter.Value = SP_AddUpdVyapariBillMasterSaveAuction.ACCOUNTNAME;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@BRANCHNAME";
                    parameter.Value = SP_AddUpdVyapariBillMasterSaveAuction.BRANCHNAME;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@TOTALJAALI";
                    parameter.Value = SP_AddUpdVyapariBillMasterSaveAuction.TOTALJAALI;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@TOTALWEIGHT";
                    parameter.Value = SP_AddUpdVyapariBillMasterSaveAuction.TOTALWEIGHT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@TOTALAMOUNT";
                    parameter.Value = SP_AddUpdVyapariBillMasterSaveAuction.TOTALAMOUNT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@TOTALLOSS";
                    parameter.Value = SP_AddUpdVyapariBillMasterSaveAuction.TOTALLOSS;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@REMAININGAMOUNT";
                    parameter.Value = SP_AddUpdVyapariBillMasterSaveAuction.REMAININGAMOUNT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@COMPANYID";
                    parameter.Value = SP_AddUpdVyapariBillMasterSaveAuction.COMPANYID;
                    command.Parameters.Add(parameter);


                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEPTID";
                    parameter.Value = SP_AddUpdVyapariBillMasterSaveAuction.DEPTID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SEVA";
                    parameter.Value = SP_AddUpdVyapariBillMasterSaveAuction.SEVA;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@APKID";
                    parameter.Value = SP_AddUpdVyapariBillMasterSaveAuction.APKID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UAID";
                    parameter.Value = SP_AddUpdVyapariBillMasterSaveAuction.UAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ISDELETED";
                    parameter.Value = SP_AddUpdVyapariBillMasterSaveAuction.ISDELETED;
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
