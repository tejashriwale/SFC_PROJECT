using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VyapariAppRepository : IGET_VyapariAppRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariAppRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_VyapariApp>> getasync(GET_VyapariAppDomain input)
        {
            List<GET_VyapariApp> result = new List<GET_VyapariApp>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT TOP 20 * FROM [GET_VyapariApp]('" + input.BAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_VyapariApp
                        {
                            BAID = reader.GetString("BAID"),
                            MAID = reader.GetString("MAID"),
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            DATE = reader.GetString("DATE"),
                            ISVYAPARIVERIFIED = reader.GetBoolean("ISVYAPARIVERIFIED"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            RATE = reader.GetDouble("RATE"),
                            WEIGHTMINMAX = reader.GetString("WEIGHTMINMAX"),
                            FNAME = reader.GetString("FNAME"),
                        });
                    }
                }
            }
            return result;
        }

        public async Task<List<GET_VyapariApp>> getkeyword(GET_VyapariAppDomain input)
        {
            List<GET_VyapariApp> result = new List<GET_VyapariApp>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT TOP 20 * FROM [GET_VyapariApp]('" + input.BAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_VyapariApp
                        {
                            BAID = reader.GetString("BAID"),
                            MAID = reader.GetString("MAID"),
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            DATE = reader.GetString("DATE"),
                            ISVYAPARIVERIFIED = reader.GetBoolean("ISVYAPARIVERIFIED"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            RATE = reader.GetDouble("RATE"),
                            WEIGHTMINMAX = reader.GetString("WEIGHTMINMAX"),
                            FNAME = reader.GetString("FNAME"),
                        });
                    }
                }
            }
            return result;
        }
    }
}
