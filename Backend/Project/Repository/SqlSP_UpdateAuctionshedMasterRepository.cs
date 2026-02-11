using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlSP_UpdateAuctionshedMasterRepository : ISP_UpdateAuctionshedMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_UpdateAuctionshedMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(List<SP_UpdateAuctionshedMaster> input)
        {
            List<ApiResponseMessage> list = new List<ApiResponseMessage>();
            foreach (var SP_UpdateAuctionshedMaster in input)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_UpdateAuctionshedMaster]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@APKID";
                    parameter.Value = SP_UpdateAuctionshedMaster.APKID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@BILLPDFID";
                    parameter.Value = SP_UpdateAuctionshedMaster.BILLPDFID;
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
            }
            return list;
        }
    }
}
