using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_SChemicalNameRepository : IGET_SChemicalNameRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SChemicalNameRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SChemicalName>> getasync(GET_SChemicalNameDomain input)
        {
            List<GET_SChemicalName> list = new List<GET_SChemicalName>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SChemicalName]('" + input.SRAID + "' , '" + input.DSCID + "' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_SChemicalName
                        {
                            SRAID = reader.GetString("SRAID"),
                            DSCID = reader.GetString("DSCID"),
                            COAST = reader.GetDouble("COAST"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            CNAME = reader.GetString("CNAME"),
                            CTYPE = reader.GetString("CTYPE"),
                            CHEMICALNAME = reader.GetString("CHEMICALNAME"),
                            QUANTITY = reader.GetDouble("QUANTITY"),
                            


                        });
                    }
                }
            }
            return list;
        }
    }
}
