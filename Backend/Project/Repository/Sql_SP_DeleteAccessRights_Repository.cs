using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class Sql_SP_DeleteAccessRights_Repository : I_SP_DeleteAccessRights_Repository
    {
        private readonly ApplicationDbContext dbContext;

        public Sql_SP_DeleteAccessRights_Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(_SP_DeleteAccessRights_ sP_DeleteAccessRights_)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_DeleteAccessRights]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@AccessRightsAutoID1";
                parameter.Value = sP_DeleteAccessRights_.AccessRightsAutoID1;
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
