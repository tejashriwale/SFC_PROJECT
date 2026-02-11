using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VyapariSanctionRepository : IGET_VyapariSanctionRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariSanctionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_VyapariSanction>> getasync(GET_VyapariSanctionDomain input)
        {
            List<GET_VyapariSanction> result = new List<GET_VyapariSanction>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_VyapariSanction]('" + input.VSAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();    

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        result.Add(new GET_VyapariSanction
                        {
                            VSAID = reader.GetString("VSAID"),
                            VNAME = reader.GetString("VNAME"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),

                            AMOUNT = reader.GetDouble("AMOUNT"),
                           
                        });
                    }
                }
            }
            return result;
        }

        public async Task<List<GET_VyapariSanction>> getkeyword(GET_VyapariSanctionDomain input)
        {
            List<GET_VyapariSanction> result = new List<GET_VyapariSanction>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_VyapariSanction]('" + input.VSAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_VyapariSanction
                        {
                            VSAID = reader.GetString("VSAID"),
                            VNAME = reader.GetString("VNAME"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                          
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            
                        });
                    }
                }
            }
            return result;
        }
    }
}
