using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_Report_AuctionRepository : IGET_Report_AuctionRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_Report_AuctionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_Report_Auction>> getasync(GET_Report_AuctionDomain input)
        {
            List<GET_Report_Auction> result = new List<GET_Report_Auction>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_Report_Auction]('" + input.STARTDATE + "' , '" + input.ENDTDATE + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_Report_Auction
                        {

                            VEHNO = reader.GetString("VEHNO"),
                            TOKNNO = reader.GetString("TOKNNO"),
                            DATE = reader.GetString("DATE"),
                            AADHARNO = reader.GetString("AADHARNO"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            MOBILENO = reader.GetString("MOBILENO"),
                            VILLAGE = reader.GetString("VILLAGE"),
                            CARETS_COUNT = reader.GetString("CARETS_COUNT"),
                            ISCOMPLETED = reader.GetBoolean("ISCOMPLETED"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            CROPLABEL = reader.GetString("CROPLABEL"),
                           
                           

                        });
                    }
                }

            }
            return result;
        }
    }
}
