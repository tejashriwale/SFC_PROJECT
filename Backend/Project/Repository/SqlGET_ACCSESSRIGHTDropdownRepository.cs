using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_ACCSESSRIGHTDropdownRepository : IGET_ACCSESSRIGHTDropdownRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ACCSESSRIGHTDropdownRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_ACCSESSRIGHTDropdown>> GetAll()
        {
            List<GET_ACCSESSRIGHTDropdown> list = new List<GET_ACCSESSRIGHTDropdown>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_ACCSESSRIGHTDropdown]()";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_ACCSESSRIGHTDropdown
                        {
                            AccessRightID = reader.GetString("AccessRightID"),
                            AccessRightTitle = reader.GetString("AccessRightTitle"),
                        });
                    }
                }
                return list;
            }
        }
    }
}
