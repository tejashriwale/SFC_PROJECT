using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlAddUpdPChallanDetailsRepository : IAddUpdPChallanDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlAddUpdPChallanDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(List<AddUpdPChallanDetails> pChallanDetails)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            foreach (var AddUpdPChallanDetails in pChallanDetails)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddPChallanDetails]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@PCDAID";
                    parameter.Value = AddUpdPChallanDetails.PCDAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PCAID";
                    parameter.Value = AddUpdPChallanDetails.PCAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PCDPRODUCT";
                    parameter.Value = AddUpdPChallanDetails.PCDPRODUCT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PCDUOM";
                    parameter.Value = AddUpdPChallanDetails.PCDUOM;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PCDQUANTITY";
                    parameter.Value = AddUpdPChallanDetails.PCDQUANTITY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PCDRATE";
                    parameter.Value = AddUpdPChallanDetails.PCDRATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PCDTAMT";
                    parameter.Value = AddUpdPChallanDetails.PCDTAMT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PCDTAXABLE";
                    parameter.Value = AddUpdPChallanDetails.PCDTAXABLE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PCDIGST";
                    parameter.Value = AddUpdPChallanDetails.PCDIGST;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PCDSGST";
                    parameter.Value = AddUpdPChallanDetails.PCDSGST;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PCDCGST";
                    parameter.Value = AddUpdPChallanDetails.PCDCGST;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@HSNCODE";
                    parameter.Value = AddUpdPChallanDetails.HSNCODE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEPTID";
                    parameter.Value = AddUpdPChallanDetails.DEPTID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@COMPANYID";
                    parameter.Value = AddUpdPChallanDetails.COMPANYID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@BATCHNO";
                    parameter.Value = AddUpdPChallanDetails.BATCHNO;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@EXPIRYDATE";
                    parameter.Value = AddUpdPChallanDetails.EXPIRYDATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ISDELETED";
                    parameter.Value = AddUpdPChallanDetails.ISDELETED;
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
