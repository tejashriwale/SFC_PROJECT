using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_SOrderNoSearchRepository : IGET_SOrderNoSearchRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SOrderNoSearchRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_SOrderNoSearch>> getasync(GET_SOrderNoSearchDomain input)
        {
           List<GET_SOrderNoSearch> list = new List<GET_SOrderNoSearch> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand ())
            {
                command.CommandText = "SELECT * FROM [GET_SOrderNoSearch]('" + input.SOTRANNO + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_SOrderNoSearch
                        {
                            CAID = reader.GetString("CAID"),
                            SOCUSTMID = reader.GetString("SOCUSTMID"),
                            SODRATE = reader.GetDouble("SODRATE"),
                            SODTAMT = reader.GetDouble("SODTAMT"),
                            SODIGST = reader.GetDouble("SODIGST"),
                            SODSGST = reader.GetDouble("SODSGST"),
                            SODCGST = reader.GetDouble("SODCGST"),
                            SODQUANTITY = reader.GetString("SODQUANTITY"),
                            UOMTITLE = reader.GetString("UOMTITLE"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                            SODPRODUCT = reader.GetString("SODPRODUCT"),
                            SODUOM = reader.GetString("SODUOM"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
