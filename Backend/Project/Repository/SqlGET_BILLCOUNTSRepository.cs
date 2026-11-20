using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_BILLCOUNTSRepository : IGET_BILLCOUNTSRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_BILLCOUNTSRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_BILLCOUNTS>> getasync(GET_BILLCOUNTSDomain input)
        {
            List<GET_BILLCOUNTS> list = new List<GET_BILLCOUNTS>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_BILLCOUNTS](@p0 , @p1)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_BILLCOUNTS
                        {
                            BillCount = reader.GetInt32("BillCount")
                        });
                    }
                }
            }
            return list;
        }
    }
}
