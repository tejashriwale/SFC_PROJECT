using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_AUCTIONSHEDdataRepository : IGET_AUCTIONSHEDdataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_AUCTIONSHEDdataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_AUCTIONSHEDdata>> getasync(GET_AUCTIONSHEDdataDomain input)
        {
            List<GET_AUCTIONSHEDdata> data = new List<GET_AUCTIONSHEDdata>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_AUCTIONSHEDdata]('" + input.APKID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        data.Add(new GET_AUCTIONSHEDdata
                        {
                            APKID = reader.GetString("APKID"),
                            DATE = reader.GetString("DATE"),
                            MAIDD = reader.GetString("MAIDD"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            FNAMEE = reader.GetString("FNAMEE"),
                            HAMALI = reader.GetString("HAMALI"),
                            TOKENN = reader.GetString("TOKENN"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            SEVA = reader.GetString("SEVA"),
                            TOLAI = reader.GetString("TOLAI"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),
                            TOTALJALI = reader.GetInt32("TOTALJALI"),
                            VILLAGEE = reader.GetString("VILLAGEE"),
                            
                            

                        });
                    }
                }
            }
            return data;
        }
    }
}
