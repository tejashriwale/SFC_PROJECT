using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddUpdSBillDetailsRepository : ISP_AddUpdSBillDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdSBillDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdSBillDetails> sP_AddUpdSBillDetails)
        {
          List<ApiResponseMessage> list = new List<ApiResponseMessage>();
            foreach (var SP_AddUpdSBillDetails in sP_AddUpdSBillDetails)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddUpdSBillDetails]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@SBDAID";
                    parameter.Value = SP_AddUpdSBillDetails.SBDAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SBAID";
                    parameter.Value = SP_AddUpdSBillDetails.SBAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SBPRODUCT";
                    parameter.Value = SP_AddUpdSBillDetails.SBPRODUCT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SBDUOM";
                    parameter.Value = SP_AddUpdSBillDetails.SBDUOM;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SBDQUANTITY";
                    parameter.Value = SP_AddUpdSBillDetails.SBDQUANTITY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SBDRATE";
                    parameter.Value = SP_AddUpdSBillDetails.SBDRATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SBDTAMT";
                    parameter.Value = SP_AddUpdSBillDetails.SBDTAMT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SBDIGST";
                    parameter.Value = SP_AddUpdSBillDetails.SBDIGST;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SBDSGST";
                    parameter.Value = SP_AddUpdSBillDetails.SBDSGST;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SBDCGST";
                    parameter.Value = SP_AddUpdSBillDetails.SBDCGST;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@HSNCODE";
                    parameter.Value = SP_AddUpdSBillDetails.HSNCODE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SBDTOTAL";
                    parameter.Value = SP_AddUpdSBillDetails.SBDTOTAL;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@COMPANYID";
                    parameter.Value = SP_AddUpdSBillDetails.COMPANYID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEPTID";
                    parameter.Value = SP_AddUpdSBillDetails.DEPTID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@BATCHNO";
                    parameter.Value = SP_AddUpdSBillDetails.BATCHNO;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@EXPIRYDATE";
                    parameter.Value = SP_AddUpdSBillDetails.EXPIRYDATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ISDELETED";
                    parameter.Value = SP_AddUpdSBillDetails.ISDELETED;
                    command.Parameters.Add(parameter);




                    dbContext.Database.OpenConnection();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
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
            }
             return list;
        }
    }
}
