using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_DelarRateRepository : IGET_DelarRateRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_DelarRateRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<List<GET_DelarRate>> getasync(GET_DelarRateDomain input)
        {
            List<GET_DelarRate> list = new List<GET_DelarRate>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20  * FROM [GET_DelarRate](@p0 , @p1,@p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.DEAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_DelarRate
                        {
                            DEAID = reader.GetString("DEAID"),
                            DELARNAME = reader.GetString("DELARNAME"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                            RATE = reader.GetDouble("RATE"),
                            DELARRATE = reader.GetDouble("DELARRATE"),
                            SIZE = reader.GetString("SIZE"),
                            BRANDNAME = reader.GetString("BRANDNAME"),
                            BRANDLABEL = reader.GetString("BRANDLABEL"),
                            DEALERLABEL = reader.GetString("DEALERLABEL"),
                            SUBCATEGORY = reader.GetString("SUBCATEGORY")


                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_DelarRate>> getkeyword(GET_DelarRateDomain input)
        {
            List<GET_DelarRate> list = new List<GET_DelarRate>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_DelarRate](@p0 , @p1,@p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.DEAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_DelarRate
                        {
                            DEAID = reader.GetString("DEAID"),
                            DELARNAME = reader.GetString("DELARNAME"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                            RATE = reader.GetDouble("RATE"),
                            DELARRATE = reader.GetDouble("DELARRATE"),
                            SIZE = reader.GetString("SIZE"),
                            BRANDNAME = reader.GetString("BRANDNAME"),
                            BRANDLABEL = reader.GetString("BRANDLABEL"),
                            DEALERLABEL = reader.GetString("DEALERLABEL"),
                            SUBCATEGORY = reader.GetString("SUBCATEGORY")


                        });
                    }
                }
            }
            return list;
        }
    }
}
