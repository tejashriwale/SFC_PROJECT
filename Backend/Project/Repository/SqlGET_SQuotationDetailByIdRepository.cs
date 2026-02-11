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
                command.CommandText = "SELECT * FROM [GET_SQuotationDetailById]('" + input.SQAMAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
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
