using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlIGET_CategoryCustomerRepository : IGET_CategoryCustomerRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlIGET_CategoryCustomerRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_CategoryCustomer>> getasync(GET_CategoryCustomerDomain customer)
        {
            List<GET_CategoryCustomer> list = new List<GET_CategoryCustomer>();

            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CategoryCustomer](@p0,@p1 , @p2, @p3  )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(customer.CTAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(customer.CTYPE) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(customer.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(customer.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_CategoryCustomer
                        {
                            CAID = reader.GetString("CAID"),
                            CCOMPANYNAME = reader.GetString("CCOMPANYNAME"),
                            CEMAIL = reader.GetString("CEMAIL"),
                            SSTATENAME = reader.GetString("SSTATENAME"),
                            CCONTACTPERSONMOBILE = reader.GetString("CCONTACTPERSONMOBILE"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
