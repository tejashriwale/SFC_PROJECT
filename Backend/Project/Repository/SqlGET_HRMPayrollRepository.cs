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
                command.CommandText = "SELECT * FROM [GET_HRMPayroll](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
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
