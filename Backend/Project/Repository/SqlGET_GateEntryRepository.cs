using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_GateEntryRepository : IGET_GateEntryRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_GateEntryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_GateEntry>> getasync(GET_GateEntryDomain input)
        {
            List<GET_GateEntry> list = new List<GET_GateEntry>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_GateEntry]('" + input.PKID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_GateEntry
                        {
                            PKID = reader.GetString("PKID"),
                            
                            DATE = reader.GetString("DATE"),
                            VEHNO = reader.GetString("VEHNO"),
                           
                            MOBILENO = reader.GetString("MOBILENO"),
                            AADHARNO = reader.GetString("AADHARNO"),
                           
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_GateEntry>> getkeyword(GET_GateEntryDomain input)
        {
            List<GET_GateEntry> list = new List<GET_GateEntry>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_GateEntry]('" + input.PKID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_GateEntry
                        {
                            PKID = reader.GetString("PKID"),
                            
                            DATE = reader.GetString("DATE"),
                            VEHNO = reader.GetString("VEHNO"),
                            
                            MOBILENO = reader.GetString("MOBILENO"),
                            AADHARNO = reader.GetString("AADHARNO"),
                          
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            
                        });
                    }
                }
            }
            return list;
        }
    }
}
