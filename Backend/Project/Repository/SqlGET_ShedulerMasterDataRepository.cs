using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_ShedulerMasterDataRepository : IGET_ShedulerMasterDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ShedulerMasterDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ;
        }
        public async Task<List<GET_ShedulerMasterData>> getasync(GET_ShedulerMasterDataDomain input)
        {
            List<GET_ShedulerMasterData> list = new List<GET_ShedulerMasterData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_ShedulerMasterData] ('" + input.VAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' ,  '" + input.KEYWORD + "' , '" + input.DATE + "')ORDER BY ISCOMPLITED , ODATE DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_ShedulerMasterData
                        {
                           AREA = reader.GetString("AREA"),
                           CONTACTNO = reader.GetString("CONTACTNO"),
                           CROPNAME = reader.GetString("CROPNAME"),
                           CTYPE = reader.GetString("CTYPE"),
                           EHDATE = reader.GetString("EHDATE"),
                           FAID = reader.GetString("FAID"),
                           FARMERNAME = reader.GetString("FARMERNAME"),
                           RATE = reader.GetDouble("RATE"),
                           VAID = reader.GetString("VAID"),
                           VDAID = reader.GetString("VDAID"),
                           EPRODUCTION = reader.GetString("EPRODUCTION"),
                           CVARIETY = reader.GetString("CVARIETY"),
                           ISCOMPLITED = reader.GetString("ISCOMPLITED"),
                            SHAID = reader.GetString("SHAID"),
                            CROPID = reader.GetString("CROPID"),
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_ShedulerMasterData>> getkeyword(GET_ShedulerMasterDataDomain input)
        {
            List<GET_ShedulerMasterData> list = new List<GET_ShedulerMasterData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_ShedulerMasterData] ( '" + input.VAID + "' , '" + input.COMPANYID + "', '" + input.DEPTID + "' ,  '" + input.KEYWORD + "' , '" + input.DATE + "')ORDER BY ISCOMPLITED , ODATE DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_ShedulerMasterData
                        {
                            AREA = reader.GetString("AREA"),
                            CONTACTNO = reader.GetString("CONTACTNO"),
                            CROPNAME = reader.GetString("CROPNAME"),
                            CTYPE = reader.GetString("CTYPE"),
                            EHDATE = reader.GetString("EHDATE"),
                            FAID = reader.GetString("FAID"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            RATE = reader.GetDouble("RATE"),
                            VAID = reader.GetString("VAID"),
                            VDAID = reader.GetString("VDAID"),
                            EPRODUCTION = reader.GetString("EPRODUCTION"),
                            CVARIETY = reader.GetString("CVARIETY"),
                            ISCOMPLITED = reader.GetString("ISCOMPLITED"),
                            SHAID = reader.GetString("SHAID"),
                            CROPID = reader.GetString("CROPID"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
