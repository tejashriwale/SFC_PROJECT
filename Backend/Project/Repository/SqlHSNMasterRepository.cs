using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.Model;

namespace Project.Repository
{
    public class SqlHSNMasterRepository : IHSNMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlHSNMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> HSNMasterAsync(HSNMaster hSNMaster)
        {
           List<ApiResponseMessage> responseMessages = new List<ApiResponseMessage>();

            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdHSNMaster]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@HAID";
                parameter.Value = hSNMaster.HAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@HCODE";
                parameter.Value = hSNMaster.HCODE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@HTYPE";
                parameter.Value = hSNMaster.HTYPE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@HDESCRIPTION";
                parameter.Value = hSNMaster.HDESCRIPTION;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@HIGST";
                parameter.Value = hSNMaster.HIGST;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@HSGST";
                parameter.Value = hSNMaster.HSGST;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@HCGST";
                parameter.Value = hSNMaster.HCGST;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@HCESS";
                parameter.Value = hSNMaster.HCESS;
                command.Parameters.Add(parameter);

                //parameter = command.CreateParameter();
                //parameter.ParameterName = "@COMPANYID";
                //parameter.Value = hSNMaster.COMPANYID;
                //command.Parameters.Add(parameter);

                //parameter = command.CreateParameter();
                //parameter.ParameterName = "@DEPTID";
                //parameter.Value = hSNMaster.DEPTID;
                //command.Parameters.Add(parameter);

                dbContext.Database.OpenConnection();
                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        responseMessages.Add(new ApiResponseMessage
                        {
                            IsSuccessful = reader.GetInt32("IsSuccessful"),
                            ResponseCode = reader.GetString("ResponseCode"),
                            ResponseMessage = reader.GetString("ResponseMessage"),
                            ResponseValues = reader.GetString("ResponseValues")
                        });
                    }
                }
            }
            return responseMessages;
        }
    }
}
