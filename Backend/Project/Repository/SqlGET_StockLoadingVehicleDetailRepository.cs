using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_StockLoadingVehicleDetailRepository : IGET_StockLoadingVehicleDetailRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_StockLoadingVehicleDetailRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_StockLoadingVehicleDetail>> getasync(GET_StockLoadingVehicleDetailDomain input)
        {
            List<GET_StockLoadingVehicleDetail> list = new List<GET_StockLoadingVehicleDetail>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_StockLoadingVehicleDetail]('" + input.SVVMID + "', '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_StockLoadingVehicleDetail
                        {
                           CGRADE = reader.GetString("CGRADE"),
                           TRNDT = reader.GetString("TRNDT"),
                            FAID = reader.GetString("FAID"),
                           FNAME = reader.GetString("FNAME"),
                           TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),
                           SALEVALUE = reader.GetDouble("SALEVALUE"),
                           CROPNAME = reader.GetString("CROPNAME"),
                           GRADE = reader.GetString("GRADE"),
                           COUNT = reader.GetDouble("COUNT"),
                           SLVDID = reader.GetString("SLVDID"),
                           TOTCR = reader.GetDouble("TOTCR"),
                           CROP = reader.GetString("CROP"),
                           EXPENSE = reader.GetDouble("EXPENSE"),
                           CHARGES = reader.GetDouble("CHARGES"),
                           SLVMID = reader.GetString("SLVMID"),
                            STAID = reader.GetString("STAID"),
                            RATE = reader.GetDouble("RATE"),
                            SVALUE = reader.GetDouble("SVALUE"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
