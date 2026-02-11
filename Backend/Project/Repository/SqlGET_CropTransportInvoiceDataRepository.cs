using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CropTransportInvoiceDataRepository : IGET_CropTransportInvoiceDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CropTransportInvoiceDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CropTransportInvoiceData>> getasync(GET_CropTransportInvoiceDataDomain input)
        {
            List<GET_CropTransportInvoiceData> list = new List<GET_CropTransportInvoiceData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CropTransportInvoiceData]('" + input.SVVMID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CropTransportInvoiceData
                        {
                            TOTEXPENSE = reader.GetDouble("TOTEXPENSE"),
                            CGRADE = reader.GetString("CGRADE"),
                            CROPID = reader.GetString("CROPID"),
                            GRADE = reader.GetString("GRADE"),
                            ITEMNAME = reader.GetString("ITEMNAME"),
                            TOTCOUNT = reader.GetDouble("TOTCOUNT"),
                            TOTAMOUNT = reader.GetDouble("TOTAMOUNT"),
                            SVALUE = reader.GetDouble("SVALUE"),    
                            CTDAID = reader.GetString("CTDAID")

                        });
                    }
                }
            }
            return list;
        }
    }
}
