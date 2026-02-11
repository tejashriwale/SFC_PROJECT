using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlAddAnalysisRepository : IAddAnalysisRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlAddAnalysisRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(List<AddAnalysis> addAnalysis)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            foreach (var AddAnalysis in addAnalysis)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddUpdAnalysis]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@AAID";
                    parameter.Value = AddAnalysis.AAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@QAMAID";
                    parameter.Value = AddAnalysis.QAMAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VAID";
                    parameter.Value = AddAnalysis.VAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PRAID";
                    parameter.Value = AddAnalysis.PRAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@NETAMT";
                    parameter.Value = AddAnalysis.NETAMT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ISAPPROVED";
                    parameter.Value = AddAnalysis.ISAPPROVED;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@COMPANYID";
                    parameter.Value = AddAnalysis.COMPANYID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEPTID";
                    parameter.Value = AddAnalysis.DEPTID;
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
