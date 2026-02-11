using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VyapariBillMasterDataRepository : IGET_VyapariBillMasterDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariBillMasterDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_VyapariBillMasterData>> getasync(GET_VyapariBillMasterDataDomain input)
        {
            List<GET_VyapariBillMasterData> list = new List<GET_VyapariBillMasterData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_VyapariBillMasterData]('" + input.VYAPARINAME + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "', '"+input.KEYWORD+"' , '"+input.DATE + "')order by VNAME,VBILLNO";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VyapariBillMasterData
                        {
                            TOTAL = reader.GetDouble("TOTAL"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            VBILLNO = reader.GetInt32("VBILLNO"),
                            DATE = reader.GetString("DATE"),
                            VNAME = reader.GetString("VNAME"),
                         


                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_VyapariBillMasterData>> getkeyword(GET_VyapariBillMasterDataDomain input)
        {
            List<GET_VyapariBillMasterData> list = new List<GET_VyapariBillMasterData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_VyapariBillMasterData]('" + input.VYAPARINAME + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' , '"+input.KEYWORD+"' , '"+input.DATE+ "')order by VNAME,VBILLNO";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VyapariBillMasterData
                        {

                            TOTAL = reader.GetDouble("TOTAL"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            VBILLNO = reader.GetInt32("VBILLNO"),
                            DATE = reader.GetString("DATE"),
                            VNAME = reader.GetString("VNAME"),
                           

                        });
                    }
                }
            }
            return list;
        }
    }
}
