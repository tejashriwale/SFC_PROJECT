using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlSP_UpdateVyapariBillMasterRepository : ISP_UpdateVyapariBillMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_UpdateVyapariBillMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(List<SP_UpdateVyapariBillMaster> input)
        {
            List<ApiResponseMessage> list = new List<ApiResponseMessage>();
            foreach (var SP_UpdateVyapariBillMaster in input)
            {

                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_UpdateVyapariBillMaster]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@VBKID";
                    parameter.Value = SP_UpdateVyapariBillMaster.VBKID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@BILLPDFID";
                    parameter.Value = SP_UpdateVyapariBillMaster.BILLPDFID;
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
