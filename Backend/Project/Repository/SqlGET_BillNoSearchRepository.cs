using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_BillNoSearchRepository : IGET_BillNoSearchRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_BillNoSearchRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_BillNoSearch>> getasync(GET_BillNoSearchDomain input)
        {
            List<GET_BillNoSearch> list = new List<GET_BillNoSearch>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_BillNoSearch]('" + input.BTOKANNO + "' , '" + input.FNAME + "' ,'" + input.VEHICLENO + "' ,'" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_BillNoSearch
                        {
                          
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            DATE = reader.GetString("DATE"),
                            VEHICLENO = reader.GetString("VEHICLENO"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            STATUS = reader.GetString("STATUS"),
                            MAID = reader.GetString("MAID"),
                            FNAME = reader.GetString("FNAME"),
                            BAID = reader.GetString("BAID"),
                            SevaValue = reader.GetString("SevaValue"),
                            nSERVICETYPETITLE = reader.GetString("nSERVICETYPETITLE"),
                            ONLINEAMOUNT = reader.GetDouble("ONLINEAMOUNT"),
                            CASHAMOUNT = reader.GetDouble("CASHAMOUNT"),
                            CHEQUEAMOUNT = reader.GetDouble("CHEQUEAMOUNT"),
                            ISCLOSE = reader.GetBoolean("ISCLOSE")

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_BillNoSearch>> getkeyword(GET_BillNoSearchDomain input)
        {
            List<GET_BillNoSearch> list = new List<GET_BillNoSearch>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_BillNoSearch]('" + input.BTOKANNO + "' , '" + input.FNAME + "' ,'" + input.VEHICLENO + "' ,'" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_BillNoSearch
                        {
                           
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            DATE = reader.GetString("DATE"),
                            VEHICLENO = reader.GetString("VEHICLENO"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            STATUS = reader.GetString("STATUS"),
                            MAID = reader.GetString("MAID"),
                            FNAME = reader.GetString("FNAME"),
                            BAID = reader.GetString("BAID"),
                            SevaValue = reader.GetString("SevaValue"),
                            nSERVICETYPETITLE = reader.GetString("nSERVICETYPETITLE"),
                            ONLINEAMOUNT = reader.GetDouble("ONLINEAMOUNT"),
                            CASHAMOUNT = reader.GetDouble("CASHAMOUNT"),
                            CHEQUEAMOUNT = reader.GetDouble("CHEQUEAMOUNT"),
                            ISCLOSE = reader.GetBoolean("ISCLOSE")
                        });
                    }
                }
            }
            return list;
        }

    }
}
