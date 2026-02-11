using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_DeficitRepository : IGET_DeficitRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_DeficitRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_Deficit>> get(GET_DeficitDomain input)
        {
            List<GET_Deficit> list = new List<GET_Deficit>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_Deficit]('" + input.DSID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_Deficit
                        {
                            DSID = reader.GetString("DSID"),
                            DATE = reader.GetString("DATE"),
                            STOCKNO = reader.GetString("STOCKNO"),
                            DESCRIPTION = reader.GetString("DESCRIPTION")
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_Deficit>> getasync(GET_DeficitDomain input)
        {
            List<GET_Deficit> list = new List<GET_Deficit>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_Deficit]('" + input.DSID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                   while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_Deficit
                        {
                            DSID = reader.GetString("DSID"),
                            DATE = reader.GetString("DATE"),
                            STOCKNO = reader.GetString("STOCKNO"),
                            DESCRIPTION = reader.GetString("DESCRIPTION")
                        });
                    }
                }
            }
            return list;
        }
    }
}
