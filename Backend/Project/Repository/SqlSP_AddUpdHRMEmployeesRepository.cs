using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddUpdHRMEmployeesRepository : ISP_AddUpdHRMEmployeesRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdHRMEmployeesRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(SP_AddUpdHRMEmployees sP_AddUpdHRMEmployees)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdHRMEmployees]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@EMPID";
                parameter.Value = sP_AddUpdHRMEmployees.EMPID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@EMPNAME";
                parameter.Value = sP_AddUpdHRMEmployees.EMPNAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@EMPEMAIL";
                parameter.Value = sP_AddUpdHRMEmployees.EMPEMAIL;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@EMPCONTACT";
                parameter.Value = sP_AddUpdHRMEmployees.EMPCONTACT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@EMPCODE";
                parameter.Value = sP_AddUpdHRMEmployees.EMPCODE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@EMPDOB";
                parameter.Value = sP_AddUpdHRMEmployees.EMPDOB;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@EMPGENDER";
                parameter.Value = sP_AddUpdHRMEmployees.EMPGENDER;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@EMPNATIONALITY";
                parameter.Value = sP_AddUpdHRMEmployees.EMPNATIONALITY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@EMPJOINDATE";
                parameter.Value = sP_AddUpdHRMEmployees.EMPJOINDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@EMPSHIFT";
                parameter.Value = sP_AddUpdHRMEmployees.EMPSHIFT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@EMPDEPARTMENT";
                parameter.Value = sP_AddUpdHRMEmployees.EMPDEPARTMENT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@EMPDESIGNATION";
                parameter.Value = sP_AddUpdHRMEmployees.EMPDESIGNATION;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@EMPBLOODGROUP";
                parameter.Value = sP_AddUpdHRMEmployees.EMPBLOODGROUP;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@EMPPINCODE";
                parameter.Value = sP_AddUpdHRMEmployees.EMPPINCODE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@EMPLANDMARK";
                parameter.Value = sP_AddUpdHRMEmployees.EMPLANDMARK;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@EMPCITY";
                parameter.Value = sP_AddUpdHRMEmployees.EMPCITY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@EMPSTATE";
                parameter.Value = sP_AddUpdHRMEmployees.EMPSTATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@EMPADDRESS";
                parameter.Value = sP_AddUpdHRMEmployees.EMPADDRESS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@EMPAREA";
                parameter.Value = sP_AddUpdHRMEmployees.EMPAREA;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@EMPPHOTO";
                parameter.Value = sP_AddUpdHRMEmployees.EMPPHOTO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@EMPSTATUS";
                parameter.Value = sP_AddUpdHRMEmployees.EMPSTATUS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@COMPANYID";
                parameter.Value = sP_AddUpdHRMEmployees.COMPANYID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DEPTID";
                parameter.Value = sP_AddUpdHRMEmployees.DEPTID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@AADHAAR";
                parameter.Value = sP_AddUpdHRMEmployees.AADHAAR;
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
