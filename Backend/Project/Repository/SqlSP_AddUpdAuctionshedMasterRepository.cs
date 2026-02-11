using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddUpdAuctionshedMasterRepository : ISP_AddUpdAuctionshedMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdAuctionshedMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage2>> getasync(SP_AddUpdAuctionshedMaster input)
        {
            List<ApiResponseMessage2> list = new List<ApiResponseMessage2>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdAuctionshedMaster]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@APKID";
                parameter.Value = input.APKID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@TOKEN";
                parameter.Value = input.TOKEN;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@BILLNO";
                parameter.Value = input.BILLNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DATE";
                parameter.Value = input.DATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@MAID";
                parameter.Value = input.MAID;
                command.Parameters.Add(parameter);


                parameter = command.CreateParameter();
                parameter.ParameterName = "@VILLAGE";
                parameter.Value = input.VILLAGE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SEVA";
                parameter.Value = input.SEVA;
                command.Parameters.Add(parameter);

              

                parameter = command.CreateParameter();
                parameter.ParameterName = "@TOTALJALI";
                parameter.Value = input.TOTALJALI;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@TOTALAMOUNT";
                parameter.Value = input.TOTALAMOUNT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@REMAININGAMOUNT";
                parameter.Value = input.REMAININGAMOUNT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@TOTALWEIGHT";
                parameter.Value = input.TOTALWEIGHT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@COMPANYID";
                parameter.Value = input.COMPANYID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DEPTID";
                parameter.Value = input.DEPTID;
                command.Parameters.Add(parameter);


                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new ApiResponseMessage2
                        {
                            IsSuccessful = reader.GetInt32("IsSuccessful"),
                            ResponseCode = reader.GetString("ResponseCode"),
                            ResponseMessage = reader.GetString("ResponseMessage"),
                            ResponseValues = reader.GetString("ResponseValues"),
                            BillCode = reader.GetInt32("BillCode"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
