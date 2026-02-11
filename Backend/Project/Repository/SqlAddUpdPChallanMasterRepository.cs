using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlAddUpdPChallanMasterRepository : IAddUpdPChallanMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlAddUpdPChallanMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(AddUpdPChallanMaster addUpdPChallanMaster)
        {
           List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdPChallanMaster]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@PCAID";
                parameter.Value = addUpdPChallanMaster.PCAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PCTRNNO";
                parameter.Value = addUpdPChallanMaster.PCTRNNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PCSELLER";
                parameter.Value = addUpdPChallanMaster.PCSELLER;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PCCONSIGNER";
                parameter.Value = addUpdPChallanMaster.PCCONSIGNER;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PCVNO";
                parameter.Value = addUpdPChallanMaster.PCVNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PCPOSUPPLY";
                parameter.Value = addUpdPChallanMaster.PCPOSUPPLY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PCNARRATION";
                parameter.Value = addUpdPChallanMaster.PCNARRATION;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PCDATE";
                parameter.Value = addUpdPChallanMaster.PCDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PCNAMT";
                parameter.Value = addUpdPChallanMaster.PCNAMT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PCGAMT";
                parameter.Value = addUpdPChallanMaster.PCGAMT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@POREFF";
                parameter.Value = addUpdPChallanMaster.POREFF;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@STATUSID";
                parameter.Value = addUpdPChallanMaster.STATUSID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@COMPANYID";
                parameter.Value = addUpdPChallanMaster.COMPANYID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DEPTID";
                parameter.Value = addUpdPChallanMaster.DEPTID;
                command.Parameters.Add(parameter);


                parameter = command.CreateParameter();
                parameter.ParameterName = "@PAYMENTMODE";
                parameter.Value = addUpdPChallanMaster.PAYMENTMODE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PONO";
                parameter.Value = addUpdPChallanMaster.PONO;
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
