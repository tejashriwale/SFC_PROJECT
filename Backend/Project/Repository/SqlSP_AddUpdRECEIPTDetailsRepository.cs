using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddUpdRECEIPTDetailsRepository : ISP_AddUpdRECEIPTDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdRECEIPTDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdRECEIPTDetails> input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            foreach (var SP_AddUpdRECEIPTDetails in input)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddUpdRECEIPTDetails]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@BDAID";
                    parameter.Value = SP_AddUpdRECEIPTDetails.BDAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@BAID";
                    parameter.Value = SP_AddUpdRECEIPTDetails.BAID;
                    command.Parameters.Add(parameter);


                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CROPTYPE";
                    parameter.Value = SP_AddUpdRECEIPTDetails.CROPTYPE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VYAPARINAME";
                    parameter.Value = SP_AddUpdRECEIPTDetails.VYAPARINAME;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@JALI";
                    parameter.Value = SP_AddUpdRECEIPTDetails.JALI;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@WEIGHT";
                    parameter.Value = SP_AddUpdRECEIPTDetails.WEIGHT;
                    command.Parameters.Add(parameter);



                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@WEIGHTMINMAX";
                    parameter.Value = SP_AddUpdRECEIPTDetails.WEIGHTMINMAX;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@RATE";
                    parameter.Value = SP_AddUpdRECEIPTDetails.RATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@AMOUNT";
                    parameter.Value = SP_AddUpdRECEIPTDetails.AMOUNT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@COMPANYID";
                    parameter.Value = SP_AddUpdRECEIPTDetails.COMPANYID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEPTID";
                    parameter.Value = SP_AddUpdRECEIPTDetails.DEPTID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UAID";
                    parameter.Value = SP_AddUpdRECEIPTDetails.UAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ISDELETED";
                    parameter.Value = SP_AddUpdRECEIPTDetails.ISDELETED;
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
