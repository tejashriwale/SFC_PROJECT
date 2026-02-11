using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_SContactNumberCheckRepository : IGET_SContactNumberCheckRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SContactNumberCheckRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SContactNumberCheck>> getasync(GET_SContactNumberCheckDomain input)
        {
            List<GET_SContactNumberCheck> list = new List<GET_SContactNumberCheck>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_SContactNumberCheck]('" + input.CONTACTNUMBER + "'  , '" + input.COMPANYID + "' , '" + input.DEPTID + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_SContactNumberCheck
                        {
                           
                            UAID = reader.GetString("UAID"),
                            UFORENAME = reader.GetString("UFORENAME"),
                            UMOBILENUMBER = reader.GetString("UMOBILENUMBER")


                        });
                    }
                }
            }
            return list;

        }
    }
}
