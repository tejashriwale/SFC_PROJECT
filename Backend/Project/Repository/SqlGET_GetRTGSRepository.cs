using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_GetRTGSRepository : IGET_GetRTGSRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_GetRTGSRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_GetRTGS>> getasync(GET_GetRTGSDomain input)
        {
            List<GET_GetRTGS> result = new List<GET_GetRTGS>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20  * FROM [GET_GetRTGS](@p0  ,@p1  , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.RTGSAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_GetRTGS
                        {
                            RTGSAID = reader.GetString("RTGSAID"),
                            PYMT_PROD_TYPE_CODE = reader.GetString("PYMT_PROD_TYPE_CODE"),
                            PYMT_MODE = reader.GetString("PYMT_MODE"),
                            DEBIT_ACC_NO = reader.GetString("DEBIT_ACC_NO"),
                            BNF_NAME = reader.GetString("BNF_NAME"),
                            BENE_ACC_NO = reader.GetString("BENE_ACC_NO"),
                            BENE_IFSC  				 = reader.GetString("BENE_IFSC"),
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            DEBIT_NARR = reader.GetString("DEBIT_NARR"),
                            CREDIT_NARR   = reader.GetString("CREDIT_NARR"),
                            MOBILE_NUM = reader.GetString("MOBILE_NUM"),
                            EMAIL_ID = reader.GetString("EMAIL_ID"),
                            REMARK = reader.GetString("REMARK"),
                            PYMT_DATE = reader.GetString("PYMT_DATE"),
                            REF_NO = reader.GetString("REF_NO"),
                            ADDL_INFO1 = reader.GetString("ADDL_INFO1"),
                            ADDL_INFO2  = reader.GetString("ADDL_INFO2"),
                            ADDL_INFO3 = reader.GetString("ADDL_INFO3"),
                            ADDL_INFO4 = reader.GetString("ADDL_INFO4"),
                            ADDL_INFO5 = reader.GetString("ADDL_INFO5"),
                            UAID = reader.GetString("UAID"),
                            
         
                        });
                    }
                }
            }
            return result;
        }

        public async Task<List<GET_GetRTGS>> getkeyword(GET_GetRTGSDomain input)
        {
            List<GET_GetRTGS> result = new List<GET_GetRTGS>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT   * FROM [GET_GetRTGS](@p0  ,@p1  , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.RTGSAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_GetRTGS
                        {
                            RTGSAID = reader.GetString("RTGSAID"),
                            PYMT_PROD_TYPE_CODE = reader.GetString("PYMT_PROD_TYPE_CODE"),
                            PYMT_MODE = reader.GetString("PYMT_MODE"),
                            DEBIT_ACC_NO = reader.GetString("DEBIT_ACC_NO"),
                            BNF_NAME = reader.GetString("BNF_NAME"),
                            BENE_ACC_NO = reader.GetString("BENE_ACC_NO"),
                            BENE_IFSC = reader.GetString("BENE_IFSC"),
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            DEBIT_NARR = reader.GetString("DEBIT_NARR"),
                            CREDIT_NARR = reader.GetString("CREDIT_NARR"),
                            MOBILE_NUM = reader.GetString("MOBILE_NUM"),
                            EMAIL_ID = reader.GetString("EMAIL_ID"),
                            REMARK = reader.GetString("REMARK"),
                            PYMT_DATE = reader.GetString("PYMT_DATE"),
                            REF_NO = reader.GetString("REF_NO"),
                            ADDL_INFO1 = reader.GetString("ADDL_INFO1"),
                            ADDL_INFO2 = reader.GetString("ADDL_INFO2"),
                            ADDL_INFO3 = reader.GetString("ADDL_INFO3"),
                            ADDL_INFO4 = reader.GetString("ADDL_INFO4"),
                            ADDL_INFO5 = reader.GetString("ADDL_INFO5"),
                            UAID = reader.GetString("UAID"),


                        });
                    }
                }
            }
            return result;
        }
    }
}
