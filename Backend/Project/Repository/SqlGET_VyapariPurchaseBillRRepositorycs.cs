using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_VyapariPurchaseBillRRepositorycs : IGET_VyapariPurchaseBillRRepositorycs
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariPurchaseBillRRepositorycs(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_VyapariPurchaseBillR>> getasync(GET_VyapariPurchaseBillRDomain input)
        {
            List<GET_VyapariPurchaseBillR> list = new List<GET_VyapariPurchaseBillR>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_VyapariPurchaseBillR]('" + input.VBKID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' , '" + input.STARTDATE + "' , '" + input.ENDTDATE + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VyapariPurchaseBillR
                        {
                            SEVA = reader.GetString("SEVA"),
                            SevaID = reader.GetString("SevaID"),
                            SevaValue = reader.GetString("SevaValue"),
                            nSERVICETYPETITLE = reader.GetString("nSERVICETYPETITLE"),
                            VBKID = reader.GetString("VBKID"),
                            BILLNO = reader.GetInt32("BILLNO"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
