using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlAddGoodReceiptNoteAssuranceRepository : IAddGoodReceiptNoteAssuranceRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlAddGoodReceiptNoteAssuranceRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(AddGoodReceiptNoteAssurance addGoodReceipt)
        {
           List<ApiResponseMessage> result = new List<ApiResponseMessage>();
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddUpdGoodReceiptNoteAssurance]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@GAAID";
                    parameter.Value = addGoodReceipt.GAAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@QCID";
                    parameter.Value = addGoodReceipt.QCID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@GRNAID";
                    parameter.Value = addGoodReceipt.GRNAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@GACONDITION";
                    parameter.Value = addGoodReceipt.GACONDITION;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@GAAMT";
                    parameter.Value = addGoodReceipt.GAAMT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@GAEXPECTED";
                    parameter.Value = addGoodReceipt.GAEXPECTED;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@GAPACKAGE";
                    parameter.Value = addGoodReceipt.GAPACKAGE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@GACONDITIONDESCRIPTION";
                    parameter.Value = addGoodReceipt.GACONDITIONDESCRIPTION;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@GAAMTDESCRIPTION";
                    parameter.Value = addGoodReceipt.GAAMTDESCRIPTION;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@GAEXPECTEDDESCRIPTION";
                    parameter.Value = addGoodReceipt.GAEXPECTEDDESCRIPTION;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@GAPACKAGEDESCRIPTION";
                    parameter.Value = addGoodReceipt.GAPACKAGEDESCRIPTION;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@COMPANYID";
                    parameter.Value = addGoodReceipt.COMPANYID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEPTID";
                    parameter.Value = addGoodReceipt.DEPTID;
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
            
            return result;
        }
    }
}
