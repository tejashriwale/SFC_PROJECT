using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_FARMERSERVICERepository : IGET_FARMERSERVICERepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_FARMERSERVICERepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_FARMERSERVICE>> getasync(GET_FARMERSERVICEDomain input)
        {
           List<GET_FARMERSERVICE> list = new List<GET_FARMERSERVICE>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_FARMERSERVICE]('" + input.SHID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_FARMERSERVICE
                        {
                            
                            RATETYPE = reader.GetString("RATETYPE"),
                            PKID = reader.GetString("PKID"),
                            RATEAPPLICABLTITLE = reader.GetString("RATEAPPLICABLTITLE"),
                            SERVICETYPETITLE = reader.GetString("SERVICETYPETITLE"),
                            RATETYPETITLE = reader.GetString("RATETYPETITLE"),
                            SERVICETYPE = reader.GetString("SERVICETYPE"),
                            NEWRATE = reader.GetDouble("NEWRATE"),
                           
                        });
                    }
                }
            }
            return list;
        }
    }
}
