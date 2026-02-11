using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlSP_AddUpdFarmerBankDetailRepository : ISP_AddUpdFarmerBankDetailRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdFarmerBankDetailRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(SP_AddUpdFarmerBankDetail input)
        {
            List<ApiResponseMessage> list = new List<ApiResponseMessage>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdFarmerBankDetail]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@FAID";
                parameter.Value = input.FAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@FBANKNAME";
                parameter.Value = input.FBANKNAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@FACCOUNTNO";
                parameter.Value = input.FACCOUNTNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@FACCONAME";
                parameter.Value = input.FACCONAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@FIFSCCODE";
                parameter.Value = input.FIFSCCODE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UAID";
                parameter.Value = input.UAID;
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
                        list.Add(new ApiResponseMessage
                        {
                            IsSuccessful = reader.GetInt32("IsSuccessful"),
                            ResponseCode = reader.GetString("ResponseCode"),
                            ResponseMessage = reader.GetString("ResponseMessage"),
                            ResponseValues = reader.GetString("ResponseValues")

                        });
                    }
                }
            }
            return list;
        }
    }
}
