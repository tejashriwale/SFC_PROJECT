using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_PARKINGRepository : IGET_PARKINGRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PARKINGRepository(ApplicationDbContext dbContext)
        {
           this.dbContext = dbContext;
        }
        public async Task<List<GET_PARKING>> GetAsync(GET_PARKINGDomain input)
        {
            List<GET_PARKING> result = new List<GET_PARKING>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PARKING]('"+input.PAID + "','"+input.KEYWORD + "','" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_PARKING
                        {
                            PAID = reader.GetString("PAID"),
                            AUTOID = reader.GetString("AUTOID"),
                           DATE		= reader.GetString("DATE"),
                           TYPE		= reader.GetString("TYPE"),
                           TOTALJALI	= reader.GetInt32("TOTALJALI"),
                           VNUMBER		= reader.GetString("VNUMBER"),
                           VOWNERNAME	= reader.GetString("VOWNERNAME"),
                           VTYPE		= reader.GetString("VTYPE"),
                           VCHARGE		= reader.GetDouble("VCHARGE"),
                           STATUS		= reader.GetBoolean("STATUS"),
                            PARKTYPE = reader.GetString("PARKTYPE"),
                            NAVEKHATE = reader.GetString("NAVEKHATE"),
                            TRANSACTIONMODE = reader.GetString("TRANSACTIONMODE")
                        });
                    }
                }
            }
            return result;
        }
        public async Task<List<GET_PARKING>> getkeyword(GET_PARKINGDomain input)
        {
            List<GET_PARKING> list = new List<GET_PARKING>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PARKING]('" + input.PAID + "','" + input.KEYWORD + "','" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_PARKING
                        {
                            PAID = reader.GetString("PAID"),
                            AUTOID = reader.GetString("AUTOID"),
                            DATE = reader.GetString("DATE"),
                            TYPE = reader.GetString("TYPE"),
                            TOTALJALI = reader.GetInt32("TOTALJALI"),
                            VNUMBER = reader.GetString("VNUMBER"),
                            VOWNERNAME = reader.GetString("VOWNERNAME"),
                            VTYPE = reader.GetString("VTYPE"),
                            VCHARGE = reader.GetDouble("VCHARGE"),
                            STATUS = reader.GetBoolean("STATUS"),
                            PARKTYPE = reader.GetString("PARKTYPE"),
                            NAVEKHATE = reader.GetString("NAVEKHATE"),
                            TRANSACTIONMODE = reader.GetString("TRANSACTIONMODE")
                        });
                    }
                }
            }
            return list;
        }
    }
}

    