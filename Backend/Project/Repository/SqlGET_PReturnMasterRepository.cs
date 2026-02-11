using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_PReturnMasterRepository : IGET_PReturnMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PReturnMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_PReturnMaster>> getasync(GET_PReturnMasterDomain input)
        {
            List<GET_PReturnMaster> list = new List<GET_PReturnMaster>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_PReturnMaster]('" + input.PRAID + "' , '" + input.RTYPE + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' )";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_PReturnMaster
                        {
                            PBDATE = reader.GetString("PBDATE"),
                            PRAID = reader.GetString("PRAID"),
                            PBNO = reader.GetString("PBNO"),
                            POTRNNO = reader.GetString("POTRNNO"),
                            PSELLER = reader.GetString("PSELLER"),
                            PRCONSIGNER = reader.GetString("PRCONSIGNER"),
                            PBREFERENCE = reader.GetString("PBREFERENCE"),
                            PRNARRATION = reader.GetString("PRNARRATION"),
                            PRDATE = reader.GetString("PRDATE"),
                            PRNAMT = reader.GetDouble("PRNAMT"),
                            PRGAMT = reader.GetDouble("PRGAMT"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            PAYMENTMODE = reader.GetString("PAYMENTMODE"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_PReturnMaster>> getkeyword(GET_PReturnMasterDomain input)
        {
            List<GET_PReturnMaster> list = new List<GET_PReturnMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PReturnMaster]('" + input.PRAID + "' , '" + input.RTYPE + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' )";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_PReturnMaster
                        {
                            PBDATE = reader.GetString("PBDATE"),
                            PRAID = reader.GetString("PRAID"),
                            PBNO = reader.GetString("PBNO"),
                            POTRNNO = reader.GetString("POTRNNO"),
                            PSELLER = reader.GetString("PSELLER"),
                            PRCONSIGNER = reader.GetString("PRCONSIGNER"),
                            PBREFERENCE = reader.GetString("PBREFERENCE"),
                            PRNARRATION = reader.GetString("PRNARRATION"),
                            PRDATE = reader.GetString("PRDATE"),
                            PRNAMT = reader.GetDouble("PRNAMT"),
                            PRGAMT = reader.GetDouble("PRGAMT"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            PAYMENTMODE = reader.GetString("PAYMENTMODE"),

                        });
                    }
                }
            }
            return list;
        }
            
    }
}
