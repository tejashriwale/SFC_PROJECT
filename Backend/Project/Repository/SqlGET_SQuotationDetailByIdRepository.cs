using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_SQuotationDetailByIdRepository : IGET_SQuotationDetailByIdRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SQuotationDetailByIdRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SQuotationDetailById>> getasync(GET_SQuotationDetailByIdDomain input)
        {
             List<GET_SQuotationDetailById> list = new List<GET_SQuotationDetailById>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SQuotationDetailById](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SQAMAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_SQuotationDetailById
                        {
                            SQAMAID = reader.GetString("SQAMAID"),
                            SQAMDAID = reader.GetString("SQAMDAID"),
                            SQPRODUCT = reader.GetString("SQPRODUCT"),
                            SQUOM = reader.GetString("SQUOM"),
                            SQQUANTITY = reader.GetString("SQQUANTITY"),
                            SQRATE = reader.GetDouble("SQRATE"),
                            SQTAMT = reader.GetDouble("SQTAMT"),
                            SQIGST = reader.GetDouble("SQIGST"),
                            SQSGST = reader.GetDouble("SQSGST"),
                            SQCGST = reader.GetDouble("SQCGST"),
                            HSNCODE = reader.GetInt32("HSNCODE"),
                            UOMTITLE = reader.GetString("UOMTITLE"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            SQTAXBALEAMOUNT = reader.GetDouble("SQTAXBALEAMOUNT"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
