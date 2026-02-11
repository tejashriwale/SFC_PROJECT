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
                command.CommandText = "SELECT * FROM [GET_CustomerDetailsByID]('" + masterDomain.PKID + "' , '" + masterDomain.COMPANYID + "' , '" + masterDomain.DEPTID + "')";
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
