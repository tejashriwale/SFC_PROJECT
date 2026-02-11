using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddUpdSChallanMasterRepository : ISP_AddUpdSChallanMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdSChallanMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(SP_AddUpdSChallanMaster sP_AddUpdSChallanMaster)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdSChallanMaster]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@SCAID";
                parameter.Value = sP_AddUpdSChallanMaster.SCAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SCSELLER";
                parameter.Value = sP_AddUpdSChallanMaster.SCSELLER;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SCTRNNO";
                parameter.Value = sP_AddUpdSChallanMaster.SCTRNNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SCCONSIGNER";
                parameter.Value = sP_AddUpdSChallanMaster.SCCONSIGNER;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SCVNO";
                parameter.Value = sP_AddUpdSChallanMaster.SCVNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SCPOSUPPLY";
                parameter.Value = sP_AddUpdSChallanMaster.SCPOSUPPLY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SCNARRATION";
                parameter.Value = sP_AddUpdSChallanMaster.SCNARRATION;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SCDATE";
                parameter.Value = sP_AddUpdSChallanMaster.SCDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SCNAMT";
                parameter.Value = sP_AddUpdSChallanMaster.SCNAMT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SCGAMT";
                parameter.Value = sP_AddUpdSChallanMaster.SCGAMT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SOREFF";
                parameter.Value = sP_AddUpdSChallanMaster.SOREFF;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@COMPANYID";
                parameter.Value = sP_AddUpdSChallanMaster.COMPANYID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DEPTID";
                parameter.Value = sP_AddUpdSChallanMaster.DEPTID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PAYMENTMODE";
                parameter.Value = sP_AddUpdSChallanMaster.PAYMENTMODE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@STATUSID";
                parameter.Value = sP_AddUpdSChallanMaster.STATUSID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SQNO";
                parameter.Value = sP_AddUpdSChallanMaster.SQNO;
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
