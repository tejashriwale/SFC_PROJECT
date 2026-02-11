using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlAddQuatationMasterRepository : IAddQuatationMasterRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlAddQuatationMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(AddQuatationMaster quatationMaster)
        {
           List<ApiResponseMessage> result = new List<ApiResponseMessage>();

            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdQuatationMaster]";
                command.CommandType = System.Data.CommandType.StoredProcedure;


                var parameter = command.CreateParameter();
                parameter.ParameterName = "@QAMAID";
                parameter.Value = quatationMaster.QAMAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@QVAID";
                parameter.Value = quatationMaster.QVAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@QVCONTACT";
                parameter.Value = quatationMaster.QVCONTACT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@QVEMAIL";
                parameter.Value = quatationMaster.QVEMAIL;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@QVADDRESS";
                parameter.Value = quatationMaster.QVADDRESS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PRNO";
                parameter.Value = quatationMaster.PRNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@QMNAMT";
                parameter.Value = quatationMaster.QMNAMT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@QGAMT";
                parameter.Value = quatationMaster.QGAMT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@QNO";
                parameter.Value = quatationMaster.QNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@QDATE";
                parameter.Value = quatationMaster.QDATE;
                command.Parameters.Add(parameter);


                parameter = command.CreateParameter();
                parameter.ParameterName = "@QDUEDATE";
                parameter.Value = quatationMaster.QDUEDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@COMPANYID";
                parameter.Value = quatationMaster.COMPANYID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DEPTID";
                parameter.Value = quatationMaster.DEPTID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@QTRANSPORTTERMS";
                parameter.Value = quatationMaster.QTRANSPORTTERMS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@QPAYMENTTERMS";
                parameter.Value = quatationMaster.QPAYMENTTERMS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@QPDUEDATE";
                parameter.Value = quatationMaster.QPDUEDATE;
                command.Parameters.Add(parameter);
                
                parameter = command.CreateParameter();
                parameter.ParameterName = "@QNODAYS";
                parameter.Value = quatationMaster.QNODAYS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ISAPPROVED";
                parameter.Value = quatationMaster.ISAPPROVED;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PRREFF";
                parameter.Value = quatationMaster.PRREFF;
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
