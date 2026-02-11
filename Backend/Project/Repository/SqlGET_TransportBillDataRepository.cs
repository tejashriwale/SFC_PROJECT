using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_TransportBillDataRepository : IGET_TransportBillDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_TransportBillDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_TransportBillData>> getasync(GET_TransportBillDataDomain input)
        {
            List<GET_TransportBillData> result = new List<GET_TransportBillData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20  * FROM [GET_TransportBillData]('" + input.TRADER + "' ,  '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_TransportBillData
                        {
                            SLVMID = reader.GetString("SLVMID"),
                            CARETCOUNT = reader.GetString("CARETCOUNT"),
                            CROP = reader.GetString("CROP"),
                            CROP1 = reader.GetString("CROP1"),
                            TRADER = reader.GetString("TRADER"),
                            GRADE = reader.GetString("GRADE"),
                            GRADE1 = reader.GetString("GRADE1"),
                            LDATE = reader.GetString("LDATE"),
                            TOTIWDAMT = reader.GetDouble("TOTIWDAMT"),
                            TRADER1 = reader.GetString("TRADER1"),
                            VEHICLENO = reader.GetString("VEHICLENO"),
                        });
                    }
                }
            }
            return result;
        }
    }
}
