using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CategoryMasterCaseRepository : IGET_CategoryMasterCaseRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CategoryMasterCaseRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
     
        public async Task<List<GET_CategoryMasterCase>> getasync(GET_CategoryMasterCaseDomain input)
        {
            List<GET_CategoryMasterCase> list = new List<GET_CategoryMasterCase>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CategoryMasterCase]('" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CategoryMasterCase
                        {
                            CTID = reader.GetString("CTID"),
                            CTNAME = reader.GetString("CTNAME"),
                            CTTYPE = reader.GetString("CTTYPE"),
                            CTSUBCATGORYID = reader.GetString("CTSUBCATGORYID"),
                            COMPANYID = reader.GetString("COMPANYID"),
                            DEPTID = reader.GetString("DEPTID"),
                            CATEGORY = reader.GetString("CATEGORY"),
                            SUBCATEGORY = reader.GetString("SUBCATEGORY"),
                            CSTATUS = reader.GetBoolean("CSTATUS"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            
                        });
                    }
                }
            }
            return list;
        }
    }
}
