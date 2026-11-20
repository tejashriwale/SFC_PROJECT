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
                command.CommandText = "SELECT * FROM [GET_SOrderNoSearch](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SOTRANNO) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
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
