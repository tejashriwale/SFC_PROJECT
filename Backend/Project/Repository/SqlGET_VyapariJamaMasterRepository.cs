using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VyapariJamaMasterRepository : IGET_VyapariJamaMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariJamaMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_VyapariJamaMaster>> getasync(GET_VyapariJamaMasterDomain input)
        {
            List<GET_VyapariJamaMaster> list = new List<GET_VyapariJamaMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_VyapariJamaMaster](@p0 , @p1 , @p2 , @p3 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.VJAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VyapariJamaMaster
                        {
                            VJAID = reader.GetString("VJAID"),
                            VYAPARI = reader.GetString("VYAPARI"),
                            TRANSACTIONMODE = reader.GetString("TRANSACTIONMODE"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            PHONENUMBER = reader.GetString("PHONENUMBER"),
                            COMPANYNAME = reader.GetString("COMPANYNAME"),
                            GSTNNUMBER = reader.GetString("GSTNNUMBER"),
                            TOTALRECEIVINGAMOUNT = reader.GetDouble("TOTALRECEIVINGAMOUNT"),
                            PAIDAMOUNT = reader.GetDouble("PAIDAMOUNT"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            DESCRIPTION = reader.GetString("DESCRIPTION"),
                            DATE = reader.GetString("DATE"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_VyapariJamaMaster>> getkeyword(GET_VyapariJamaMasterDomain input)
        {
            List<GET_VyapariJamaMaster> list = new List<GET_VyapariJamaMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_VyapariJamaMaster](@p0 , @p1 , @p2 , @p3 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.VJAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VyapariJamaMaster
                        {
                            VJAID = reader.GetString("VJAID"),
                            VYAPARI = reader.GetString("VYAPARI"),
                            TRANSACTIONMODE = reader.GetString("TRANSACTIONMODE"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            PHONENUMBER = reader.GetString("PHONENUMBER"),
                            COMPANYNAME = reader.GetString("COMPANYNAME"),
                            GSTNNUMBER = reader.GetString("GSTNNUMBER"),
                            TOTALRECEIVINGAMOUNT = reader.GetDouble("TOTALRECEIVINGAMOUNT"),
                            PAIDAMOUNT = reader.GetDouble("PAIDAMOUNT"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            DESCRIPTION = reader.GetString("DESCRIPTION"),
                            DATE = reader.GetString("DATE"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
