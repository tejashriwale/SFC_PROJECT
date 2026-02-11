using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_DeleteCompanyMasterRepository : ISP_DeleteCompanyMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_DeleteCompanyMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(SP_DeleteCompanyMaster input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_DeleteCompanyMaster]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@CPAID";
                parameter.Value = input.CPAID;
                command.Parameters.Add(parameter);


                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new ApiResponseMessage
                        {
                            ResponseCode = reader.GetString("ResponseCode"),
                            ResponseMessage = reader.GetString("ResponseMessage"),
                            ResponseValues = reader.GetString("ResponseValues"),
                            IsSuccessful = reader.GetInt32("IsSuccessful")
                        });
                    }
                }
            }
            return result;
        }
    }
}
