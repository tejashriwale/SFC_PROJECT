using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VYAPARISERVICERepository : IGET_VYAPARISERVICERepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VYAPARISERVICERepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_VYAPARISERVICE>> getasync(GET_VYAPARISERVICEDomain input)
        {
            List<GET_VYAPARISERVICE> result = new List<GET_VYAPARISERVICE>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_VYAPARISERVICE]('" + input.SCID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        result.Add(new GET_VYAPARISERVICE
                        {
                            FARMER = reader.GetString("FARMER"),
                            SCID = reader.GetString("SCID"),
                            RATE = reader.GetDouble("RATE"),
                            RATETYPE = reader.GetString("RATETYPE"),
                            SERVICETYPE = reader.GetString("SERVICETYPE"),
                            SEVA = reader.GetString("SEVA"),
                            RATETYPENAME = reader.GetString("RATETYPENAME")

                        });
                    }
                }
            }
            return result;
        }
    }
}
