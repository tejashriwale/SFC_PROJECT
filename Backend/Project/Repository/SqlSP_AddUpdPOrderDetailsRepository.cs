using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddUpdPOrderDetailsRepository : ISP_AddUpdPOrderDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlSP_AddUpdPOrderDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdPOrderDetails> pOrderDetails)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            foreach (var AddUpdPOrderDetails in pOrderDetails)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddUpdPOrderDetails]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@PODAID";
                    parameter.Value = AddUpdPOrderDetails.PODAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PODPRODUCT";
                    parameter.Value = AddUpdPOrderDetails.PODPRODUCT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PODUOM";
                    parameter.Value = AddUpdPOrderDetails.PODUOM;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PODQUANTITY";
                    parameter.Value = AddUpdPOrderDetails.PODQUANTITY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PODRATE";
                    parameter.Value = AddUpdPOrderDetails.PODRATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PODTAMT";
                    parameter.Value = AddUpdPOrderDetails.PODTAMT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PODIGST";
                    parameter.Value = AddUpdPOrderDetails.PODIGST;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PODSGST";
                    parameter.Value = AddUpdPOrderDetails.PODSGST;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PODCGST";
                    parameter.Value = AddUpdPOrderDetails.PODCGST;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PODTOTAL";
                    parameter.Value = AddUpdPOrderDetails.PODTOTAL;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@POAID";
                    parameter.Value = AddUpdPOrderDetails.POAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@HSNCODE";
                    parameter.Value = AddUpdPOrderDetails.HSNCODE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@COMPANYID";
                    parameter.Value = AddUpdPOrderDetails.COMPANYID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEPTID";
                    parameter.Value = AddUpdPOrderDetails.DEPTID;
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
