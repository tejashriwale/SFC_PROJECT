using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_GateDateRepository : IGET_GateDateRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_GateDateRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_GateDate>> getall()
        {
            List<GET_GateDate> result = new List<GET_GateDate>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_GateDate]()";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_GateDate
                        {
                            UREGISTRATIONDATE = reader.GetDateTime("UREGISTRATIONDATE"),
                            


                        });
                    }
                }
            }
            return result;
        }
    }
}
