using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlSP_CheckCounterVoucherRepository :ISP_CheckCounterVoucherRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_CheckCounterVoucherRepository (ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(List<SP_CheckCounterVoucher> input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            foreach(var SP_CheckCounterVoucher in input)

                
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_CheckCounterVoucher]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@UAID";
                    parameter.Value = SP_CheckCounterVoucher.UAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@OrganizationID";
                    parameter.Value = SP_CheckCounterVoucher.OrganizationID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DivisionID";
                    parameter.Value = SP_CheckCounterVoucher.DivisionID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VoucherDate";
                    parameter.Value = SP_CheckCounterVoucher.VoucherDate;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@FARMERACC";
                    parameter.Value = SP_CheckCounterVoucher.FARMERACC;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CHECKACC";
                    parameter.Value = SP_CheckCounterVoucher.CHECKACC;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VoucherAmount";
                    parameter.Value = SP_CheckCounterVoucher.VoucherAmount;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ReferenceKey";
                    parameter.Value = SP_CheckCounterVoucher.ReferenceKey;
                    command.Parameters.Add(parameter);


                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VREFF";
                    parameter.Value = SP_CheckCounterVoucher.VREFF;
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
