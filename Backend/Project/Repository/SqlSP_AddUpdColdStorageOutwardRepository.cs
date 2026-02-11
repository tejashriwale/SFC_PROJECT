using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddUpdColdStorageOutwardRepository : ISP_AddUpdColdStorageOutwardRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdColdStorageOutwardRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdColdStorageOutward> input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            foreach (var SP_AddUpdColdStorageOutward in input)
            { 
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddUpdColdStorageOutward]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@CSOWID";
                    parameter.Value = SP_AddUpdColdStorageOutward.CSOWID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CSID";
                    parameter.Value = SP_AddUpdColdStorageOutward.CSID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@STARTDATE";
                    parameter.Value = SP_AddUpdColdStorageOutward.STARTDATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ENDDATE";
                    parameter.Value = SP_AddUpdColdStorageOutward.ENDDATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DAYFROM";
                    parameter.Value = SP_AddUpdColdStorageOutward.DAYFROM;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DAYTO";
                    parameter.Value = SP_AddUpdColdStorageOutward.DAYTO;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@RATE";
                    parameter.Value = SP_AddUpdColdStorageOutward.RATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@WEIGHTINKG";
                    parameter.Value = SP_AddUpdColdStorageOutward.WEIGHTINKG;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DAYINSLAB";
                    parameter.Value = SP_AddUpdColdStorageOutward.DAYINSLAB;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@OUTWARDWEIGHT";
                    parameter.Value = SP_AddUpdColdStorageOutward.OUTWARDWEIGHT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@REMAININGWEIGHT";
                    parameter.Value = SP_AddUpdColdStorageOutward.REMAININGWEIGHT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@AMOUNT";
                    parameter.Value = SP_AddUpdColdStorageOutward.AMOUNT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@TOTALAMOUNT";
                    parameter.Value = SP_AddUpdColdStorageOutward.TOTALAMOUNT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@STATUS";
                    parameter.Value = SP_AddUpdColdStorageOutward.STATUS;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@COMPANYID";
                    parameter.Value = SP_AddUpdColdStorageOutward.COMPANYID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEPTID";
                    parameter.Value = SP_AddUpdColdStorageOutward.DEPTID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UAID";
                    parameter.Value = SP_AddUpdColdStorageOutward.UAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@TRANSACTIONMODE";
                    parameter.Value = SP_AddUpdColdStorageOutward.TRANSACTIONMODE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@NAVEKHATE";
                    parameter.Value = SP_AddUpdColdStorageOutward.NAVEKHATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CHEQUEDATE";
                    parameter.Value = SP_AddUpdColdStorageOutward.CHEQUEDATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CHEQUENO";
                    parameter.Value = SP_AddUpdColdStorageOutward.CHEQUENO;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@NARRATION";
                    parameter.Value = SP_AddUpdColdStorageOutward.NARRATION;
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
