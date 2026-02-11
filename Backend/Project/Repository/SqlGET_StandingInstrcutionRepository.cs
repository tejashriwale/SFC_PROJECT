using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_StandingInstrcutionRepository : IGET_StandingInstrcutionRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_StandingInstrcutionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_StandingInstrcution>> getasync(GET_StandingInstrcutionDomain input)
        {
            List<GET_StandingInstrcution> list = new List<GET_StandingInstrcution> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_StandingInstrcution]('" + input.SIID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_StandingInstrcution
                        {
                            SCID = reader.GetString("SCID"),
                            SHID = reader.GetString("SHID"),
                            NEWRATE = reader.GetDouble("NEWRATE"),
                            SIID = reader.GetString("SIID"),
                            RATE = reader.GetDouble("RATE"),
                        });
                    }
                }
            }
            return list;
        }

       
        
    }
}
