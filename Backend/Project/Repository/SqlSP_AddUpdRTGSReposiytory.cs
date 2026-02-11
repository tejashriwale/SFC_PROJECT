using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;
using System.Data;

namespace Project.Repository
{
    public class SqlSP_AddUpdRTGSReposiytory : ISP_AddUpdRTGSReposiytory
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdRTGSReposiytory(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(SP_AddUpdRTGS input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                List<ApiResponseMessage> list = new List<ApiResponseMessage>();

                using (var Command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddUpdRTGS]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@RTGSAID";
                    parameter.Value = input.RTGSAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PYMT_PROD_TYPE_CODE";
                    parameter.Value = input.PYMT_PROD_TYPE_CODE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PYMT_MODE";
                    parameter.Value = input.PYMT_MODE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEBIT_ACC_NO";
                    parameter.Value = input.DEBIT_ACC_NO;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@BNF_NAME";
                    parameter.Value = input.BNF_NAME;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@BENE_ACC_NO";
                    parameter.Value = input.BENE_ACC_NO;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@BENE_IFSC";
                    parameter.Value = input.BENE_IFSC;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@AMOUNT";
                    parameter.Value = input.AMOUNT;
                    command.Parameters.Add(parameter);


                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEBIT_NARR";
                    parameter.Value = input.DEBIT_NARR;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CREDIT_NARR";
                    parameter.Value = input.CREDIT_NARR;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@MOBILE_NUM";
                    parameter.Value = input.MOBILE_NUM;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@EMAIL_ID";
                    parameter.Value = input.EMAIL_ID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@REMARK";
                    parameter.Value = input.REMARK;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PYMT_DATE";
                    parameter.Value = input.PYMT_DATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@REF_NO";
                    parameter.Value = input.REF_NO;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ADDL_INFO1";
                    parameter.Value = input.ADDL_INFO1;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ADDL_INFO2";
                    parameter.Value = input.ADDL_INFO2;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ADDL_INFO3";
                    parameter.Value = input.ADDL_INFO3;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ADDL_INFO4";
                    parameter.Value = input.ADDL_INFO4;
                    command.Parameters.Add(parameter);


                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ADDL_INFO5";
                    parameter.Value = input.ADDL_INFO5;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UAID";
                    parameter.Value = input.UAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@COMPANYID";
                    parameter.Value = input.COMPANYID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEPTID";
                    parameter.Value = input.DEPTID;
                    command.Parameters.Add(parameter);


                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ISDELETED";
                    parameter.Value = input.ISDELETED;
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
}
