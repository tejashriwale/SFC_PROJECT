using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_AdvertisementRepository : IGET_AdvertisementRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_AdvertisementRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_Advertisement>> getasync(GET_AdvertisementDomain input)
        {
            List<GET_Advertisement> list = new List<GET_Advertisement>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_Advertisement](@p0 , @p1 , @p2 , @p3)ORDER BY DATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.ADid) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_Advertisement
                        {
                            ADid = reader.GetString("ADid"),
                            ADimage = reader.GetString("ADimage"),
                            ISACTIVE = reader.GetString("ISACTIVE"),
                            ADtitle = reader.GetString("ADtitle"),
                            LOGTYPE = reader.GetString("LOGTYPE"),
                            DATE = reader.GetString("DATE"),
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_Advertisement>> getkeyword(GET_AdvertisementDomain input)
        {
            List<GET_Advertisement> list = new List<GET_Advertisement>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_Advertisement](@p0 , @p1 , @p2 , @p3)ORDER BY DATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.ADid) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_Advertisement
                        {
                            ADid = reader.GetString("ADid"),
                            ADimage = reader.GetString("ADimage"),
                            ISACTIVE = reader.GetString("ISACTIVE"),
                            ADtitle = reader.GetString("ADtitle"),
                            DATE = reader.GetString("DATE"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
