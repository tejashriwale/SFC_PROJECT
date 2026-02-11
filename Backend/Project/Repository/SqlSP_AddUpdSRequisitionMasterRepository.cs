using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddUpdSRequisitionMasterRepository : ISP_AddUpdSRequisitionMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdSRequisitionMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(SP_AddUpdSRequisitionMaster sP_AddUpdSRequisitionMaster)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdSRequisitionMaster]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@SRAID";
                parameter.Value = sP_AddUpdSRequisitionMaster.SRAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SRQDATE";
                parameter.Value = sP_AddUpdSRequisitionMaster.SRQDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SREQNO";
                parameter.Value = sP_AddUpdSRequisitionMaster.SREQNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SREXPCDATE";
                parameter.Value = sP_AddUpdSRequisitionMaster.SREXPCDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SRCATEGORY";
                parameter.Value = sP_AddUpdSRequisitionMaster.SRCATEGORY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SRVANDORID";
                parameter.Value = sP_AddUpdSRequisitionMaster.SRVANDORID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SRNARRATION";
                parameter.Value = sP_AddUpdSRequisitionMaster.SRNARRATION;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SRTERMANDCONDITION";
                parameter.Value = sP_AddUpdSRequisitionMaster.SRTERMANDCONDITION;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SRMDATE";
                parameter.Value = sP_AddUpdSRequisitionMaster.SRMDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SRNO";
                parameter.Value = sP_AddUpdSRequisitionMaster.SRNO;
                command.Parameters.Add(parameter);


                parameter = command.CreateParameter();
                parameter.ParameterName = "@COMPANYID";
                parameter.Value = sP_AddUpdSRequisitionMaster.COMPANYID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DEPTID";
                parameter.Value = sP_AddUpdSRequisitionMaster.DEPTID;
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
