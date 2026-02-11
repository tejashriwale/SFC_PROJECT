using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CustomerTransportBillDataRepository : IGET_CustomerTransportBillDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CustomerTransportBillDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CustomerTransportBillData>> getasync(GET_CustomerTransportBillDataDomain input)
        {
            List<GET_CustomerTransportBillData> list = new List<GET_CustomerTransportBillData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_CustomerTransportBillData]('" + input.COMPANYID + "' , '" + input.DEPTID + "' , '" + input.MAID + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CustomerTransportBillData
                        {
                           CUSTOMERNAME = reader.GetString("CUSTOMERNAME"),
                           ADVANCEAMT   = reader.GetDouble("ADVANCEAMT"),
                           MAID        = reader.GetString("MAID"),
                           BILLAMOUNT = reader.GetDouble("BILLAMOUNT"),
                           CBAL = reader.GetDouble("CBAL"),
                           CROPNAME = reader.GetString("CROPNAME"),
                           REFAID = reader.GetString("REFAID"),
                            CROPID = reader.GetString("CROPID"),
                            PENDINGRECIVE = reader.GetDouble("PENDINGRECIVE"),
                            RESIVEDAMT = reader.GetDouble("RESIVEDAMT"),
                            TRNDT = reader.GetString("TRNDT"),
                            TRNSR = reader.GetInt32("TRNSR"),
                            TRAID = reader.GetString("TRAID"),
                            VEHCHNO = reader.GetString("VEHCHNO"),
                            NARR = reader.GetString("NARR"),
                            

                        });
                    }
                }
            }
            return list;
        }
    }
}
