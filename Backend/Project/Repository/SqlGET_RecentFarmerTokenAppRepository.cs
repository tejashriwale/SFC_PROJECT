using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_RecentFarmerTokenAppRepository : IGET_RecentFarmerTokenAppRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_RecentFarmerTokenAppRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_RecentFarmerTokenApp>> getasync(GET_RecentFarmerTokenAppDomain input)
        {
            List<GET_RecentFarmerTokenApp> list = new List<GET_RecentFarmerTokenApp>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_RecentFarmerTokenApp](@p0 , @p1 , @p2, @p3 , @p4 , @p5)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.VEHNO) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DATE) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.AADHARNO) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.MOBILENO) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(input.MAID) ?? ""); command.Parameters.Add(command_p4); }
                { var command_p5 = command.CreateParameter(); command_p5.ParameterName = "@p5"; command_p5.Value = (object)(Convert.ToString(input.CROP) ?? ""); command.Parameters.Add(command_p5); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_RecentFarmerTokenApp
                        {
                            CCNT= reader.GetString("CCNT"),
                            TOKEN= reader.GetString("TOKEN"),
                            DPKID = reader.GetString("DPKID"),
                           

                        });
                    }
                }
            }
            return list;
        }
    }
    
}
