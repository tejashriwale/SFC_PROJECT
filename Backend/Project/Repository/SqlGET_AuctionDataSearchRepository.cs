using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_AuctionDataSearchRepository : IGET_AuctionDataSearchRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_AuctionDataSearchRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_AuctionDataSearch>> getasync(GET_AuctionDataSearchDomain input)
        {
            List<GET_AuctionDataSearch> list = new List<GET_AuctionDataSearch> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_AuctionDataSearch](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader  = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_AuctionDataSearch
                        {
                            TOKEN = reader.GetString("TOKEN"),
                            FNAME = reader.GetString("FNAME"),
                            FCONTACTNO = reader.GetString("FCONTACTNO"),
                            FADDHARNO = reader.GetString("FADDHARNO"),
                            APKID = reader.GetString("APKID"),
                            MAID = reader.GetString("MAID"),
                            DATE = reader.GetString("DATE"),
                            VEHNO = reader.GetString("VEHNO"),
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_AuctionDataSearch>> getkeyword(GET_AuctionDataSearchDomain input)
        {
            List<GET_AuctionDataSearch> list = new List<GET_AuctionDataSearch>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_AuctionDataSearch](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_AuctionDataSearch
                        {
                            TOKEN = reader.GetString("TOKEN"),
                            FNAME = reader.GetString("FNAME"),
                            FCONTACTNO = reader.GetString("FCONTACTNO"),
                            FADDHARNO = reader.GetString("FADDHARNO"),
                            APKID = reader.GetString("APKID"),
                            MAID = reader.GetString("MAID"),
                            DATE = reader.GetString("DATE"),
                            VEHNO = reader.GetString("VEHNO"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
