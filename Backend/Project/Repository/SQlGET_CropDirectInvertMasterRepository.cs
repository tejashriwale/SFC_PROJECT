using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SQlGET_CropDirectInvertMasterRepository : IGET_CropDirectInvertMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SQlGET_CropDirectInvertMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CropDirectInvertMaster>> getasync(GET_CropDirectInvertMasterDomain input)
        {
            List<GET_CropDirectInvertMaster> list = new List<GET_CropDirectInvertMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20  * FROM [GET_CropDirectInvertMaster]( '" + input.CROPID + "', '" + input.KEYWORD + "'  , '" + input.COMPANYID + "' , '" + input.DEPTID +"') ORDER BY CRDATE DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CropDirectInvertMaster
                        {
                            SHAID = reader.GetString("SHAID"),
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            CAREA = reader.GetString("CAREA"),
                            BILLNO = reader.GetString("BILLNO"),
                            CARET = reader.GetDouble("CARET"),
                            CONTACTNO = reader.GetString("CONTACTNO"),
                            CRDATE = reader.GetString("CRDATE"),
                            CROPNAME = reader.GetString("CROPNAME"),
                            CVARIETY = reader.GetString("CVARIETY"),
                            FAID = reader.GetString("FAID"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            NARR = reader.GetString("NARR"),
                            RATE = reader.GetDouble("RATE"),
                            CROPID = reader.GetString("CROPID"),
                            CTYPE = reader.GetString("CTYPE"),
                            EPRODUCTION = reader.GetString("EPRODUCTION"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_CropDirectInvertMaster>> getkeyword(GET_CropDirectInvertMasterDomain input)
        {
            List<GET_CropDirectInvertMaster> list = new List<GET_CropDirectInvertMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT   * FROM [GET_CropDirectInvertMaster]( '" + input.CROPID + "', '" + input.KEYWORD + "'  , '" + input.COMPANYID + "' , '" + input.DEPTID + "') ORDER BY CRDATE DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CropDirectInvertMaster
                        {
                            SHAID = reader.GetString("SHAID"),
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            CAREA = reader.GetString("CAREA"),
                            BILLNO = reader.GetString("BILLNO"),
                            CARET = reader.GetDouble("CARET"),
                            CONTACTNO = reader.GetString("CONTACTNO"),
                            CRDATE = reader.GetString("CRDATE"),
                            CROPNAME = reader.GetString("CROPNAME"),
                            CVARIETY = reader.GetString("CVARIETY"),
                            FAID = reader.GetString("FAID"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            NARR = reader.GetString("NARR"),
                            RATE = reader.GetDouble("RATE"),
                            CROPID = reader.GetString("CROPID"),
                            CTYPE = reader.GetString("CTYPE"),
                            EPRODUCTION = reader.GetString("EPRODUCTION"),
                            WEIGHT = reader.GetDouble("WEIGHT"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
