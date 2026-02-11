using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_UOMImplicationRepository : IGET_UOMImplicationRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_UOMImplicationRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_UOMImplication>> getasync(GET_UOMImplicationDomain input)
        {
            List<GET_UOMImplication> list = new List<GET_UOMImplication>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_UOMImplication]('" + input.PSALE + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_UOMImplication
                        {
                            IGroup = reader.GetString("IGroup"),
                            ITitle = reader.GetString("ITitle"),
                            IValue = reader.GetString("IValue"),
                        });
                    }
                }

            }
            return list;
        }
    }
}
