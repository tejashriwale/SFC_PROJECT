using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_GeneralGruopRepository : IGET_GeneralGruopRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_GeneralGruopRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_GeneralGruop>> getasync(GET_GeneralGruopDomain input)
        {
            List<GET_GeneralGruop> list = new List<GET_GeneralGruop>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_GeneralGruop]('" + input.SGRPID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_GeneralGruop
                        {
                            ACCTM = reader.GetString("ACCTM"),
                            GLRPID = reader.GetString("GLRPID"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
