using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_StockTransferDRepository : IGET_StockTransferDRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_StockTransferDRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_StockTransferD>> getasync(GET_StockTransferDDomain input)
        {
            List<GET_StockTransferD> list = new List<GET_StockTransferD>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_StockTransferD]('" + input.STDID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_StockTransferD
                        {
                            TDID = reader.GetString("TDID"),
                            STMID = reader.GetString("STMID"),
                            STDPRODUCT = reader.GetString("STDPRODUCT"),
                            STDHSNCODE = reader.GetString("STDHSNCODE"),
                            STDREMS = reader.GetInt32("STDREMS"),
                            STDUOM = reader.GetString("STDUOM"),
                            STDQTY = reader.GetInt32("STDQTY"),
                            STDRATE = reader.GetDouble("STDRATE"),
                            STDTOTAL = reader.GetDouble("STDTOTAL"),
                            STDBATCHNO = reader.GetString("STDBATCHNO"),
                            STDBATACHDATE = reader.GetString("STDBATACHDATE"),
                            UOMTITLE = reader.GetString("UOMTITLE"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
