using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_SReturnMasterRepository : IGET_SReturnMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SReturnMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SReturnMaster>> getasync(GET_SReturnMasterDomain input)
        {
            List<GET_SReturnMaster> list = new List<GET_SReturnMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_SReturnMaster]('" + input.KEYWORD + "', '"+input.SRAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                { 
                    while(await reader .ReadAsync())
                    {
                        list.Add(new GET_SReturnMaster
                        {
                            SRAID = reader.GetString("SRAID"),
                            SBNO = reader.GetString("SBNO"),
                            SBDATE = reader.GetString("SBDATE"),
                            SOTRNNO = reader.GetString("SOTRNNO"),
                            SSELLER = reader.GetString("SSELLER"),
                            SRCONSIGNER = reader.GetString("SRCONSIGNER"),
                            SBREFEWENCE = reader.GetString("SBREFEWENCE"),
                            SRNARRATION = reader.GetString("SRNARRATION"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            TRANSPORTERNAME = reader.GetString("TRANSPORTERNAME"),
                            SRDATE = reader.GetString("SRDATE"),
                            SRNAMT = reader.GetDouble("SRNAMT"),
                            SRGAMT = reader.GetDouble("SRGAMT"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_SReturnMaster>> getkeyword(GET_SReturnMasterDomain input)
        {
            List<GET_SReturnMaster> list = new List<GET_SReturnMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SReturnMaster]('" + input.KEYWORD + "', '" + input.SRAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_SReturnMaster
                        {
                            SRAID = reader.GetString("SRAID"),
                            SBNO = reader.GetString("SBNO"),
                            SBDATE = reader.GetString("SBDATE"),
                            SOTRNNO = reader.GetString("SOTRNNO"),
                            SSELLER = reader.GetString("SSELLER"),
                            SRCONSIGNER = reader.GetString("SRCONSIGNER"),
                            SBREFEWENCE = reader.GetString("SBREFEWENCE"),
                            SRNARRATION = reader.GetString("SRNARRATION"),
                            SRDATE = reader.GetString("SRDATE"),
                            SRNAMT = reader.GetDouble("SRNAMT"),
                            SRGAMT = reader.GetDouble("SRGAMT"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            TRANSPORTERNAME = reader.GetString("TRANSPORTERNAME"),

                        });
                    }
                }
            }
            return list;
        }
            
    }
}
