using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddUpdPDeficitDetailsRepository : ISP_AddUpdPDeficitDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdPDeficitDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdPDeficitDetails> input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            foreach (var SP_AddUpdPDeficitDetails in input)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddUpdPDeficitDetails]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@DSID";
                    parameter.Value = SP_AddUpdPDeficitDetails.DSID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PKID";
                    parameter.Value = SP_AddUpdPDeficitDetails.PKID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PRODUCTNAME";
                    parameter.Value = SP_AddUpdPDeficitDetails.PRODUCTNAME;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UNIT";
                    parameter.Value = SP_AddUpdPDeficitDetails.UNIT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@QUANTITY";
                    parameter.Value = SP_AddUpdPDeficitDetails.QUANTITY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PRICE";
                    parameter.Value = SP_AddUpdPDeficitDetails.PRICE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@TOTAL";
                    parameter.Value = SP_AddUpdPDeficitDetails.TOTAL;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@COMPANYID";
                    parameter.Value = SP_AddUpdPDeficitDetails.COMPANYID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEPTID";
                    parameter.Value = SP_AddUpdPDeficitDetails.DEPTID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@REMAININGSTOCK";
                    parameter.Value = SP_AddUpdPDeficitDetails.REMAININGSTOCK;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@BATCHNO";
                    parameter.Value = SP_AddUpdPDeficitDetails.BATCHNO;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@BATCHDATE";
                    parameter.Value = SP_AddUpdPDeficitDetails.BATCHDATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ISDELETED";
                    parameter.Value = SP_AddUpdPDeficitDetails.ISDELETED;
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
            }
            return result;
        }
    }
}
