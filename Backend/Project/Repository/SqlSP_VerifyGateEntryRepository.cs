using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_VerifyGateEntryRepository : ISP_VerifyGateEntryRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_VerifyGateEntryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(List<SP_VerifyGateEntry> input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            foreach (var SP_VerifyGateEntry in input)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_VerifyGateEntry]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@DPKID";
                    parameter.Value = SP_VerifyGateEntry.DPKID;
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
