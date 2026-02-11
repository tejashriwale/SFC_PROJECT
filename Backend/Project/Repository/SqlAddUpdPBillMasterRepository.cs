using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlAddUpdPBillMasterRepository : IAddUpdPBillMasterRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlAddUpdPBillMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(AddUpdPBillMaster pBillMaster)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddPBillMaster]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@PBAID";
                parameter.Value = pBillMaster.PBAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PBVENDORID";
                parameter.Value = pBillMaster.PBVENDORID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PBCONSIGNER";
                parameter.Value = pBillMaster.PBCONSIGNER;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PBVEHICALNO";
                parameter.Value = pBillMaster.PBVEHICALNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PBPOSUPPLY";
                parameter.Value = pBillMaster.PBPOSUPPLY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PBBILLNO";
                parameter.Value = pBillMaster.PBBILLNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PBDATE";
                parameter.Value = pBillMaster.PBDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PBDUEDATE";
                parameter.Value = pBillMaster.PBDUEDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PBBILLDATE";
                parameter.Value = pBillMaster.PBBILLDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PBNARRATION";
                parameter.Value = pBillMaster.PBNARRATION;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PBTRANSPORT";
                parameter.Value = pBillMaster.PBTRANSPORT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PBTERMANDCONDITION";
                parameter.Value = pBillMaster.PBTERMANDCONDITION;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@COMPANYID";
                parameter.Value = pBillMaster.COMPANYID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DEPTID";
                parameter.Value = pBillMaster.DEPTID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PBCHALLANNO";
                parameter.Value = pBillMaster.PBCHALLANNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PBDISCOUNT";
                parameter.Value = pBillMaster.PBDISCOUNT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PBNETAMOUNT";
                parameter.Value = pBillMaster.PBNETAMOUNT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@VBILLNO";
                parameter.Value = pBillMaster.VBILLNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@QNODAYS";
                parameter.Value = pBillMaster.QNODAYS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PCREFF";
                parameter.Value = pBillMaster.PCREFF;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@QPAYMENTTERMS";
                parameter.Value = pBillMaster.QPAYMENTTERMS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@QPDUEDATE";
                parameter.Value = pBillMaster.QPDUEDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PAYMENTMODE";
                parameter.Value = pBillMaster.PAYMENTMODE;
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
