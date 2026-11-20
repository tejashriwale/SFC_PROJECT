using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_AuctionDetailsByIdRepository : IGET_AuctionDetailsByIdRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_AuctionDetailsByIdRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_AuctionDetailsById>> getasync(GET_AuctionDetailsByIdDomain input)
        {
            List<GET_AuctionDetailsById> list = new List<GET_AuctionDetailsById>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_AuctionDetailsById](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.APKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var  reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_AuctionDetailsById
                        {
                            DPKID = reader.GetString("DPKID"),
                            APKID = reader.GetString("APKID"),
                            VYAPARI = reader.GetString("VYAPARI"),
                            JAALI = reader.GetInt32("JAALI"),
                            WEIGHT = reader.GetString("WEIGHT"),
                            WEIGHTLESS = reader.GetDouble("WEIGHTLESS"),
                            RATE = reader.GetDouble("RATE"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            WEIGHTMAXMIN = reader.GetBoolean("WEIGHTMAXMIN"),
                            VILLAGE = reader.GetString("VILLAGE"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            TOKEN = reader.GetString("TOKEN"),
                            MAID = reader.GetString("MAID")
                        });
                    }
                }
            }
            return list;
        }
    }
}
