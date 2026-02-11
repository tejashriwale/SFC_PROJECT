using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_GoodReceiptNoteAssuranceByIdRepository : IGET_GoodReceiptNoteAssuranceByIdRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_GoodReceiptNoteAssuranceByIdRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_GoodReceiptNoteAssuranceById>> getasync(GET_GoodReceiptNoteAssuranceByIdDomain input)
        {
            List<GET_GoodReceiptNoteAssuranceById> list = new List<GET_GoodReceiptNoteAssuranceById>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_GoodReceiptNoteAssuranceById]('" + input.GRNAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_GoodReceiptNoteAssuranceById
                        {
                            GAAID = reader.GetString("GAAID"),
                            QCID = reader.GetString("QCID"),
                            GRNAID = reader.GetString("GRNAID"),
                            GACONDITION = reader.GetBoolean("GACONDITION"),
                            GAAMT = reader.GetBoolean("GAAMT"),
                            GAEXPECTED = reader.GetBoolean("GAEXPECTED"),
                            GAPACKAGE = reader.GetBoolean("GAPACKAGE"),
                            GACONDITIONDESCRIPTION = reader.GetString("GACONDITIONDESCRIPTION"),
                            GAAMTDESCRIPTION = reader.GetString("GAAMTDESCRIPTION"),
                            GAEXPECTEDDESCRIPTION = reader.GetString("GAEXPECTEDDESCRIPTION"),
                            GAPACKAGEDESCRIPTION = reader.GetString("GAPACKAGEDESCRIPTION"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
