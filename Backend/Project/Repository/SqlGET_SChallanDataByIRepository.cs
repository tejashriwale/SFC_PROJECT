using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_SChallanDataByIRepository : IGET_SChallanDataByIRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SChallanDataByIRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SChallanDataByID>> getasync(GET_SChallanDataByIDDomain input)
        {
            List<GET_SChallanDataByID> list = new List<GET_SChallanDataByID> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SChallanDataByID]('" + input.SCAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_SChallanDataByID
                        {
                            SCTRNNO = reader.GetString("SCTRNNO"),
                            SCSELLER = reader.GetString("SCSELLER"),
                            SCCONSIGNER = reader.GetString("SCCONSIGNER"),
                            SCVNO = reader.GetString("SCVNO"),
                            SCPOSUPPLY = reader.GetString("SCPOSUPPLY"),
                            SCNAMT = reader.GetDouble("SCNAMT"),
                            SCGAMT = reader.GetDouble("SCGAMT"),
                            PAYMENTMODE = reader.GetString("PAYMENTMODE"),
                            SCDPRODUCT = reader.GetString("SCDPRODUCT"),
                            SCDUOM = reader.GetString("SCDUOM"),
                            SCDQUANTITY = reader.GetString("SCDQUANTITY"),
                            SCDRATE = reader.GetDouble("SCDRATE"),
                            SCDTAMT = reader.GetDouble("SCDTAMT"),
                            SCDTAXABLE = reader.GetString("SCDTAXABLE"),
                            SCDIGST = reader.GetDouble("SCDIGST"),
                            SCDSGST = reader.GetDouble("SCDSGST"),
                            SCDCGST = reader.GetDouble("SCDCGST"),
                            HSNCODE = reader.GetInt32("HSNCODE"),
                            BATCHNO = reader.GetString("BATCHNO"),
                            EXPIRYDATE = reader.GetString("EXPIRYDATE"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                            UOMTITLE = reader.GetString("UOMTITLE"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
