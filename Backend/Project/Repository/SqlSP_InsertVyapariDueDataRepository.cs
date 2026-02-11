using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_InsertVyapariDueDataRepository : ISP_InsertVyapariDueDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_InsertVyapariDueDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(List<SP_InsertVyapariDueData> input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
             foreach(var SP_InsertVyapariDueData in input)
             { 
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_InsertVyapariDueData]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@VDAID";
                    parameter.Value = SP_InsertVyapariDueData.VDAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@TRNDT";
                    parameter.Value = SP_InsertVyapariDueData.TRNDT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UAID";
                    parameter.Value = SP_InsertVyapariDueData.UAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@TAMT";
                    parameter.Value = SP_InsertVyapariDueData.TAMT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VPAID";
                    parameter.Value = SP_InsertVyapariDueData.VPAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@TRNSR";
                    parameter.Value = SP_InsertVyapariDueData.@TRNSR;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@TRNTID";
                    parameter.Value = SP_InsertVyapariDueData.TRNTID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@REFKEY";
                    parameter.Value = SP_InsertVyapariDueData.REFKEY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VREFF";
                    parameter.Value = SP_InsertVyapariDueData.VREFF;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SEVA";
                    parameter.Value = SP_InsertVyapariDueData.SEVA;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ISDELETED";
                    parameter.Value = SP_InsertVyapariDueData.ISDELETED;
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
