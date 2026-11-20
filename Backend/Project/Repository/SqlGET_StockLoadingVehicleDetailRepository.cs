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
                command.CommandText = "SELECT  * FROM [GET_StockLoadingVehicleDetail](@p0, @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SVVMID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
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
                            MSIZE = "",

                        });
                    }
                }
            }
            return list;
        }
    }
}
