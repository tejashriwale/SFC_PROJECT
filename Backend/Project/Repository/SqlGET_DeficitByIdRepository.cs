using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_DeficitByIdRepository : IGET_DeficitByIdRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_DeficitByIdRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_DeficitById>> getasync(GET_DeficitByIdDomain input)
        {
            List<GET_DeficitById> list = new List<GET_DeficitById>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_DeficitById](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.DSID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_DeficitById
                        {
                            DSID = reader.GetString("DSID"),
                            PKID = reader.GetString("PKID"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                            UNIT = reader.GetString("UNIT"),
                            QUANTITY = reader.GetInt32("QUANTITY"),
                            PRICE = reader.GetDouble("PRICE"),
                            TOTAL = reader.GetDouble("TOTAL"),
                            REMAININGSTOCK = reader.GetString("REMAININGSTOCK"),
                            BATCHNO = reader.GetString("BATCHNO"),
                            BATCHDATE = reader.GetString("BATCHDATE"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
