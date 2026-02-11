using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.Model;

namespace Project.Repository
{
    public class SqlGSTUPDATERepository : IGSTUPDATERepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlGSTUPDATERepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(GSTUPDATE gSTUPDATE)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();

            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_UPDATEHSNGST]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@HCODE";
                parameter.Value = gSTUPDATE.HCODE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@HIGST";
                parameter.Value = gSTUPDATE.HIGST;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@HSGST";
                parameter.Value = gSTUPDATE.HSGST;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@HCGST";
                parameter.Value = gSTUPDATE.HCGST;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@HCESS";
                parameter.Value = gSTUPDATE.HCESS;
                command.Parameters.Add(parameter);

                dbContext.Database.OpenConnection();

                using(var reader = await  command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
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
