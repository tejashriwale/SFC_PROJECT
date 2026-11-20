using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_TopFarmerOnCratesRepository : IGET_TopFarmerOnCratesRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_TopFarmerOnCratesRepository(ApplicationDbContext dbContext)
        {
          this.dbContext = dbContext;
        }
        public async Task<List<GET_TopFarmerOnCrates>> getasync(GET_TopFarmerOnCratesDomain input)
        {
            List<GET_TopFarmerOnCrates> result = new List<GET_TopFarmerOnCrates>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20  * FROM [GET_TopFarmerOnCrates](@p0 , @p1 , @p2 , @p3, @p4, @p5)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.DPKID) ?? ""); command.Parameters.Add(command_p0); }
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
                        result.Add(new GET_TopFarmerOnCrates
                        {
                            DPKID = reader.GetString("DPKID"),
                            MAID = reader.GetString("MAID"),
                            FULLNAME = reader.GetString("FULLNAME"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            CARETS_COUNT = reader.GetString("CARETS_COUNT"),
                            UID = reader.GetString("UID"),
                            ISCOMPLETED = reader.GetBoolean("ISCOMPLETED"),
                        });
                    }
                }
            }
            return result;
        }

        public async Task<List<GET_TopFarmerOnCrates>> getkeyword(GET_TopFarmerOnCratesDomain input)
        {
            List<GET_TopFarmerOnCrates> result = new List<GET_TopFarmerOnCrates>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_TopFarmerOnCrates](@p0 , @p1 , @p2 , @p3, @p4, @p5)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.DPKID) ?? ""); command.Parameters.Add(command_p0); }
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
                        result.Add(new GET_TopFarmerOnCrates
                        {
                            DPKID = reader.GetString("DPKID"),
                            MAID = reader.GetString("MAID"),
                            FULLNAME = reader.GetString("FULLNAME"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            CARETS_COUNT = reader.GetString("CARETS_COUNT"),
                            UID = reader.GetString("UID"),
                            ISCOMPLETED = reader.GetBoolean("ISCOMPLETED"),
                        });
                    }
                }
            }
            return result;
        }
    }
}
