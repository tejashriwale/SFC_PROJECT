using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_ST_Implication_ShopLOCRepository : IGET_ST_Implication_ShopLOCRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ST_Implication_ShopLOCRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_ST_Implication_ShopLOC>> GetAll()
        {
            List<GET_ST_Implication_ShopLOC> list = new List<GET_ST_Implication_ShopLOC>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_ST_Implication_ShopLOC]()";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_ST_Implication_ShopLOC
                        {
                            CNT = reader.GetInt32("CNT")
                        });
                    }
                }
            }
            return list;
        }
    }
}
