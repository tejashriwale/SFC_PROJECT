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
                command.CommandText = "SELECT * FROM [GET_CropTransportInvoiceData](@p0 , @p1 , @p2 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SVVMID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
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
