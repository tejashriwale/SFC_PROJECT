using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddUpdSBillMasterRepository : ISP_AddUpdSBillMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdSBillMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(SP_AddUpdSBillMaster sP_AddUpdSBillMaster)
        {
           List<ApiResponseMessage> list = new List<ApiResponseMessage>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdSBillMaster]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@SBAID";
                parameter.Value = sP_AddUpdSBillMaster.SBAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SBVENDORID";
                parameter.Value = sP_AddUpdSBillMaster.SBVENDORID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SBCONSIGNER";
                parameter.Value = sP_AddUpdSBillMaster.SBCONSIGNER;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SBVEHICALNO";
                parameter.Value = sP_AddUpdSBillMaster.SBVEHICALNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SBPOSUPPLY";
                parameter.Value = sP_AddUpdSBillMaster.SBPOSUPPLY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SBBILLNO";
                parameter.Value = sP_AddUpdSBillMaster.SBBILLNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SBDATE";
                parameter.Value = sP_AddUpdSBillMaster.SBDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SBBILLDATE";
                parameter.Value = sP_AddUpdSBillMaster.SBBILLDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SBDUEDATE";
                parameter.Value = sP_AddUpdSBillMaster.SBDUEDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SBNARRATION";
                parameter.Value = sP_AddUpdSBillMaster.SBNARRATION;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SBTRANSPORT";
                parameter.Value = sP_AddUpdSBillMaster.SBTRANSPORT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SBTERMANDCONDITION";
                parameter.Value = sP_AddUpdSBillMaster.SBTERMANDCONDITION;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SBCHALLANNO";
                parameter.Value = sP_AddUpdSBillMaster.SBCHALLANNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SBDISCOUNT";
                parameter.Value = sP_AddUpdSBillMaster.SBDISCOUNT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SBNETAMOUNT";
                parameter.Value = sP_AddUpdSBillMaster.SBNETAMOUNT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@VBILLNO";
                parameter.Value = sP_AddUpdSBillMaster.VBILLNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@QNODAYS";
                parameter.Value = sP_AddUpdSBillMaster.QNODAYS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@QPAYMENTTERMS";
                parameter.Value = sP_AddUpdSBillMaster.QPAYMENTTERMS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@QPDUEDATE";
                parameter.Value = sP_AddUpdSBillMaster.QPDUEDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@COMPANYID";
                parameter.Value = sP_AddUpdSBillMaster.COMPANYID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DEPTID";
                parameter.Value = sP_AddUpdSBillMaster.DEPTID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SCREFF";
                parameter.Value = sP_AddUpdSBillMaster.SCREFF;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PAYMENTMODE";
                parameter.Value = sP_AddUpdSBillMaster.PAYMENTMODE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SBILLTOTAL";
                parameter.Value = sP_AddUpdSBillMaster.SBILLTOTAL;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SBGROSSAMT";
                parameter.Value = sP_AddUpdSBillMaster.SBGROSSAMT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@STATUSID";
                parameter.Value = sP_AddUpdSBillMaster.STATUSID;
                command.Parameters.Add(parameter);

                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new ApiResponseMessage
                        {
                            IsSuccessful = reader.GetInt32("IsSuccessful"),
                            ResponseCode = reader.GetString("ResponseCode"),
                            ResponseMessage = reader.GetString("ResponseMessage"),
                            ResponseValues = reader.GetString("ResponseValues")
                        });
                    }
                }
            }
            return list;
        }
    }
}
