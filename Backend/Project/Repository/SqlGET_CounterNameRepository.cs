using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_CounterNameRepository : IGET_CounterNameRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CounterNameRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CounterName>> getasync(GET_CounterNameDomain input)
        {
            List<GET_CounterName> list = new List<GET_CounterName>();
            {
                using(var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [GET_CounterName]('" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                    dbContext.Database.OpenConnection();

                    using(var reader = await command.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            list.Add(new GET_CounterName
                            {
                                CAID = reader.GetString("CAID"),
                                CNAME = reader.GetString("CNAME"),
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}
