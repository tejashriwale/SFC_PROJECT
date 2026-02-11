using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_TransportersRepository : IGET_TransportersRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_TransportersRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_Transporters>> getasync(GET_TransportersDomain input)
        {
            List<GET_Transporters> list = new List<GET_Transporters>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_Transporters]('" + input.TAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY TDATE DESC";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_Transporters
                        {
                            TAID = reader.GetString("TAID"),
                            TNAME = reader.GetString("TNAME"),
                            VEHICALNO = reader.GetString("VEHICALNO"),
                            TPAN = reader.GetString("TPAN"),
                            VEHICALTYPE = reader.GetString("VEHICALTYPE"),
                            VEHICALCAPACITY = reader.GetString("VEHICALCAPACITY"),
                            TBIRTHDATE = reader.GetString("TBIRTHDATE"),
                            TGSTIN	 = reader.GetString("TGSTIN"),
                            TTSTATE = reader.GetString("TTSTATE"),
                            TADDRESS = reader.GetString("TADDRESS"),
                            TCITY = reader.GetString("TCITY"),
                            TPINCODE = reader.GetString("TPINCODE"),
                            TCANTACTNO = reader.GetString("TCANTACTNO"),
                            TDATE = reader.GetString("TDATE"),
                            TPROFILE = reader.GetString("TPROFILE"),
                            TUID = reader.GetString("TUID"),
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_Transporters>> getkeyword(GET_TransportersDomain input)
        {
            List<GET_Transporters> list = new List<GET_Transporters>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_Transporters]('" + input.TAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY TDATE DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_Transporters
                        {
                            TAID = reader.GetString("TAID"),
                            TNAME = reader.GetString("TNAME"),
                            VEHICALNO = reader.GetString("VEHICALNO"),
                            TPAN = reader.GetString("TPAN"),
                            VEHICALTYPE = reader.GetString("VEHICALTYPE"),
                            VEHICALCAPACITY = reader.GetString("VEHICALCAPACITY"),
                            TBIRTHDATE = reader.GetString("TBIRTHDATE"),
                            TGSTIN = reader.GetString("TGSTIN"),
                            TTSTATE = reader.GetString("TTSTATE"),
                            TADDRESS = reader.GetString("TADDRESS"),
                            TCITY = reader.GetString("TCITY"),
                            TPINCODE = reader.GetString("TPINCODE"),
                            TCANTACTNO = reader.GetString("TCANTACTNO"),
                            TDATE = reader.GetString("TDATE"),
                            TPROFILE = reader.GetString("TPROFILE"),
                            TUID = reader.GetString("TUID"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
