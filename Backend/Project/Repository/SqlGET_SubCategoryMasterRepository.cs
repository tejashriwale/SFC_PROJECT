using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_SubCategoryMasterRepository : IGET_SubCategoryMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SubCategoryMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SubCategoryMaster>> getasync(GET_SubCategoryMasterDomain input)
        {
            List<GET_SubCategoryMaster> list = new List<GET_SubCategoryMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_SubCategoryMaster]('" + input.CTAID + "', '"+ input.CTID + "'  , '"+ input.KEYWORD + "'  ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_SubCategoryMaster
                        {
                            CTID = reader.GetString("CTID"),
                            CTAID = reader.GetString("CTAID"),
                            CSTATUS = reader.GetBoolean("CSTATUS"),
                            SUBCATEGORYNAME = reader.GetString("SUBCATEGORYNAME"),
                         

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_SubCategoryMaster>> getkeyword(GET_SubCategoryMasterDomain input)
        {
            List<GET_SubCategoryMaster> list = new List<GET_SubCategoryMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SubCategoryMaster]('" + input.CTAID + "', '" + input.CTID + "'  , '" + input.KEYWORD + "'  ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_SubCategoryMaster
                        {
                            CTID = reader.GetString("CTID"),
                            CTAID = reader.GetString("CTAID"),
                            CSTATUS = reader.GetBoolean("CSTATUS"),
                            SUBCATEGORYNAME = reader.GetString("SUBCATEGORYNAME"),


                        });
                    }
                }
            }
            return list;
        }
    }
}
