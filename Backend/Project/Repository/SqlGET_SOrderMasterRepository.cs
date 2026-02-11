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
                command.CommandText = "SELECT Top 20 * FROM [GET_SOrderMaster] ('" + input.SOAID + "' ,'"+ input.KEYWORD +"', '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
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
                command.CommandText = "SELECT * FROM [GET_SOrderMaster] ('" + input.SOAID + "' ,'" + input.KEYWORD + "', '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
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
