using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlAddUpdPReturnDetailsRepository : IAddUpdPReturnDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlAddUpdPReturnDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(List<AddUpdPReturnDetails> pReturnDetails)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            foreach (var AddUpdPReturnDetails in pReturnDetails)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddUpdPReturnDetails]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@PRDAID";
                    parameter.Value = AddUpdPReturnDetails.PRDAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PRAID";
                    parameter.Value = AddUpdPReturnDetails.PRAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PRDPRODUCT";
                    parameter.Value = AddUpdPReturnDetails.PRDPRODUCT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PRDUOM";
                    parameter.Value = AddUpdPReturnDetails.PRDUOM;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PRDQUANTITY";
                    parameter.Value = AddUpdPReturnDetails.PRDQUANTITY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PRTDAMT";
                    parameter.Value = AddUpdPReturnDetails.PRTDAMT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PRDIGST";
                    parameter.Value = AddUpdPReturnDetails.PRDIGST;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PRDSGST";
                    parameter.Value = AddUpdPReturnDetails.PRDSGST;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PRDCGST";
                    parameter.Value = AddUpdPReturnDetails.PRDCGST;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PRDQUANTITYRETURN";
                    parameter.Value = AddUpdPReturnDetails.PRDQUANTITYRETURN;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@HSNCODE";
                    parameter.Value = AddUpdPReturnDetails.HSNCODE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@COMPANYID";
                    parameter.Value = AddUpdPReturnDetails.COMPANYID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEPTID";
                    parameter.Value = AddUpdPReturnDetails.DEPTID;
                    command.Parameters.Add(parameter);


                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@BATCHNO";
                    parameter.Value = AddUpdPReturnDetails.BATCHNO;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@EXPIRYDATE";
                    parameter.Value = AddUpdPReturnDetails.EXPIRYDATE;
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
