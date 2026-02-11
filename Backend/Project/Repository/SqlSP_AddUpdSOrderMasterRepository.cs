using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddUpdSOrderMasterRepository : ISP_AddUpdSOrderMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdSOrderMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(SP_AddUpdSOrderMaster sP_AddUpdSOrderMaster)
        {
           List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdSOrderMaster]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@SOAID";
                parameter.Value = sP_AddUpdSOrderMaster.SOAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SOTRANNO";
                parameter.Value = sP_AddUpdSOrderMaster.SOTRANNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SOCUSTMID";
                parameter.Value = sP_AddUpdSOrderMaster.SOCUSTMID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CAID";
                parameter.Value = sP_AddUpdSOrderMaster.CAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SOPOSUPPLY";
                parameter.Value = sP_AddUpdSOrderMaster.SOPOSUPPLY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SOQUANTITY";
                parameter.Value = sP_AddUpdSOrderMaster.SOQUANTITY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SONARRATION";
                parameter.Value = sP_AddUpdSOrderMaster.SONARRATION;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SOGAMT";
                parameter.Value = sP_AddUpdSOrderMaster.SOGAMT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SOPDUEDAYS";
                parameter.Value = sP_AddUpdSOrderMaster.SOPDUEDAYS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SODDATE";
                parameter.Value = sP_AddUpdSOrderMaster.SODDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SODESCRIPTION";
                parameter.Value = sP_AddUpdSOrderMaster.SODESCRIPTION;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SOPLACE";
                parameter.Value = sP_AddUpdSOrderMaster.SOPLACE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SOPTERMS";
                parameter.Value = sP_AddUpdSOrderMaster.SOPTERMS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SOTERMANDCONDITION";
                parameter.Value = sP_AddUpdSOrderMaster.SOTERMANDCONDITION;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SODATE";
                parameter.Value = sP_AddUpdSOrderMaster.SODATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SNODAYS";
                parameter.Value = sP_AddUpdSOrderMaster.SNODAYS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@QNO";
                parameter.Value = sP_AddUpdSOrderMaster.QNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@QDATE";
                parameter.Value = sP_AddUpdSOrderMaster.QDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SONMT";
                parameter.Value = sP_AddUpdSOrderMaster.SONMT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SQREFF";
                parameter.Value = sP_AddUpdSOrderMaster.SQREFF;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SOTRANSPORTID";
                parameter.Value = sP_AddUpdSOrderMaster.SOTRANSPORTID;
                command.Parameters.Add(parameter);


                parameter = command.CreateParameter();
                parameter.ParameterName = "@COMPANYID";
                parameter.Value = sP_AddUpdSOrderMaster.COMPANYID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DEPTID";
                parameter.Value = sP_AddUpdSOrderMaster.DEPTID;
                command.Parameters.Add(parameter);

                dbContext.Database.OpenConnection();
                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
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
