using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_ReceiptDetailsByIdRepository : IGET_ReceiptDetailsByIdRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ReceiptDetailsByIdRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_ReceiptDetailsById>> getasync(GET_ReceiptDetailsByIdDomain input)
        {
            List<GET_ReceiptDetailsById> result = new List<GET_ReceiptDetailsById>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_ReceiptDetailsById](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.BAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_ReceiptDetailsById
                        {
                            BAID = reader.GetString("BAID"),
                            BDAID = reader.GetString("BDAID"),
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            CROPTYPE = reader.GetString("CROPTYPE"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            JALI = reader.GetInt32("JALI"),
                            RATE = reader.GetDouble("RATE"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            WEIGHTMINMAX = reader.GetString("WEIGHTMINMAX")

                        });
                    }
                }
            }
            return result;
        }
    }
}
