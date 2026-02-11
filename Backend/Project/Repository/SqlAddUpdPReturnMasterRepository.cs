using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlAddUpdPReturnMasterRepository : IAddUpdPReturnMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlAddUpdPReturnMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(AddUpdPReturnMaster pReturnMaster)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdPReturnMaster]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@PRAID";
                parameter.Value = pReturnMaster.PRAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PBNO";
                parameter.Value = pReturnMaster.PBNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PBDATE";
                parameter.Value = pReturnMaster.PBDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@POTRNNO";
                parameter.Value = pReturnMaster.POTRNNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PSELLER";
                parameter.Value = pReturnMaster.PSELLER;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PRCONSIGNER";
                parameter.Value = pReturnMaster.PRCONSIGNER;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PBREFEWENCE";
                parameter.Value = pReturnMaster.PBREFEWENCE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PRNARRATION";
                parameter.Value = pReturnMaster.PRNARRATION;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PRDATE";
                parameter.Value = pReturnMaster.PRDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PRNAMT";
                parameter.Value = pReturnMaster.PRNAMT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PRGAMT";
                parameter.Value = pReturnMaster.PRGAMT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@RTYPE";
                parameter.Value = pReturnMaster.RTYPE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PAYMENTMODE";
                parameter.Value = pReturnMaster.PAYMENTMODE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@COMPANYID";
                parameter.Value = pReturnMaster.COMPANYID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DEPTID";
                parameter.Value = pReturnMaster.DEPTID;
                command.Parameters.Add(parameter);

                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await  reader.ReadAsync())
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
