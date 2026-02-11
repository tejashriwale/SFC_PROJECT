using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_BillNORepository : IGET_BillNORepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_BillNORepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_BillNO>> getasync(GET_BillNODomain input)
        {
            List<GET_BillNO> list = new List<GET_BillNO>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_BillNO]('" + input.SBBILLNO + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_BillNO
                        {
                          SBBILLNO = reader.GetString("SBBILLNO")
                        });
                    }
                }
            }
            return list;
        }
    }
}
