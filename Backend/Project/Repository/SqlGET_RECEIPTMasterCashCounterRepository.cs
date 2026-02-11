using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_RECEIPTMasterCashCounterRepository : IGET_RECEIPTMasterCashCounterRepositorycs
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_RECEIPTMasterCashCounterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_RECEIPTMasterCashCounter>> getasync(GET_RECEIPTMasterCashCounterDomain input)
        {
            List<GET_RECEIPTMasterCashCounter> list = new List<GET_RECEIPTMasterCashCounter>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_RECEIPTMasterCashCounter]('" + input.BAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' , '" + Convert.ToDateTime(input.DATE).ToString("yyyy-MM-dd") + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_RECEIPTMasterCashCounter
                        {
                            BAID = reader.GetString("BAID"),
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            DATE = reader.GetString("DATE"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            FADDHARNO = reader.GetString("FADDHARNO"),
                            STATUS = reader.GetString("STATUS"),
                            ISVYAPARIVERIFIED = reader.GetBoolean("ISVYAPARIVERIFIED"),
                            FCONTACTNO = reader.GetString("FCONTACTNO"),
                            FNAME = reader.GetString("FNAME"),
                            MAID = reader.GetString("MAID"),
                            TOKNNO = reader.GetString("TOKNNO"),
                            VEHNO = reader.GetString("VEHNO"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_RECEIPTMasterCashCounter>> getkeyword(GET_RECEIPTMasterCashCounterDomain input)
        {
            List<GET_RECEIPTMasterCashCounter> list = new List<GET_RECEIPTMasterCashCounter>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_RECEIPTMasterCashCounter]('" + input.BAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' , '" + Convert.ToDateTime(input.DATE).ToString("yyyy-MM-dd") + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_RECEIPTMasterCashCounter
                        {

                            BAID = reader.GetString("BAID"),
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            DATE = reader.GetString("DATE"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            FADDHARNO = reader.GetString("FADDHARNO"),
                            STATUS = reader.GetString("STATUS"),
                            ISVYAPARIVERIFIED = reader.GetBoolean("ISVYAPARIVERIFIED"),
                            FCONTACTNO = reader.GetString("FCONTACTNO"),
                            FNAME = reader.GetString("FNAME"),
                            MAID = reader.GetString("MAID"),
                            TOKNNO = reader.GetString("TOKNNO"),
                            VEHNO = reader.GetString("VEHNO"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),

                        });
                    }
                }
            }
            return list;
        }

    }
}
