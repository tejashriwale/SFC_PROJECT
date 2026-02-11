using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlAccessRightsRepository : IAccessRightsRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlAccessRightsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(AccessRights accessRights)
        {
            List<ApiResponseMessage> apiResponseMessages = new List<ApiResponseMessage>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdAccessRights]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@AccessRightsAutoID1";
                parameter.Value = accessRights.AccessRightsAutoID1;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@AccessRightID";
                parameter.Value = accessRights.@AccessRightID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@AccessRightTitle";
                parameter.Value = accessRights.@AccessRightTitle;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@AccessRightDescription";
                parameter.Value = accessRights.@AccessRightDescription;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ApplicationGroup";
                parameter.Value = accessRights.@ApplicationGroup;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@AccessRightGroup";
                parameter.Value = accessRights.@AccessRightGroup;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@IsAdminRight";
                parameter.Value = accessRights.@IsAdminRight;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@COMPANYID";
                parameter.Value = accessRights.COMPANYID;
                command.Parameters.Add(parameter);


                dbContext.Database.OpenConnection();

                using(var reader= await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        apiResponseMessages.Add( new ApiResponseMessage
                        {
                            IsSuccessful = reader.GetInt32("IsSuccessful"),
                            ResponseCode = reader.GetString("ResponseCode"),
                            ResponseMessage = reader.GetString("ResponseMessage"),
                            ResponseValues = reader.GetString("ResponseValues")
                        });
                    
                    }
                }


            }
            return apiResponseMessages;
        }

    }
}
