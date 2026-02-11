using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_chalanNORepository : IGET_chalanNORepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_chalanNORepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_chalanNO>> getasync(GET_chalanNODomain input)
        {
            List<GET_chalanNO> list = new List<GET_chalanNO>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECt  * FROM [GET_chalanNO]('" + input.SQNO + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_chalanNO
                        {
                            SQNO = reader.GetString("SQNO"),


                        });
                    }

                }
            }
            return list;
        }
    }
}
