using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_ACCSESSRIGHTDropdownSFCRepository : IGET_ACCSESSRIGHTDropdownSFCRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ACCSESSRIGHTDropdownSFCRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_ACCSESSRIGHTDropdownSFC>> getasync(GET_ACCSESSRIGHTDropdownSFCDomain input)
        {
            List<GET_ACCSESSRIGHTDropdownSFC> list = new List<GET_ACCSESSRIGHTDropdownSFC>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_ACCSESSRIGHTDropdownSFC]('" + input.COMPANYID + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_ACCSESSRIGHTDropdownSFC
                        {
                            
                            AccessRightID = reader.GetString("AccessRightID"),
                            AccessRightTitle = reader.GetString("AccessRightTitle"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
