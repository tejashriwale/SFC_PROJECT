using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_RECEIPTMasterRepository : IGET_RECEIPTMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_RECEIPTMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_RECEIPTMaster>> getasync(GET_RECEIPTMasterDomain input)
        {
            List<GET_RECEIPTMaster> list = new List<GET_RECEIPTMaster>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_RECEIPTMaster]('" + input.BAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' , '" + input.DATE + "' )order by STATUS,DATEO desc";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_RECEIPTMaster
                        {
                            BAID = reader.GetString("BAID"),
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            DATE = reader.GetString("DATE"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            FADDHARNO = reader.GetString("FADDHARNO"),
                            STATUS = reader.GetString("STATUS"),
                            ISVYAPARIVERIFIED = reader.GetBoolean("ISVYAPARIVERIFIED"),
                            DATEO = reader.GetDateTime("DATEO"),
                            FCONTACTNO = reader.GetString("FCONTACTNO"),
                            FNAME = reader.GetString("FNAME"),
                            MAID = reader.GetString("MAID"),
                            TOKNNO = reader.GetString("TOKNNO"),
                            VEHNO = reader.GetString("VEHNO"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_RECEIPTMaster>> getkeyword(GET_RECEIPTMasterDomain input)
        {
            List<GET_RECEIPTMaster> list = new List<GET_RECEIPTMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_RECEIPTMaster]('" + input.BAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' ,  '" + input.DATE + "' )order by STATUS,DATEO desc";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_RECEIPTMaster
                        {

                            BAID = reader.GetString("BAID"),
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            DATE = reader.GetString("DATE"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            FADDHARNO = reader.GetString("FADDHARNO"),
                            STATUS = reader.GetString("STATUS"),
                            ISVYAPARIVERIFIED = reader.GetBoolean("ISVYAPARIVERIFIED"),
                            FCONTACTNO = reader.GetString("FCONTACTNO"),
                            FNAME = reader.GetString("FNAME"),
                            MAID = reader.GetString("MAID"),
                            TOKNNO = reader.GetString("TOKNNO"),
                            VEHNO = reader.GetString("VEHNO"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
