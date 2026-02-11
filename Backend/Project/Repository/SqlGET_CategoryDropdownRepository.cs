using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_CategoryDropdownRepository : IGET_CategoryDropdownRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CategoryDropdownRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CategoryDropdown>> getasync(GET_CategoryDropdownDomain input)
        {
            List<GET_CategoryDropdown> list = new List<GET_CategoryDropdown>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CategoryDropdown]('" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_CategoryDropdown
                        {
                            CATEGORYNAME = reader.GetString("CATEGORYNAME"),
                            CTID = reader.GetString("CTID"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
