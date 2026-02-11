using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_VisitorMasterApprovalRepository : IGET_VisitorMasterApprovalRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VisitorMasterApprovalRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_VisitorMasterApproval>> getasync(GET_VisitorMasterApprovalDomain input)
        {
            List<GET_VisitorMasterApproval> list = new List<GET_VisitorMasterApproval>();
            using (var commmand = dbContext.Database.GetDbConnection().CreateCommand())
            {
                commmand.CommandText = "SELECT Top 20  * FROM [GET_VisitorMasterApproval]( '" + input.VAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY ODATE DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await commmand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VisitorMasterApproval
                        {
                            VISITINGDATE = reader.GetString("VISITINGDATE"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            FNAME = reader.GetString("FNAME"),
                            PHONENUMBER = reader.GetString("PHONENUMBER"),
                            LOCATION = reader.GetString("LOCATION"),
                            VAID = reader.GetString("VAID"),
                            ODATE = reader.GetDateTime("ODATE")
                        });
                    }
                }
                return list;
            }
        }

        public async Task<List<GET_VisitorMasterApproval>> getkeyword(GET_VisitorMasterApprovalDomain input)
        {
            List<GET_VisitorMasterApproval> list = new List<GET_VisitorMasterApproval>();
            using (var commmand = dbContext.Database.GetDbConnection().CreateCommand())
            {
                commmand.CommandText = "SELECT  * FROM [GET_VisitorMasterApproval]( '" + input.VAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY ODATE DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await commmand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VisitorMasterApproval
                        {
                            VISITINGDATE = reader.GetString("VISITINGDATE"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            FNAME = reader.GetString("FNAME"),
                            PHONENUMBER = reader.GetString("PHONENUMBER"),
                            LOCATION = reader.GetString("LOCATION"),
                            VAID = reader.GetString("VAID"),
                            ODATE = reader.GetDateTime("ODATE")

                        });
                    }
                }
                return list;
            }
        }
    }
}
