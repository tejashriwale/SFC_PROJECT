using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_ColdStorageRepository : IGET_ColdStorageInwardRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ColdStorageRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_ColdStorageInward>> getasync(GET_ColdStorageInwardDomain input)
        {
            List<GET_ColdStorageInward> result = new List<GET_ColdStorageInward>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_ColdStorageInward]('" + input.CSID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_ColdStorageInward
                        {
                            CSID = reader.GetString("CSID"),
                            COLDSTORAGEID = reader.GetString("COLDSTORAGEID"),
                            LOTNO = reader.GetString("LOTNO"),
                            DATE = reader.GetString("DATE"),
                            CUSTOMERNAME = reader.GetString("CUSTOMERNAME"),
                            THIRDPARTYNAME = reader.GetString("THIRDPARTYNAME"),
                            ITEM = reader.GetString("ITEM"),
                          
                            TYPEOFVARITY = reader.GetString("TYPEOFVARITY"),
                            RACKPOSITION = reader.GetString("RACKPOSITION"),
                            CNAME = reader.GetString("CNAME"),
                            PRODUCT = reader.GetString("PRODUCT"),
                            VAJAN = reader.GetString("VAJAN"),
                            GROSSWEIGHT = reader.GetDouble("GROSSWEIGHT"),
                            NETWEIGHT = reader.GetDouble("NETWEIGHT"),
                            WEIGHTUNIT = reader.GetString("WEIGHTUNIT"),
                            WEIGHTINKG = reader.GetDouble("WEIGHTINKG"),
                            CARETWEIGHT = reader.GetDouble("CARETWEIGHT"),
                            
                        });
                    }
                }
            }
            return result;
        }

        public async Task<List<GET_ColdStorageInward>> getkeyword(GET_ColdStorageInwardDomain input)
        {
            List<GET_ColdStorageInward> result = new List<GET_ColdStorageInward>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_ColdStorageInward]('" + input.CSID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_ColdStorageInward
                        {
                            CSID = reader.GetString("CSID"),
                            COLDSTORAGEID = reader.GetString("COLDSTORAGEID"),
                            LOTNO = reader.GetString("LOTNO"),
                            DATE = reader.GetString("DATE"),
                            CUSTOMERNAME = reader.GetString("CUSTOMERNAME"),
                            THIRDPARTYNAME = reader.GetString("THIRDPARTYNAME"),
                            ITEM = reader.GetString("ITEM"),
                        
                            TYPEOFVARITY = reader.GetString("TYPEOFVARITY"),
                            RACKPOSITION = reader.GetString("RACKPOSITION"),
                            CNAME = reader.GetString("CNAME"),
                            PRODUCT = reader.GetString("PRODUCT"),
                            VAJAN = reader.GetString("VAJAN"),
                            GROSSWEIGHT = reader.GetDouble("GROSSWEIGHT"),
                            NETWEIGHT = reader.GetDouble("NETWEIGHT"),
                            WEIGHTUNIT = reader.GetString("WEIGHTUNIT"),
                            WEIGHTINKG = reader.GetDouble("WEIGHTINKG"),
                            CARETWEIGHT = reader.GetDouble("CARETWEIGHT"),

                        });
                    }
                }
            }
            return result;
        }
    }
}
