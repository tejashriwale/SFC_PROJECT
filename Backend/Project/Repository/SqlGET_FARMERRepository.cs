using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_FARMERRepository : IGET_FARMERRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_FARMERRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_FARMER>> getasync(GET_FARMERDomain input)
        {
            List<GET_FARMER> result = new List<GET_FARMER>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_FARMER]('" + input.VBKID + "', '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_FARMER
                        {
                            VBKID = reader.GetString("VBKID"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            FNAME = reader.GetString("FNAME"),
                            

                        });
                    }
                }
            }
            return result;
        }

        public async Task<List<GET_FARMER>> getkeyword(GET_FARMERDomain input)
        {
            List<GET_FARMER> result = new List<GET_FARMER>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_FARMER]('" + input.VBKID + "' , '" + input.KEYWORD + "','" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_FARMER
                        {
                            VBKID = reader.GetString("VBKID"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            FNAME = reader.GetString("FNAME"),

                        });
                    }
                }
            }
            return result;
        }
    }
    
}
