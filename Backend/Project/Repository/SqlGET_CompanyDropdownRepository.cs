using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_CompanyDropdownRepository : IGET_CompanyDropdownRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CompanyDropdownRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CompanyDropdown>> getall()
        {
            List<GET_CompanyDropdown> list = new List<GET_CompanyDropdown>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CompanyDropdown]()";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_CompanyDropdown
                        {
                            CNAME = reader.GetString("CNAME"),
                            COMPANYID = reader.GetString("COMPANYID"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
