using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_PurchaseOrderDataByIdRepository : IGET_PurchaseOrderDataByIdRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PurchaseOrderDataByIdRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_PurchaseOrderDataById>> getasync(GET_PurchaseOrderDataByIdDomain input)
        {
            List<GET_PurchaseOrderDataById> list = new List<GET_PurchaseOrderDataById>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PurchaseOrderDataById](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.POAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await  command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_PurchaseOrderDataById
                        {
                            POAID = reader.GetString("POAID"),
                            POTRANNO = reader.GetString("POTRANNO"),
                            POPLACE = reader.GetString("POPLACE"),
                            PODAID = reader.GetString("PODAID"),
                            PODPRODUCT = reader.GetString("PODPRODUCT"),
                            PODUOM = reader.GetString("PODUOM"),
                            PODRATE = reader.GetDouble("PODRATE"),
                            PODQUANTITY = reader.GetDouble("PODQUANTITY"),
                            PODTAMT = reader.GetDouble("PODTAMT"),
                            PODIGST = reader.GetDouble("PODIGST"),
                            PODSGST = reader.GetDouble("PODSGST"),
                            PODCGST = reader.GetDouble("PODCGST"),
                            PODTOTAL = reader.GetDouble("PODTOTAL"),
                            UOMTITLE = reader.GetString("UOMTITLE"),
                            PNAME = reader.GetString("PNAME"),
                            HSNCODE = reader.GetInt32("HSNCODE"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
