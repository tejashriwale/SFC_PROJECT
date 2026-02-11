using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_SBillDetailsRepository : IGET_SBillDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SBillDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SBillDetails>> getasync(GET_SBillDetailsDomain input)
        {
            List<GET_SBillDetails> list = new List<GET_SBillDetails>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SBillDetails] ('" + input.SBDAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                   while(await reader.ReadAsync())
                    {
                        list.Add(new GET_SBillDetails
                        {
                            SBAID = reader.GetString("SBAID"),
                            SBDAID = reader.GetString("SBDAID"),
                            SBPRODUCT = reader.GetString("SBPRODUCT"),
                            SBDUOM = reader.GetString("SBDUOM"),
                            SBDQUANTITY = reader.GetString("SBDQUANTITY"),
                            SBDRATE = reader.GetDouble("SBDRATE"),
                            SBDTAMT = reader.GetDouble("SBDTAMT"),
                            SBDIGST = reader.GetDouble("SBDIGST"),
                            SBDSGST = reader.GetDouble("SBDSGST"),
                            SBDCGST = reader.GetDouble("SBDCGST"),
                            HSNCODE = reader.GetInt32("HSNCODE"),
                            BATCHNO = reader.GetString("BATCHNO"),
                            EXPIRYDATE = reader.GetString("EXPIRYDATE"),
                            SBDTOTAL = reader.GetDouble("SBDTOTAL"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
