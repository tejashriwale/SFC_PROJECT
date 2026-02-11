using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGETItemDropdownRepository : IGETItemDropdownRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGETItemDropdownRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GETItemDropdown>> getasync(GETItemDropdownDomain input)
        {
            List<GETItemDropdown> list = new List<GETItemDropdown>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GETItemDropdown]('" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();
                
                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GETItemDropdown
                        {
                            ITEMAID = reader.GetString("ITEMAID"),
                            ITEMNM = reader.GetString("ITEMNM"),
                            ITEMNE = reader.GetString("ITEMNE"),
                        });
                    }
                }

            }
            return list;
            
        }
    }
}
