using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_SQuatationDetailsRepository : IGET_SQuatationDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SQuatationDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SQuatationDetails>> getasync(GET_SQuatationDetailsDomain input)
        {
            List<GET_SQuatationDetails> list = new List<GET_SQuatationDetails>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SQuatationDetails]('" + input.SQAMDAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await  reader.ReadAsync())
                    {
                        list.Add(new GET_SQuatationDetails
                        {
                            SQAMAID = reader.GetString("SQAMAID"),
                            SQAMDAID = reader.GetString("SQAMDAID"),
                            SQPRODUCT = reader.GetString("SQPRODUCT"),
                            SQQUANTITY = reader.GetString("SQQUANTITY"),
                            SQUOM = reader.GetString("SQUOM"),
                            SQRATE = reader.GetDouble("SQRATE"),
                            SQTAMT = reader.GetDouble("SQTAMT"),
                            SQIGST = reader.GetDouble("SQIGST"),
                            SQSGST = reader.GetDouble("SQSGST"),
                            SQCGST = reader.GetDouble("SQCGST"),
                            SQCESS = reader.GetDouble("SQCESS"),
                            HSNCODE = reader.GetInt32("HSNCODE"),
                            SQTAXBALEAMOUNT = reader.GetInt32("SQTAXBALEAMOUNT"),
                            IsDeleted = reader.GetBoolean("IsDeleted"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
