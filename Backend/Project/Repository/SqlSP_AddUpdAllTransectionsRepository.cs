using Azure.Core.Pipeline;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlSP_AddUpdAllTransectionsRepository : ISP_AddUpdAllTransectionsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdAllTransectionsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdAllTransections> input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            foreach (var SP_AddUpdAllTransections in input)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddUpdAllTransections]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@TRAID";
                    parameter.Value = SP_AddUpdAllTransections.TRAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@TRNSR";
                    parameter.Value = SP_AddUpdAllTransections.TRNSR;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@TRNSID";
                    parameter.Value = SP_AddUpdAllTransections.TRNSID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@TRNTID";
                    parameter.Value = SP_AddUpdAllTransections.TRNTID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@TRNDT";
                    parameter.Value = SP_AddUpdAllTransections.TRNDT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@REFAID";
                    parameter.Value = SP_AddUpdAllTransections.REFAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@MAID";
                    parameter.Value = SP_AddUpdAllTransections.MAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PAMT";
                    parameter.Value = SP_AddUpdAllTransections.PAMT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CBAL";
                    parameter.Value = SP_AddUpdAllTransections.CBAL;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ADVAMT";
                    parameter.Value = SP_AddUpdAllTransections.ADVAMT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ASID";
                    parameter.Value = SP_AddUpdAllTransections.ASID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ISDELETED";
                    parameter.Value = SP_AddUpdAllTransections.ISDELETED;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@COMPANYID";
                    parameter.Value = SP_AddUpdAllTransections.COMPANYID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEPTID";
                    parameter.Value = SP_AddUpdAllTransections.DEPTID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CROPID";
                    parameter.Value = SP_AddUpdAllTransections.CROPID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PAYMENTDT";
                    parameter.Value = SP_AddUpdAllTransections.PAYMENTDT ?? (object)DBNull.Value;
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
