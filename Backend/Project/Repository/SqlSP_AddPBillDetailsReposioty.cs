using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddPBillDetailsReposioty : ISP_AddPBillDetailsReposioty
    {
        private readonly ApplicationDbContext dbContext;

        public SqlSP_AddPBillDetailsReposioty(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(List<SP_AddPBillDetails> input)
        {
           List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            foreach (var SP_AddPBillDetails in input)
            { 
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddPBillDetails]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@PBDAID";
                    parameter.Value = SP_AddPBillDetails.PBDAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PBAID";
                    parameter.Value = SP_AddPBillDetails.PBAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PBPRODUCT";
                    parameter.Value = SP_AddPBillDetails.PBPRODUCT;
                    command.Parameters.Add(parameter);


                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PBDUOM";
                    parameter.Value = SP_AddPBillDetails.PBDUOM;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PBDQUANTITY";
                    parameter.Value = SP_AddPBillDetails.PBDQUANTITY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PBDRATE";
                    parameter.Value = SP_AddPBillDetails.PBDRATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PBDTAMT";
                    parameter.Value = SP_AddPBillDetails.PBDTAMT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PBDIGST";
                    parameter.Value = SP_AddPBillDetails.PBDIGST;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PBDSGST";
                    parameter.Value = SP_AddPBillDetails.PBDSGST;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PBDCGST";
                    parameter.Value = SP_AddPBillDetails.PBDCGST;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@HSNCODE";
                    parameter.Value = SP_AddPBillDetails.HSNCODE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@COMPANYID";
                    parameter.Value = SP_AddPBillDetails.COMPANYID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEPTID";
                    parameter.Value = SP_AddPBillDetails.DEPTID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@BATCHNO";
                    parameter.Value = SP_AddPBillDetails.BATCHNO;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@EXPIRYDATE";
                    parameter.Value = SP_AddPBillDetails.EXPIRYDATE;
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
               
            }
            return result;
        }
    }
}
