using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_PARKINGRepository : IGET_PARKINGRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PARKINGRepository(ApplicationDbContext dbContext)
        {
           this.dbContext = dbContext;
        }
        public async Task<List<GET_PARKING>> GetAsync(GET_PARKINGDomain input)
        {
            List<GET_PARKING> result = new List<GET_PARKING>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PARKING](@p0,@p1,@p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_PARKING
                        {
                            PAID = reader.GetString("PAID"),
                            AUTOID = reader.GetString("AUTOID"),
                           DATE		= reader.GetString("DATE"),
                           TYPE		= reader.GetString("TYPE"),
                           TOTALJALI	= reader.GetInt32("TOTALJALI"),
                           VNUMBER		= reader.GetString("VNUMBER"),
                           VOWNERNAME	= reader.GetString("VOWNERNAME"),
                           VTYPE		= reader.GetString("VTYPE"),
                           VCHARGE		= reader.GetDouble("VCHARGE"),
                           STATUS		= reader.GetBoolean("STATUS"),
                            PARKTYPE = reader.GetString("PARKTYPE"),
                            NAVEKHATE = reader.GetString("NAVEKHATE"),
                            TRANSACTIONMODE = reader.GetString("TRANSACTIONMODE")
                        });
                    }
                }
            }
            return result;
        }
        public async Task<List<GET_PARKING>> getkeyword(GET_PARKINGDomain input)
        {
            List<GET_PARKING> list = new List<GET_PARKING>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PARKING](@p0,@p1,@p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_PARKING
                        {
                            PAID = reader.GetString("PAID"),
                            AUTOID = reader.GetString("AUTOID"),
                            DATE = reader.GetString("DATE"),
                            TYPE = reader.GetString("TYPE"),
                            TOTALJALI = reader.GetInt32("TOTALJALI"),
                            VNUMBER = reader.GetString("VNUMBER"),
                            VOWNERNAME = reader.GetString("VOWNERNAME"),
                            VTYPE = reader.GetString("VTYPE"),
                            VCHARGE = reader.GetDouble("VCHARGE"),
                            STATUS = reader.GetBoolean("STATUS"),
                            PARKTYPE = reader.GetString("PARKTYPE"),
                            NAVEKHATE = reader.GetString("NAVEKHATE"),
                            TRANSACTIONMODE = reader.GetString("TRANSACTIONMODE")
                        });
                    }
                }
            }
            return list;
        }
    }
}

    