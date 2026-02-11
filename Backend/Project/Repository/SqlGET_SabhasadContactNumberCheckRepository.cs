using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_SabhasadContactNumberCheckRepository : IGET_SabhasadContactNumberCheckRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SabhasadContactNumberCheckRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SabhasadContactNumberCheck>> getasync(GET_SabhasadContactNumberCheckDomain input)
        {
            List<GET_SabhasadContactNumberCheck> result = new List<GET_SabhasadContactNumberCheck>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SabhasadContactNumberCheck]('" + input.CONTACTNO + "' ,  '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_SabhasadContactNumberCheck
                        {
                            SABHASADNAME = reader.GetString("SABHASADNAME"),
                            SRAID = reader.GetString("SRAID"),
                            CONTACTNUMBER = reader.GetString("CONTACTNUMBER")
                        });
                    }
                }

            }
            return result;
        }
    }
}
