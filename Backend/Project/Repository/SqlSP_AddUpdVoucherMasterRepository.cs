using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddUpdVoucherMasterRepository : ISP_AddUpdVoucherMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdVoucherMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(SP_AddUpdVoucherMaster input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdVoucherMaster]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@VMKID";
                parameter.Value = input.VMKID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DEPOSITE";
                parameter.Value = input.DEPOSITE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@REFERENCE";
                parameter.Value = input.REFERENCE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@AMOUNT";
                parameter.Value = input.AMOUNT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@VOUCHERTYPE";
                parameter.Value = input.VOUCHERTYPE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@JAMANAVE";
                parameter.Value = input.JAMANAVE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@REMARK";
                parameter.Value = input.REMARK;
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
            return result;
        }
    }
}
