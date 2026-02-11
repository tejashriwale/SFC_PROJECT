using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddUpdHRMDesignationRepository : ISP_AddUpdHRMDesignationRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdHRMDesignationRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(SP_AddUpdHRMDesignation sP_AddUpdHRMDesignation)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdHRMDesignation]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@DAID";
                parameter.Value = sP_AddUpdHRMDesignation.DAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DNAME";
                parameter.Value = sP_AddUpdHRMDesignation.DNAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DDATE";
                parameter.Value = sP_AddUpdHRMDesignation.DDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DSTATUS";
                parameter.Value = sP_AddUpdHRMDesignation.DSTATUS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@COMPANYID";
                parameter.Value = sP_AddUpdHRMDesignation.COMPANYID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DEPTID";
                parameter.Value = sP_AddUpdHRMDesignation.DEPTID;
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
