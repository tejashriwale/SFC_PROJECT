using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_TopVyapariOnCratesRepository : IGET_TopVyapariOnCratesRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_TopVyapariOnCratesRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_TopVyapariOnCrates>> getasync(GET_TopVyapariOnCratesDomain input)
        {
            List<GET_TopVyapariOnCrates> list = new List<GET_TopVyapariOnCrates>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_TopVyapariOnCrates]('" + input.VBKID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "', '" + input.FROMDATE + "' , '" + input.TODATE + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_TopVyapariOnCrates
                        {
                            VBKID = reader.GetString("VBKID"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            TOTALJAALI = reader.GetInt32("TOTALJAALI"),
                            COMPANYID = reader.GetString("COMPANYID"),
                            DEPTID = reader.GetString("DEPTID"),
                            VSHORTNAME = reader.GetString("VSHORTNAME"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),


                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_TopVyapariOnCrates>> getkeyword(GET_TopVyapariOnCratesDomain input)
        {
            List<GET_TopVyapariOnCrates> list = new List<GET_TopVyapariOnCrates>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_TopVyapariOnCrates]('" + input.VBKID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_TopVyapariOnCrates
                        {
                            VBKID = reader.GetString("VBKID"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            TOTALJAALI = reader.GetInt32("TOTALJAALI"),
                            COMPANYID = reader.GetString("COMPANYID"),
                            DEPTID = reader.GetString("DEPTID"),
                            VSHORTNAME = reader.GetString("VSHORTNAME"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),


                        });
                    }
                }
            }
            return list;
        }
    }
}
