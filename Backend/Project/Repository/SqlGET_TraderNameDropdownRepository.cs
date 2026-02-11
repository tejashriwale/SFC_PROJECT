using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_TraderNameDropdownRepository : IGET_TraderNameDropdownRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_TraderNameDropdownRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_TraderNameDropdown>> getasync(GET_TraderNameDropdownDomain input)
        {
            List<GET_TraderNameDropdown> result = new List<GET_TraderNameDropdown>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_TraderNameDropdown]('" + input.CTYPE + "' ,  '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_TraderNameDropdown
                        {
                            CAID		 = reader.GetString("CAID"),
                            CCOMPANYNAME = reader.GetString("CCOMPANYNAME"),
                           
                        });
                    }
                }
            }
            return result;
        }
    }
}
