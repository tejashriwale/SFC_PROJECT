using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlSP_UserLoginUpadateRepository : ISP_UserLoginUpadateRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_UserLoginUpadateRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(SP_UserLoginUpadate input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_UserLoginUpadate]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@UAID";
                parameter.Value = input.UAID;
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
