using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public class SqlIGET_CustomerDetailsRepository : IGET_CustomerDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlIGET_CustomerDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_CustomerDetails>> getasync(CustomerDetailsDomain input)
        {
           List<GET_CustomerDetails> list = new List<GET_CustomerDetails>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CustomerDetails]('"+input.CAID+"','" + input.PRAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await  command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_CustomerDetails
                        {
                            caid = reader.GetString("caid"),
                            CCOMPANYNAME = reader.GetString("CCOMPANYNAME"),
                            CCONTACTPERSONMOBILE = reader.GetString("CCONTACTPERSONMOBILE"),
                            CEMAIL = reader.GetString("CEMAIL"),
                            CLANDMARK = reader.GetString("CLANDMARK"),
                            CACCOUNTNUMBER = reader.GetString("CACCOUNTNUMBER"),
                            CBANKNAME = reader.GetString("CBANKNAME"),
                            CIFSC = reader.GetString("CIFSC"),
                            CBRANCHNAME = reader.GetString("CBRANCHNAME"),
                            CSTATE = reader.GetString("CSTATE"),
                            STATUS = reader.GetInt32("STATUS"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
