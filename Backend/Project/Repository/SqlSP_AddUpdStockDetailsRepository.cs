using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlSP_AddUpdStockDetailsRepository : ISP_AddUpdStockDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdStockDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdStockDetails> input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            {
                foreach (var SP_AddUpdStockDetails in input)
                {
                    using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                    {
                        command.CommandText = "[SP_AddUpdStockDetails]";
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        var parameter = command.CreateParameter();
                        parameter.ParameterName = "@STAID";
                        parameter.Value = SP_AddUpdStockDetails.STAID;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@TRNDT";
                        parameter.Value = SP_AddUpdStockDetails.TRNDT;
                        command.Parameters.Add(parameter);


                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@CROP";
                        parameter.Value = SP_AddUpdStockDetails.CROP;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@FAID";
                        parameter.Value = SP_AddUpdStockDetails.FAID;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@CIAID";
                        parameter.Value = SP_AddUpdStockDetails.CIAID;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@TOTCR";
                        parameter.Value = SP_AddUpdStockDetails.TOTCR;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@TOTWT";
                        parameter.Value = SP_AddUpdStockDetails.TOTWT;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@TRNTID";
                        parameter.Value = SP_AddUpdStockDetails.TRNTID;
                        command.Parameters.Add(parameter);


                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@COMPANYID";
                        parameter.Value = SP_AddUpdStockDetails.COMPANYID;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@DEPTID";
                        parameter.Value = SP_AddUpdStockDetails.DEPTID;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@ISDELETED";
                        parameter.Value = SP_AddUpdStockDetails.ISDELETED;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@CGRADE";
                        parameter.Value = SP_AddUpdStockDetails.CGRADE;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@REFF";
                        parameter.Value = SP_AddUpdStockDetails.REFF;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@RTOTCR";
                        parameter.Value = SP_AddUpdStockDetails.RTOTCR;
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
}
