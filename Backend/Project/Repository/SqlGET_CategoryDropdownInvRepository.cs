using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CategoryDropdownInvRepository : IGET_CategoryDropdownInvRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CategoryDropdownInvRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CategoryDropdownInv>> getasync(GET_CategoryDropdownInvDomain input)
        {
            List<GET_CategoryDropdownInv> list = new List<GET_CategoryDropdownInv>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CategoryDropdownInv]('" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CategoryDropdownInv
                        {
                            CTID = reader.GetString("CTID"),
                            CATEGORYNAME = reader.GetString("CATEGORYNAME"),
                        });
                    }
                }
            }
            return list;
        }
    }
    
}
