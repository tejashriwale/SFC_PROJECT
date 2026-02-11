using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddUpdAdvertisementRepository : ISP_AddUpdAdvertisementRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdAdvertisementRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(SP_AddUpdAdvertisement input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdAdvertisement]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@ADid";
                parameter.Value = input.ADid;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@LOGTYPE";
                parameter.Value = input.LOGTYPE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ADtitle";
                parameter.Value = input.ADtitle;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ADimage";
                parameter.Value = input.ADimage;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ISACTIVE";
                parameter.Value = input.ISACTIVE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@COMPANYID";
                parameter.Value = input.COMPANYID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DEPTID";
                parameter.Value = input.DEPTID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UAID";
                parameter.Value = input.UAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DATE";
                parameter.Value = input.DATE;
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
