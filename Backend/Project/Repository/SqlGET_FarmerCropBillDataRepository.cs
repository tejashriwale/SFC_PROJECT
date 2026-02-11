using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_FarmerCropBillDataRepository : IGET_FarmerCropBillDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_FarmerCropBillDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_FarmerCropBillData>> getasync(GET_FarmerCropBillDataDomain input)
        {
            List<GET_FarmerCropBillData> list = new List<GET_FarmerCropBillData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_FarmerCropBillData]('" + input.COMPANYID + "' , '" + input.DEPTID + "' , '" + input.MAID + "' , '" + input.CROPID + "' ) ORDER BY TRNDT DESC, TRNSR DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_FarmerCropBillData
                        {
                            TRNSR = reader.GetInt32("TRNSR"),
                            ADVANCEAMT = reader.GetDouble("ADVANCEAMT"),
                            MAID = reader.GetString("MAID"),
                            NARR = reader.GetString("NARR"),
                            BILLAMOUNT = reader.GetDouble("BILLAMOUNT"),
                            CROPNAME = reader.IsDBNull(reader.GetOrdinal("CROPNAME")) ? "" : reader.GetString("CROPNAME"),
                            FNAME = reader.GetString("FNAME"),
                            BILLNO = reader.GetString("BILLNO"),
                            CVARIETY = reader.IsDBNull(reader.GetOrdinal("CVARIETY")) ? "" : reader.GetString("CVARIETY"),
                            CGRADE = reader.IsDBNull(reader.GetOrdinal("CGRADE")) ? "" : reader.GetString("CGRADE"),
                            CROPID = reader.GetString("CROPID"),
                            PAYABLEAMT = reader.GetDouble("PAYABLEAMT"),
                            PKID = reader.GetString("PKID"),
                            REFAID = reader.GetString("REFAID"),
                            TRAID = reader.GetString("TRAID"),
                            TRNDT = reader.GetString("TRNDT"),
                            PAIDAMT = reader.GetDouble("PAIDAMT"),
                            CBAL = reader.GetDouble("CBAL"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
