using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_POrderMasterRepository : IGET_POrderMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_POrderMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_POrderMaster>> getasync(GET_POrderMasterDomain input)
        {
           List<GET_POrderMaster> list = new List<GET_POrderMaster>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_POrderMaster](@p0,@p1 , @p2 , @p3)order by PDATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.POAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                await dbContext.Database.OpenConnectionAsync();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_POrderMaster
                        {
                            POAID = reader.GetString("POAID"),
                            POTRANNO = reader.GetString("POTRANNO"),
                            POVENDORID = reader.GetString("POVENDORID"),
                            POCONSIGNER = reader.GetString("POCONSIGNER"),
                            POQUANTITY = reader.GetString("POQUANTITY"),
                            PODUEDATE = reader.GetString("PODUEDATE"),
                            PONARRATION = reader.GetString("PONARRATION"),
                            POGAMT = reader.GetDouble("POGAMT"),
                            POPDUEDAYS = reader.GetInt32("POPDUEDAYS"),
                            PODDATE = reader.GetString("PODDATE"),
                            PODESCRIPTION = reader.GetString("PODESCRIPTION"),
                            POPLACE = reader.GetString("POPLACE"),
                            POPTERMS = reader.GetString("POPTERMS"),
                            POTERMANDCONDITION = reader.GetString("POTERMANDCONDITION"),
                            PODATE = reader.GetString("PODATE"),
                            PNODAYS = reader.GetInt32("PNODAYS"),
                            QNO = reader.GetString("QNO"),
                            PONMT = reader.GetDouble("PONMT"),
                            QDATE = reader.GetString("QDATE"),
                            STATENAME = reader.GetString("STATENAME"),
                            PQREFF = reader.GetString("PQREFF"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            EMAIL = reader.GetString("EMAIL"),
                            VENDORSTATE = reader.GetString("VENDORSTATE"),
                            CONTACTNUMBER = reader.GetString("CONTACTNUMBER"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_POrderMaster>> getkeyword(GET_POrderMasterDomain input)
        {
            List<GET_POrderMaster> list = new List<GET_POrderMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_POrderMaster](@p0,@p1 , @p2 , @p3)order by PDATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.POAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                 dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_POrderMaster
                        {
                            POAID = reader.GetString("POAID"),
                            POTRANNO = reader.GetString("POTRANNO"),
                            POVENDORID = reader.GetString("POVENDORID"),
                            POCONSIGNER = reader.GetString("POCONSIGNER"),
                            POQUANTITY = reader.GetString("POQUANTITY"),
                            PODUEDATE = reader.GetString("PODUEDATE"),
                            PONARRATION = reader.GetString("PONARRATION"),
                            POGAMT = reader.GetDouble("POGAMT"),
                            POPDUEDAYS = reader.GetInt32("POPDUEDAYS"),
                            PODDATE = reader.GetString("PODDATE"),
                            PODESCRIPTION = reader.GetString("PODESCRIPTION"),
                            POPLACE = reader.GetString("POPLACE"),
                            POPTERMS = reader.GetString("POPTERMS"),
                            POTERMANDCONDITION = reader.GetString("POTERMANDCONDITION"),
                            PODATE = reader.GetString("PODATE"),
                            PNODAYS = reader.GetInt32("PNODAYS"),
                            QNO = reader.GetString("QNO"),
                            PONMT = reader.GetDouble("PONMT"),
                            QDATE = reader.GetString("QDATE"),
                            STATENAME = reader.GetString("STATENAME"),
                            PQREFF = reader.GetString("PQREFF"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            EMAIL = reader.GetString("EMAIL"),
                            VENDORSTATE = reader.GetString("VENDORSTATE"),
                            CONTACTNUMBER = reader.GetString("CONTACTNUMBER"),

                        });
                    }
                }
            }
            return list;
        }


            
    }
}
