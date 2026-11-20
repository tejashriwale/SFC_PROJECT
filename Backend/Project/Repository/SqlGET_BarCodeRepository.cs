using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_BarCodeRepository : IGET_BarCodeRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_BarCodeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_BarCode>> getasync(GET_BarCodeDomain input)
        {
            List<GET_BarCode> list = new List<GET_BarCode>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_BarCode](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.BAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_BarCode
                        {
                            BAID = reader.GetString("BAID"),
                            BCATEGORY = reader.GetString("BCATEGORY"),
                            BPRODUCT = reader.GetString("BPRODUCT"),
                            BUOM = reader.GetString("BUOM"),
                            BBATCHNO = reader.GetString("BBATCHNO"),
                            BPRICE = reader.GetDouble("BPRICE"),
                            HSNCODE = reader.GetInt32("HSNCODE"),
                            BQUANTITY = reader.GetString("BQUANTITY"),
                            BARCODE = reader.GetString("BARCODE"),
                            BSHOWBATCHNO = reader.GetBoolean("BSHOWBATCHNO"),
                            BSHOWPRODUCTNAME = reader.GetBoolean("BSHOWPRODUCTNAME"),
                            BSHOWPRICE = reader.GetBoolean("BSHOWPRICE"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
