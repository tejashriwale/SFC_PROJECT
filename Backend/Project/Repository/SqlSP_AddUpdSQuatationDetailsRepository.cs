using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddUpdSQuatationDetailsRepository : ISP_AddUpdSQuatationDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdSQuatationDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdSQuatationDetails> sP_AddUpdSQuatationDetails)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            foreach (var SP_AddUpdSQuatationDetails in sP_AddUpdSQuatationDetails)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddUpdSQuatationDetails]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@SQAMDAID";
                    parameter.Value = SP_AddUpdSQuatationDetails.SQAMDAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SQAMAID";
                    parameter.Value = SP_AddUpdSQuatationDetails.SQAMAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SQPRODUCT";
                    parameter.Value = SP_AddUpdSQuatationDetails.SQPRODUCT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SQQUANTITY";
                    parameter.Value = SP_AddUpdSQuatationDetails.SQQUANTITY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SQUOM";
                    parameter.Value = SP_AddUpdSQuatationDetails.SQUOM;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SQRATE";
                    parameter.Value = SP_AddUpdSQuatationDetails.SQRATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SQTAMT";
                    parameter.Value = SP_AddUpdSQuatationDetails.SQTAMT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SQIGST";
                    parameter.Value = SP_AddUpdSQuatationDetails.SQIGST;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SQSGST";
                    parameter.Value = SP_AddUpdSQuatationDetails.SQSGST;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SQCGST";
                    parameter.Value = SP_AddUpdSQuatationDetails.SQCGST;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SQCESS";
                    parameter.Value = SP_AddUpdSQuatationDetails.SQCESS;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@HSNCODE";
                    parameter.Value = SP_AddUpdSQuatationDetails.HSNCODE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SQTAXBALEAMOUNT";
                    parameter.Value = SP_AddUpdSQuatationDetails.SQTAXBALEAMOUNT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@IsDeleted";
                    parameter.Value = SP_AddUpdSQuatationDetails.IsDeleted;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@COMPANYID";
                    parameter.Value = SP_AddUpdSQuatationDetails.COMPANYID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEPTID";
                    parameter.Value = SP_AddUpdSQuatationDetails.DEPTID;
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
