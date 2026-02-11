using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_CheckOTPRepository : ISP_CheckOTPRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_CheckOTPRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(SP_CheckOTP input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_CheckOTP]";
                command.CommandType = System.Data.CommandType.StoredProcedure;



                var parameter = command.CreateParameter();
                parameter.ParameterName = "@PKOID";
                parameter.Value = input.PKOID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@OTP";
                parameter.Value = input.OTP;
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
