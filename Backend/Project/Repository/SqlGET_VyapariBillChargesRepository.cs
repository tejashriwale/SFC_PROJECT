using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VyapariBillChargesRepository : IGET_VyapariBillChargesRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariBillChargesRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_VyapariBillCharges>> getsync(GET_VyapariBillChargesDomain input)
        {
            List<GET_VyapariBillCharges> list = new List<GET_VyapariBillCharges>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_VyapariBillCharges]('" + input.VYAPARINAME + "' , '"+input.BILLNO+"' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VyapariBillCharges
                        {
                            SEVA = reader.GetString("SEVA"),
                            SevaID = reader.GetString("SevaID"),
                            SevaValue = reader.GetString("SevaValue"),
                            nSERVICETYPETITLE = reader.GetString("nSERVICETYPETITLE"),
                          

                        });
                    }
                }
            }
            return list;
        }
    }
}
