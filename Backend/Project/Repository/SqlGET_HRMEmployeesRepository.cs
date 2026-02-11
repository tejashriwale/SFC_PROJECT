using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_HRMEmployeesRepository : IGET_HRMEmployeesRepository
    {
        private readonly  ApplicationDbContext dbContext;
        public SqlGET_HRMEmployeesRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_HRMEmployees>> getasync(GET_HRMEmployeesDomain input)
        {
            List<GET_HRMEmployees> list = new List<GET_HRMEmployees> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText= "SELECT * FROM [GET_HRMEmployees]( '" + input.KEYWORD + "','" + input.EMPID+"' ,  '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_HRMEmployees
                        {
                            EMPID = reader.GetString("EMPID"),
                            EMPNAME = reader.GetString("EMPNAME"),
                            EMPEMAIL = reader.GetString("EMPEMAIL"),
                            EMPCONTACT = reader.GetString("EMPCONTACT"),
                            EMPCODE = reader.GetString("EMPCODE"),
                            EMPDOB = reader.GetString("EMPDOB"),
                            EMPGENDER = reader.GetString("EMPGENDER"),
                            EMPNATIONALITY = reader.GetString("EMPNATIONALITY"),
                            EMPJOINDATE = reader.GetString("EMPJOINDATE"),
                            EMPSHIFT = reader.GetString("EMPSHIFT"),
                            EMPDEPARTMENT = reader.GetString("EMPDEPARTMENT"),
                            EMPDESIGNATION = reader.GetString("EMPDESIGNATION"),
                            EMPBLOODGROUP = reader.GetString("EMPBLOODGROUP"),
                            EMPPINCODE = reader.GetString("EMPPINCODE"),
                            EMPLANDMARK = reader.GetString("EMPLANDMARK"),
                            EMPCITY = reader.GetString("EMPCITY"),
                            EMPSTATE = reader.GetString("EMPSTATE"),
                            EMPADDRESS = reader.GetString("EMPADDRESS"),
                            EMPAREA = reader.GetString("EMPAREA"),
                            EMPPHOTO = reader.GetString("EMPPHOTO"),
                            EMPSTATUS = reader.GetString("EMPSTATUS"),
                            dname = reader.GetString("dname"),
                            dsname = reader.GetString("dsname"),
                            AADHAAR = reader.GetString("AADHAAR"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
