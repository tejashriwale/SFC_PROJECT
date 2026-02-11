using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VapariStandingInstrcutionRepository : IGET_VapariStandingInstrcutionRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VapariStandingInstrcutionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_VapariStandingInstrcution>> getsync(GET_VapariStandingInstrcutionDomain input)
        {
            List<GET_VapariStandingInstrcution> list = new List<GET_VapariStandingInstrcution> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_VapariStandingInstrcution]('" + input.SHID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_VapariStandingInstrcution
                        {
                          
                            RATETYPE = reader.GetString("RATETYPE"),
                            
                            NEWRATE = reader.GetDouble("NEWRATE"),
                            
                            PKID = reader.GetString("PKID"),
                            SERVICETYPE = reader.GetString("SERVICETYPE"),
                        });
                    }
                }
            }
            return list;

        }
    }
}
