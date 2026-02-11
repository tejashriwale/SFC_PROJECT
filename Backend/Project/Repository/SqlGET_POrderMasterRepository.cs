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
                command.CommandText = "SELECT Top 20 * FROM [GET_POrderMaster]('"+ input.POAID+"','"+ input.KEYWORD+"' , '"+input.COMPANYID+"' , '"+input.DEPTID + "')order by PDATE DESC";
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
                command.CommandText = "SELECT * FROM [GET_POrderMaster]('" + input.POAID + "','" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')order by PDATE DESC";
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
