using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public class SqlGET_CustomerDetailsByIDRepository : IGET_CustomerDetailsByIDRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CustomerDetailsByIDRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CustomerDetailsByID>> getasync(CounterMasterDomain masterDomain)
        {
           List<GET_CustomerDetailsByID> list = new List<GET_CustomerDetailsByID>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CustomerDetailsByID](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(masterDomain.PKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(masterDomain.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(masterDomain.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_CustomerDetailsByID
                        {
                            CAID = reader.GetString("CAID"),
                            CCOMPANYNAME = reader.GetString("CCOMPANYNAME"),
                            CCONTACTPERSONMOBILE = reader.GetString("CCONTACTPERSONMOBILE"),
                            CEMAIL = reader.GetString("CEMAIL"),
                            CLANDMARK = reader.GetString("CLANDMARK"),
                            CACCOUNTNUMBER = reader.GetString("CACCOUNTNUMBER"),
                            CBANKNAME = reader.GetString("CBANKNAME"),
                            CIFSC = reader.GetString("CIFSC"),
                            CBRANCHNAME = reader.GetString("CBRANCHNAME"),
                            CSTATE = reader.GetString("CSTATE"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
