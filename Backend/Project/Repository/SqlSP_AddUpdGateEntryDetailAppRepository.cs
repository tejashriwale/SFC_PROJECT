using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddUpdGateEntryDetailAppRepository : ISP_AddUpdGateEntryDetailAppRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdGateEntryDetailAppRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdGateEntryDetailApp> input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            foreach (var SP_AddUpdGateEntryDetailApp in input)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddUpdGateEntryDetail]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@DPKID";
                    parameter.Value = SP_AddUpdGateEntryDetailApp.DPKID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@MAID";
                    parameter.Value = SP_AddUpdGateEntryDetailApp.MAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@TOKNNO";
                    parameter.Value = SP_AddUpdGateEntryDetailApp.TOKNNO;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VILLAGE";
                    parameter.Value = SP_AddUpdGateEntryDetailApp.VILLAGE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@FULLNAME";
                    parameter.Value = SP_AddUpdGateEntryDetailApp.FULLNAME;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CARETS_COUNT";
                    parameter.Value = SP_AddUpdGateEntryDetailApp.CARETS_COUNT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@COMPANYID";
                    parameter.Value = SP_AddUpdGateEntryDetailApp.COMPANYID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEPTID";
                    parameter.Value = SP_AddUpdGateEntryDetailApp.DEPTID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ISDELETED";
                    parameter.Value = SP_AddUpdGateEntryDetailApp.ISDELETED;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VEHNO";
                    parameter.Value = SP_AddUpdGateEntryDetailApp.VEHNO;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@AADHARNO";
                    parameter.Value = SP_AddUpdGateEntryDetailApp.AADHARNO;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@MOBILENO";
                    parameter.Value = SP_AddUpdGateEntryDetailApp.MOBILENO;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CROP_TYPE";
                    parameter.Value = SP_AddUpdGateEntryDetailApp.CROP_TYPE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DATE";
                    parameter.Value = SP_AddUpdGateEntryDetailApp.DATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UID";
                    parameter.Value = SP_AddUpdGateEntryDetailApp.UID;
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
