using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_SuggestPhoneNumberRepository : IGET_SuggestPhoneNumberRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SuggestPhoneNumberRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SuggestPhoneNumber>> getasync(GET_SuggestPhoneNumberDomain input)
        {
            List<GET_SuggestPhoneNumber> list = new List<GET_SuggestPhoneNumber>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_SuggestPhoneNumber]('" + input.FCONTACTNO + "' , '" + input.KEYWORD + "' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_SuggestPhoneNumber
                        {
                           FAID = reader.GetString("FAID"),
                           FCONTACTNO = reader.GetString("FCONTACTNO"),
                           FNAME = reader.GetString("FNAME"),
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_SuggestPhoneNumber>> getkeyword(GET_SuggestPhoneNumberDomain input)
        {
            List<GET_SuggestPhoneNumber> list = new List<GET_SuggestPhoneNumber>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_SuggestPhoneNumber]('" + input.FCONTACTNO + "' , '" + input.KEYWORD + "' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_SuggestPhoneNumber
                        {
                            FAID = reader.GetString("FAID"),
                            FCONTACTNO = reader.GetString("FCONTACTNO"),
                            FNAME = reader.GetString("FNAME"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
