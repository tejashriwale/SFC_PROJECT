using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_ComplaintRepository : IGET_ComplaintRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ComplaintRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_Complaint>> getasync(GET_ComplaintDomain input)
        {
            List<GET_Complaint> list = new List<GET_Complaint>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_Complaint]('" + input.PKID + "' ,  '" + input.KEYWORD + "'   ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY date DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_Complaint
                        {
                            PKID = reader.GetString("PKID"),
                            NAME = reader.GetString("NAME"),
                            DATE = reader.GetString("DATE"),
                            UAID = reader.GetString("UAID"),
                            TITLE = reader.GetString("TITLE"),
                            DESCRIPTION = reader.GetString("DESCRIPTION"),
                            STATUS = reader.GetString("STATUS"),
                            CONTACT = reader.GetString("CONTACT"),
                            UFORENAME = reader.GetString("UFORENAME"),
                            UMOBILENUMBER = reader.GetString("UMOBILENUMBER"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_Complaint>> getkeyword(GET_ComplaintDomain input)
        {
            List<GET_Complaint> list = new List<GET_Complaint>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_Complaint]('" + input.PKID + "' ,  '" + input.KEYWORD + "'   ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY date DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_Complaint
                        {
                            PKID = reader.GetString("PKID"),
                            NAME = reader.GetString("NAME"),
                            DATE = reader.GetString("DATE"),
                            UAID = reader.GetString("UAID"),
                            TITLE = reader.GetString("TITLE"),
                            DESCRIPTION = reader.GetString("DESCRIPTION"),
                            STATUS = reader.GetString("STATUS"),
                            CONTACT = reader.GetString("CONTACT"),
                            UFORENAME = reader.GetString("UFORENAME"),
                            UMOBILENUMBER = reader.GetString("UMOBILENUMBER"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
