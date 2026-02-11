using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_VisitorMasterRepository : IGET_VisitorMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VisitorMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ;
        }
        public async Task<List<GET_VisitorMaster>> getasync(GET_VisitorMasterDomain input)
        {
            List<GET_VisitorMaster> list = new List<GET_VisitorMaster>();
            using (var commmand = dbContext.Database.GetDbConnection().CreateCommand())
            {
                commmand.CommandText = "SELECT Top 20 * FROM [GET_VisitorMaster]('" + input.VAID + "','" + input.KEYWORD + "','" + input.COMPANYID + "','" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await commmand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VisitorMaster
                        {
                            VAID = reader.GetString("VAID"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            VISITINGDATE = reader.GetString("VISITINGDATE"),
                            PHONENUMBER = reader.GetString("PHONENUMBER"),
                            LOCATION = reader.GetString("LOCATION"),
                            UAID = reader.GetString("UAID"),
                            COMPANYID = reader.GetString("COMPANYID"),
                            DEPTID = reader.GetString("DEPTID"),
                            FNAME = reader.GetString("FNAME"),
                            ISCOMPLITED = reader.GetString("ISCOMPLITED"),
                            ADVAMT = reader.GetDouble("ADVAMT")






                        });
                    }
                }
                return list;
            }
        }

        public async Task<List<GET_VisitorMaster>> getkeyword(GET_VisitorMasterDomain input)
        {
            List<GET_VisitorMaster> list = new List<GET_VisitorMaster>();
            using (var commmand = dbContext.Database.GetDbConnection().CreateCommand())
            {
                commmand.CommandText = "SELECT Top 20 * FROM [GET_VisitorMaster]('" + input.VAID + "','" + input.KEYWORD + "','" + input.COMPANYID + "','" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await commmand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VisitorMaster
                        {
                            VAID = reader.GetString("VAID"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            VISITINGDATE = reader.GetString("VISITINGDATE"),
                            PHONENUMBER = reader.GetString("PHONENUMBER"),
                            LOCATION = reader.GetString("LOCATION"),
                            UAID = reader.GetString("UAID"),
                            COMPANYID = reader.GetString("COMPANYID"),
                            DEPTID = reader.GetString("DEPTID"),
                            FNAME = reader.GetString("FNAME"),
                            ISCOMPLITED = reader.GetString("ISCOMPLITED"),
                            ADVAMT = reader.GetDouble("ADVAMT")


                        });
                    }
                }
                return list;
            }
        }
    }
}
