using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CropTransportInvoiceRepository : IGET_CropTransportInvoiceRepository
    {
        private readonly  ApplicationDbContext dbContext;
        public SqlGET_CropTransportInvoiceRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CropTransportInvoice>> getasync(GET_CropTransportInvoiceDomain input)
        {
            List<GET_CropTransportInvoice> list = new List<GET_CropTransportInvoice>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CropTransportInvoice]('" + input.CTAID + "' ,  '" + input.COMPANYID + "'   ,'" + input.DEPTID + "' ,  '" + input.STARTDATE + "'   ,'" + input.ENDDATE + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CropTransportInvoice
                        {
                             SAMT = reader.GetDouble("SAMT"),
                             CGRADE = reader.GetString("CGRADE"),
                             CTAID = reader.GetString("CTAID"),
                             CTDAID = reader.GetString("CTDAID"),
                             CROP = reader.GetString("CROP"),
                             CRTCNT = reader.GetInt32("CRTCNT"),
                             DATE = reader.GetString("DATE"),
                             EXAMT = reader.GetDouble("EXAMT"),
                             IWDAMT = reader.GetDouble("IWDAMT"),
                             VEHCHNO = reader.GetString("VEHCHNO"),
                             ITEMNE = reader.GetString("ITEMNE"),
                            IMPLICATIONTitle = reader.GetString("IMPLICATIONTitle"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            FNAME = reader.GetString("FNAME")

                        });
                    }
                }
            }
            return list;
        }
    }
}
