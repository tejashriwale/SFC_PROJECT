using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddUpdSQuatationMasterRepository : ISP_AddUpdSQuatationMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdSQuatationMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(SP_AddUpdSQuatationMaster sP_AddUpdSQuatationMaster)
        {
           List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdSQuatationMaster]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@SQAMAID";
                parameter.Value = sP_AddUpdSQuatationMaster.SQAMAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SQCAID";
                parameter.Value = sP_AddUpdSQuatationMaster.SQCAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CAID";
                parameter.Value = sP_AddUpdSQuatationMaster.CAID;
                command.Parameters.Add(parameter);


                parameter = command.CreateParameter();
                parameter.ParameterName = "@SPRNO";
                parameter.Value = sP_AddUpdSQuatationMaster.SPRNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SQMNAMT";
                parameter.Value = sP_AddUpdSQuatationMaster.SQMNAMT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SQGAMT";
                parameter.Value = sP_AddUpdSQuatationMaster.SQGAMT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SQNO";
                parameter.Value = sP_AddUpdSQuatationMaster.SQNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SQDATE";
                parameter.Value = sP_AddUpdSQuatationMaster.SQDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SQDUEDATE";
                parameter.Value = sP_AddUpdSQuatationMaster.SQDUEDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SQTRANSPORTTERMS";
                parameter.Value = sP_AddUpdSQuatationMaster.SQTRANSPORTTERMS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SQPAYMENTTERMS";
                parameter.Value = sP_AddUpdSQuatationMaster.SQPAYMENTTERMS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ISAPPROVED";
                parameter.Value = sP_AddUpdSQuatationMaster.ISAPPROVED;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SQPDUEDATE";
                parameter.Value = sP_AddUpdSQuatationMaster.SQPDUEDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SQNODAYS";
                parameter.Value = sP_AddUpdSQuatationMaster.SQNODAYS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@COMPANYID";
                parameter.Value = sP_AddUpdSQuatationMaster.COMPANYID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DEPTID";
                parameter.Value = sP_AddUpdSQuatationMaster.DEPTID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SRREFF";
                parameter.Value = sP_AddUpdSQuatationMaster.SRREFF;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SQTRANSPORTID";
                parameter.Value = sP_AddUpdSQuatationMaster.SQTRANSPORTID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SATATUSID";
                parameter.Value = sP_AddUpdSQuatationMaster.SATATUSID;
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
            return result;
        }
    }
}
