using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_TOKENNoSearchRepository : IGET_TOKENNoSearchRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_TOKENNoSearchRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_TOKENNoSearch>> getasync(GET_TOKENNoSearchDomain input)
        {
            List<GET_TOKENNoSearch> list = new List<GET_TOKENNoSearch>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_TOKENNoSearch]('" + input.TOKNNO + "'  , '" + input.COMPANYID + "' , '" + input.DEPTID + "' , '" + Convert.ToDateTime(input.DATE).ToString("yyyy-MM-dd") + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_TOKENNoSearch
                        {
                            DPKID = reader.GetString("DPKID"),
                            FULLNAME = reader.GetString("FULLNAME"),
                            VILLAGE = reader.GetString("VILLAGE"),
                            MAID = reader.GetString("MAID"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            CARETS_COUNT = reader.GetString("CARETS_COUNT"),
                            ISCOMPLITED = reader.GetBoolean("ISCOMPLITED"),
                        });
                    }
                }
            }
            return list;

        }
    }
}
