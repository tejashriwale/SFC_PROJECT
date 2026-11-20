using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_SOrderMasterRepository : IGET_SOrderMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SOrderMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SOrderMaster>> getasync(GET_SOrderMasterDomain input)
        {
            List<GET_SOrderMaster> list = new List<GET_SOrderMaster>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_SOrderMaster] (@p0 ,@p1, @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SOAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await  command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_SOrderMaster
                        {
                            SOAID = reader.GetString("SOAID"),
                            SOTRANNO = reader.GetString("SOTRANNO"),
                            SOCUSTMID = reader.GetString("SOCUSTMID"),
                            CAID = reader.GetString("CAID"),
                            SOPOSUPPLY = reader.GetString("SOPOSUPPLY"),
                            SOQUANTITY = reader.GetString("SOQUANTITY"),
                            SODUEDATE = reader.GetString("SODUEDATE"),
                            SONARRATION = reader.GetString("SONARRATION"),
                            SOGAMT = reader.GetDouble("SOGAMT"),
                            SOPDUEDAYS = reader.GetInt32("SOPDUEDAYS"),
                            SODDATE = reader.GetString("SODDATE"),
                            SODESCRIPTION = reader.GetString("SODESCRIPTION"),
                            SOPLACE = reader.GetString("SOPLACE"),
                            SOPTERMS = reader.GetString("SOPTERMS"),
                            SOTERMANDCONDITION = reader.GetString("SOTERMANDCONDITION"),
                            SODATE = reader.GetString("SODATE"),
                            SNODAYS = reader.GetInt32("SNODAYS"),
                            QNO = reader.GetString("QNO"),
                            QDATE = reader.GetString("QDATE"),
                            SONMT = reader.GetDouble("SONMT"),
                            SQREFF = reader.GetString("SQREFF"),
                            SOTRANSPORTID = reader.GetString("SOTRANSPORTID"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_SOrderMaster>> getkeyword(GET_SOrderMasterDomain input)
        {
            List<GET_SOrderMaster> list = new List<GET_SOrderMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SOrderMaster] (@p0 ,@p1, @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SOAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_SOrderMaster
                        {
                            SOAID = reader.GetString("SOAID"),
                            SOTRANNO = reader.GetString("SOTRANNO"),
                            SOCUSTMID = reader.GetString("SOCUSTMID"),
                            CAID = reader.GetString("CAID"),
                            SOPOSUPPLY = reader.GetString("SOPOSUPPLY"),
                            SOQUANTITY = reader.GetString("SOQUANTITY"),
                            SODUEDATE = reader.GetString("SODUEDATE"),
                            SONARRATION = reader.GetString("SONARRATION"),
                            SOGAMT = reader.GetDouble("SOGAMT"),
                            SOPDUEDAYS = reader.GetInt32("SOPDUEDAYS"),
                            SODDATE = reader.GetString("SODDATE"),
                            SODESCRIPTION = reader.GetString("SODESCRIPTION"),
                            SOPLACE = reader.GetString("SOPLACE"),
                            SOPTERMS = reader.GetString("SOPTERMS"),
                            SOTERMANDCONDITION = reader.GetString("SOTERMANDCONDITION"),
                            SODATE = reader.GetString("SODATE"),
                            SNODAYS = reader.GetInt32("SNODAYS"),
                            QNO = reader.GetString("QNO"),
                            QDATE = reader.GetString("QDATE"),
                            SONMT = reader.GetDouble("SONMT"),
                            SQREFF = reader.GetString("SQREFF"),
                            SOTRANSPORTID = reader.GetString("SOTRANSPORTID"),

                        });
                    }
                }
            }
            return list;
        }
            
    }
}
