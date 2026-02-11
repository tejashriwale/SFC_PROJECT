using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlSP_AddUpdTransportersAppRepository : ISP_AddUpdTransportersAppRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdTransportersAppRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<SP_AddUpdTransportersApp2>> getasync(SP_AddUpdTransportersApp input)
        {
            List<SP_AddUpdTransportersApp2> result = new List<SP_AddUpdTransportersApp2>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdTransportersApp]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@TAID";
                parameter.Value = input.TAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@TNAME";
                parameter.Value = input.TNAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@TADDRESS";
                parameter.Value = input.TADDRESS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@TCITY";
                parameter.Value = input.TCITY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@TPINCODE";
                parameter.Value = input.TPINCODE;
                command.Parameters.Add(parameter);


                parameter = command.CreateParameter();
                parameter.ParameterName = "@TCANTACTNO";
                parameter.Value = input.TCANTACTNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@TGSTIN";
                parameter.Value = input.TGSTIN;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@TSTATE";
                parameter.Value = input.TSTATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@TPAN";
                parameter.Value = input.TPAN;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@TDATE";
                parameter.Value = input.TDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@TBIRTHDATE";
                parameter.Value = input.TBIRTHDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@VEHICALNO";
                parameter.Value = input.VEHICALNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@VEHICALTYPE";
                parameter.Value = input.VEHICALTYPE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@VEHICALCAPACITY";
                parameter.Value = input.VEHICALCAPACITY;
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
                parameter.ParameterName = "@TUID";
                parameter.Value = input.TUID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UAID";
                parameter.Value = input.UAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@TPROFILE";
                parameter.Value = input.TPROFILE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@FCMTOKEN";
                parameter.Value = input.FCMTOKEN;
                command.Parameters.Add(parameter);

                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new SP_AddUpdTransportersApp2
                        {
                            TAID = reader.GetString("TAID"),
                            TBIRTHDATE = reader.GetString("TBIRTHDATE"),
                            TCANTACTNO = reader.GetString("TCANTACTNO"),
                            TNAME = reader.GetString("TNAME"),
                        });
                    }
                }
            }
            return result;
        }

      
    }
}