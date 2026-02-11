using System.Data;
using System.Reflection.PortableExecutable;
using Azure.Core.Pipeline;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.Model;

namespace Project.Repository
{
    public class SqlStatecodeRepository : IStatecodeRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlStatecodeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getAsync(Statecode statecode)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();

            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdStateCodeMasters]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@SAID";
                parameter.Value = statecode.SAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SCIRCLENAME";
                parameter.Value = statecode.SCIRCLENAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SREGIONNAME";
                parameter.Value = statecode.SREGIONNAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SDIVISIONNAME";
                parameter.Value = statecode.SDIVISIONNAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SOFFICENAME";
                parameter.Value = statecode.SOFFICENAME;
                command.Parameters.Add(parameter);


                parameter = command.CreateParameter();
                parameter.ParameterName = "@SPINCODE";
                parameter.Value = statecode.SPINCODE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SOFFICETYPE";
                parameter.Value = statecode.SOFFICETYPE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SDELIVERY";
                parameter.Value = statecode.SDELIVERY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SDISTRICT";
                parameter.Value = statecode.SDISTRICT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SSTATENAME";
                parameter.Value = statecode.SSTATENAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SSTATECODE";
                parameter.Value = statecode.SSTATECODE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SLATITUDE";
                parameter.Value = statecode.SLATITUDE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SLONTITUDE";
                parameter.Value = statecode.SLONTITUDE;
                command.Parameters.Add(parameter);


                parameter = command.CreateParameter();
                parameter.ParameterName = "@COMPANYID";
                parameter.Value = statecode.COMPANYID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DEPTID";
                parameter.Value = statecode.DEPTID;
                command.Parameters.Add(parameter);


                command.Parameters.Add(parameter);

                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
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

