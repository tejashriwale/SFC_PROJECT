using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddUpdAccountsRepository : ISP_AddUpdAccountsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdAccountsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(SP_AddUpdAccounts input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdAccounts]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@ACCAID";
                parameter.Value = input.ACCAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ACCGID";
                parameter.Value = input.ACCGID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@MGRPID";
                parameter.Value = input.MGRPID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SGRPID";
                parameter.Value = input.SGRPID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@GLRPID";
                parameter.Value = input.GLRPID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SGLRPID";
                parameter.Value = input.SGLRPID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@LevelID";
                parameter.Value = input.LevelID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ACCTM";
                parameter.Value = input.ACCTM;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ACCTE";
                parameter.Value = input.ACCTE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@TRNModeID";
                parameter.Value = input.TRNModeID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@RELID";
                parameter.Value = input.RELID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UAID";
                parameter.Value = input.UAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@COMPANYID";
                parameter.Value = input.COMPANYID;
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
