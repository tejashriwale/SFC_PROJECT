using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddUpdHRMEmpolyeeLeavesRepository : ISP_AddUpdHRMEmpolyeeLeavesRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdHRMEmpolyeeLeavesRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(SP_AddUpdHRMEmpolyeeLeaves sP_AddUpdHRMEmpolyeeLeaves)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdHRMEmpolyeeLeaves]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@ELID";
                parameter.Value = sP_AddUpdHRMEmpolyeeLeaves.ELID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ELDAYS";
                parameter.Value = sP_AddUpdHRMEmpolyeeLeaves.ELDAYS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ELLEAVEDATE";
                parameter.Value = sP_AddUpdHRMEmpolyeeLeaves.ELLEAVEDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ELLEAVETYPE";
                parameter.Value = sP_AddUpdHRMEmpolyeeLeaves.ELLEAVETYPE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ELDAYTYPE";
                parameter.Value = sP_AddUpdHRMEmpolyeeLeaves.ELDAYTYPE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ELREASON";
                parameter.Value = sP_AddUpdHRMEmpolyeeLeaves.ELREASON;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ELSDT";
                parameter.Value = sP_AddUpdHRMEmpolyeeLeaves.ELSDT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ELEDT";
                parameter.Value = sP_AddUpdHRMEmpolyeeLeaves.ELEDT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ELTDAYS";
                parameter.Value = sP_AddUpdHRMEmpolyeeLeaves.ELTDAYS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ELMLEAVETYPE";
                parameter.Value = sP_AddUpdHRMEmpolyeeLeaves.ELMLEAVETYPE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ELMREASON";
                parameter.Value = sP_AddUpdHRMEmpolyeeLeaves.ELMREASON;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ELSTATUS";
                parameter.Value = sP_AddUpdHRMEmpolyeeLeaves.ELSTATUS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@COMPANYID";
                parameter.Value = sP_AddUpdHRMEmpolyeeLeaves.COMPANYID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DEPTID";
                parameter.Value = sP_AddUpdHRMEmpolyeeLeaves.DEPTID;
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
