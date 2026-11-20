using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_RECEIPTMasterCashCounterRepository : IGET_RECEIPTMasterCashCounterRepositorycs
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_RECEIPTMasterCashCounterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_RECEIPTMasterCashCounter>> getasync(GET_RECEIPTMasterCashCounterDomain input)
        {
            List<GET_RECEIPTMasterCashCounter> list = new List<GET_RECEIPTMasterCashCounter>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_RECEIPTMasterCashCounter](@p0 , @p1 , @p2 , @p3 , @p4 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.BAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(Convert.ToDateTime(input.DATE).ToString("yyyy-MM-dd")) ?? ""); command.Parameters.Add(command_p4); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_RECEIPTMasterCashCounter
                        {
                            BAID = reader.GetString("BAID"),
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            DATE = reader.GetString("DATE"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            FADDHARNO = reader.GetString("FADDHARNO"),
                            STATUS = reader.GetString("STATUS"),
                            ISVYAPARIVERIFIED = reader.GetBoolean("ISVYAPARIVERIFIED"),
                            FCONTACTNO = reader.GetString("FCONTACTNO"),
                            FNAME = reader.GetString("FNAME"),
                            MAID = reader.GetString("MAID"),
                            TOKNNO = reader.GetString("TOKNNO"),
                            VEHNO = reader.GetString("VEHNO"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_RECEIPTMasterCashCounter>> getkeyword(GET_RECEIPTMasterCashCounterDomain input)
        {
            List<GET_RECEIPTMasterCashCounter> list = new List<GET_RECEIPTMasterCashCounter>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_RECEIPTMasterCashCounter](@p0 , @p1 , @p2 , @p3 , @p4)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.BAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(Convert.ToDateTime(input.DATE).ToString("yyyy-MM-dd")) ?? ""); command.Parameters.Add(command_p4); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_RECEIPTMasterCashCounter
                        {

                            BAID = reader.GetString("BAID"),
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            DATE = reader.GetString("DATE"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            FADDHARNO = reader.GetString("FADDHARNO"),
                            STATUS = reader.GetString("STATUS"),
                            ISVYAPARIVERIFIED = reader.GetBoolean("ISVYAPARIVERIFIED"),
                            FCONTACTNO = reader.GetString("FCONTACTNO"),
                            FNAME = reader.GetString("FNAME"),
                            MAID = reader.GetString("MAID"),
                            TOKNNO = reader.GetString("TOKNNO"),
                            VEHNO = reader.GetString("VEHNO"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),

                        });
                    }
                }
            }
            return list;
        }

    }
}
