using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlSP_UpdateVyapariDueRepository : ISP_UpdateVyapariDueRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_UpdateVyapariDueRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(List<SP_UpdateVyapariDue> input)
        {
            List<ApiResponseMessage> list = new List<ApiResponseMessage>();
            foreach (var SP_UpdateVyapariDue in input)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_UpdateVyapariDue]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@VDAID";
                    parameter.Value = SP_UpdateVyapariDue.VDAID;
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
