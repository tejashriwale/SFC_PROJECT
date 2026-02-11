using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlAddGoodReceiptNoteMasterRepository : IAddGoodReceiptNoteMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
       public SqlAddGoodReceiptNoteMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(AddGoodReceiptNoteMaster addGoodReceipt)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdGoodReceiptNoteMaster]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@GRNAID";
                parameter.Value = addGoodReceipt.GRNAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@GRNTRANSACTIONID";
                parameter.Value = addGoodReceipt.GRNTRANSACTIONID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PCAID";
                parameter.Value = addGoodReceipt.PCAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@GRNDATE";
                parameter.Value = addGoodReceipt.GRNDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@GRNNUMBER";
                parameter.Value = addGoodReceipt.GRNNUMBER;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@GRNCHALLANNO";
                parameter.Value = addGoodReceipt.GRNCHALLANNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@GRNCHALLANDATE";
                parameter.Value = addGoodReceipt.GRNCHALLANDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@GRNVECHILENO";
                parameter.Value = addGoodReceipt.GRNVECHILENO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@GRNVENDOR";
                parameter.Value = addGoodReceipt.GRNVENDOR;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@GRNCONSIGNER";
                parameter.Value = addGoodReceipt.GRNCONSIGNER;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@GRNINSPECTEDBY";
                parameter.Value = addGoodReceipt.GRNINSPECTEDBY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@GRNNARRATION";
                parameter.Value = addGoodReceipt.GRNNARRATION;
                command.Parameters.Add(parameter);


                parameter = command.CreateParameter();
                parameter.ParameterName = "@COMPANYID";
                parameter.Value = addGoodReceipt.COMPANYID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DEPTID";
                parameter.Value = addGoodReceipt.DEPTID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PCREFF";
                parameter.Value = addGoodReceipt.PCREFF;
                command.Parameters.Add(parameter);

                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
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
            return result;
        }
    }
}
