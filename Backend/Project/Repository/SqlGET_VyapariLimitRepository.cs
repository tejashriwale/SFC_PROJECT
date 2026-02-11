using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VyapariLimitRepository : IGET_VyapariLimitRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariLimitRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_VyapariLimit>> getasync(GET_VyapariLimitDomain input)
        {
            List<GET_VyapariLimit> result = new List<GET_VyapariLimit>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT TOP 20 * FROM [GET_VyapariLimit]('" + input.VLEID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await  command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        result.Add(new GET_VyapariLimit
                        {
                            VLEID = reader.GetString("VLEID"),
                            SHILLAKRAKKAM = reader.GetString("SHILLAKRAKKAM"),
                            ADHICHIMARYADA = reader.GetString("ADHICHIMARYADA"),
                            RAKKAMVADHAVANE = reader.GetString("RAKKAMVADHAVANE"),
                            VAYPARI = reader.GetString("VAYPARI"),
                            STATUS = reader.GetBoolean("STATUS"),
                            VNAME = reader.GetString("VNAME"),
                            VAPNAME = reader.GetString("VAPNAME"),
                        });
                    }
                }
            }
            return result;
        }

        public async Task<List<GET_VyapariLimit>> getkeyword(GET_VyapariLimitDomain input)
        {
            List<GET_VyapariLimit> result = new List<GET_VyapariLimit>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_VyapariLimit]('" + input.VLEID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_VyapariLimit
                        {
                            VLEID = reader.GetString("VLEID"),
                            SHILLAKRAKKAM = reader.GetString("SHILLAKRAKKAM"),
                            ADHICHIMARYADA = reader.GetString("ADHICHIMARYADA"),
                            RAKKAMVADHAVANE = reader.GetString("RAKKAMVADHAVANE"),
                            VAYPARI = reader.GetString("VAYPARI"),
                            STATUS = reader.GetBoolean("STATUS"),
                            VNAME = reader.GetString("VNAME"),
                            VAPNAME = reader.GetString("VAPNAME"),
                        });
                    }
                }
            }
            return result;
        }
    }
}
