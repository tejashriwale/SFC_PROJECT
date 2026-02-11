using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_challan_from_Return_masterRepository : IGET_challan_from_Return_masterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_challan_from_Return_masterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_challan_from_Return_master>> getasync(GET_challan_from_Return_masterDomain input)
        {
            List<GET_challan_from_Return_master> list = new List<GET_challan_from_Return_master>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_challan_from_Return_master]('" + input.PBNO + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_challan_from_Return_master
                        {
                            PBNO = reader.GetString("PBNO")

                        });
                    }
                }
            }
            return list;
        }
    }
}
