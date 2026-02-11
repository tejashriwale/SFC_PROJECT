using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_HRMPayrollRepository : IGET_HRMPayrollRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_HRMPayrollRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_HRMPayroll>> getasync(GET_HRMPayrollDomain input)
        {
            List<GET_HRMPayroll> list = new List<GET_HRMPayroll>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_HRMPayroll]('" + input.PAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_HRMPayroll
                        {
                            PAID = reader.GetString("PAID"),
                            PEMPLOYEE = reader.GetString("PEMPLOYEE"),
                            PBASICSALARY = reader.GetDouble("PBASICSALARY"),
                            PSTATUS = reader.GetBoolean("PSTATUS"),
                            PHRAALLOWANCE = reader.GetDouble("PHRAALLOWANCE"),
                            PCONVEYANCE = reader.GetDouble("PCONVEYANCE"),
                            PMEDICALALLOWANCE = reader.GetDouble("PMEDICALALLOWANCE"),
                            PBONUS = reader.GetDouble("PBONUS"),
                            PALLOWANCESOTHER = reader.GetDouble("PALLOWANCESOTHER"),
                            PPF = reader.GetDouble("PPF"),
                            PPROFESSIONALTAX = reader.GetDouble("PPROFESSIONALTAX"),
                            PTDS = reader.GetDouble("PTDS"),
                            PLOANANDOTHER = reader.GetDouble("PLOANANDOTHER"),
                            PDEDUCTIONOTHER = reader.GetDouble("PDEDUCTIONOTHER"),
                            PTOTALALLOWANCE = reader.GetDouble("PTOTALALLOWANCE"),
                            PTOTALDEDUCTION = reader.GetDouble("PTOTALDEDUCTION"),
                            PNETSALAREY = reader.GetDouble("PNETSALAREY"),
                            EMPNAME = reader.GetString("EMPNAME"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
