using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.Model;

namespace Project.Repository
{
    public class SqlIServiceMasterRepository : IServiceMasterRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlIServiceMasterRepository(ApplicationDbContext dbContext)

        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(ServiceMaster serviceMaster)
        {
           List<ApiResponseMessage> list = new List<ApiResponseMessage>();

            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdServicesMaster]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@SAID";
                parameter.Value = serviceMaster.SAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SCODE";
                parameter.Value = serviceMaster.SCODE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SNAME";
                parameter.Value = serviceMaster.SNAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@STYPE";
                parameter.Value = serviceMaster.STYPE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SDESCRIPTION";
                parameter.Value = serviceMaster.SDESCRIPTION;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SCATEGORY";
                parameter.Value = serviceMaster.SCATEGORY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SSUBCATEGORY";
                parameter.Value = serviceMaster.SSUBCATEGORY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SHSN";
                parameter.Value = serviceMaster.SHSN;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@COMPANYID";
                parameter.Value = serviceMaster.COMPANYID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DEPTID";
                parameter.Value = serviceMaster.DEPTID;
                command.Parameters.Add(parameter);


                dbContext.Database.OpenConnection();

                using(var reader = await  command.ExecuteReaderAsync())
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
            return list;
        }
    }
}
