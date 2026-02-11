using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddUpdStandingInstrcutionRepository : ISP_AddUpdStandingInstrcutionRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdStandingInstrcutionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdStandingInstrcution> input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            foreach(var SP_AddUpdStandingInstrcution in input)
            { 
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddUpdStandingInstrcution]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@SIID";
                    parameter.Value = SP_AddUpdStandingInstrcution.SIID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SCID";
                    parameter.Value = SP_AddUpdStandingInstrcution.SCID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SHID";
                    parameter.Value = SP_AddUpdStandingInstrcution.SHID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VPAID";
                    parameter.Value = SP_AddUpdStandingInstrcution.VPAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@NAME";
                    parameter.Value = SP_AddUpdStandingInstrcution.NAME;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PHONE";
                    parameter.Value = SP_AddUpdStandingInstrcution.PHONE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ADHAR";
                    parameter.Value = SP_AddUpdStandingInstrcution.ADHAR;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@RATEAPPLICABLE";
                    parameter.Value = SP_AddUpdStandingInstrcution.RATEAPPLICABLE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SERVICETYPE";
                    parameter.Value = SP_AddUpdStandingInstrcution.SERVICETYPE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@RATETYPE";
                    parameter.Value = SP_AddUpdStandingInstrcution.RATETYPE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@RATE";
                    parameter.Value = SP_AddUpdStandingInstrcution.RATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@STATUS";
                    parameter.Value = SP_AddUpdStandingInstrcution.STATUS;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@COMPANYID";
                    parameter.Value = SP_AddUpdStandingInstrcution.COMPANYID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEPTID";
                    parameter.Value = SP_AddUpdStandingInstrcution.DEPTID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UAID";
                    parameter.Value = SP_AddUpdStandingInstrcution.UAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@NEWRATE";
                    parameter.Value = SP_AddUpdStandingInstrcution.NEWRATE;
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
