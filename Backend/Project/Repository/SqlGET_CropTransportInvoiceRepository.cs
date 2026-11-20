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
                command.CommandText = "SELECT * FROM [GET_CropTransportInvoice](@p0 ,  @p1   ,@p2 ,  @p3   ,@p4 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.CTAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.STARTDATE) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(input.ENDDATE) ?? ""); command.Parameters.Add(command_p4); }
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
