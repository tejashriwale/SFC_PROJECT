using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_TopVyapariOnCratesRepository : IGET_TopVyapariOnCratesRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_TopVyapariOnCratesRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_TopVyapariOnCrates>> getasync(GET_TopVyapariOnCratesDomain input)
        {
            List<GET_TopVyapariOnCrates> list = new List<GET_TopVyapariOnCrates>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_TopVyapariOnCrates](@p0 , @p1 , @p2 , @p3, @p4 , @p5)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.VBKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(input.FROMDATE) ?? ""); command.Parameters.Add(command_p4); }
                { var command_p5 = command.CreateParameter(); command_p5.ParameterName = "@p5"; command_p5.Value = (object)(Convert.ToString(input.TODATE) ?? ""); command.Parameters.Add(command_p5); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_TopVyapariOnCrates
                        {
                            VBKID = reader.GetString("VBKID"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            TOTALJAALI = reader.GetInt32("TOTALJAALI"),
                            COMPANYID = reader.GetString("COMPANYID"),
                            DEPTID = reader.GetString("DEPTID"),
                            VSHORTNAME = reader.GetString("VSHORTNAME"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),


                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_TopVyapariOnCrates>> getkeyword(GET_TopVyapariOnCratesDomain input)
        {
            List<GET_TopVyapariOnCrates> list = new List<GET_TopVyapariOnCrates>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_TopVyapariOnCrates](@p0 , @p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.VBKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_TopVyapariOnCrates
                        {
                            VBKID = reader.GetString("VBKID"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            TOTALJAALI = reader.GetInt32("TOTALJAALI"),
                            COMPANYID = reader.GetString("COMPANYID"),
                            DEPTID = reader.GetString("DEPTID"),
                            VSHORTNAME = reader.GetString("VSHORTNAME"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),


                        });
                    }
                }
            }
            return list;
        }
    }
}
