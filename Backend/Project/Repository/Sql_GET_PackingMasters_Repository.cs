using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class Sql_GET_PackingMasters_Repository : I_GET_PackingMasters_Repository
    {
        private readonly ApplicationDbContext dbContext;    

        public Sql_GET_PackingMasters_Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<_GET_PackingMasters_>> getasync(PackingDoomain    input)
        {
          List<_GET_PackingMasters_> list = new List<_GET_PackingMasters_>();

            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = " select * from [GET_PackingMasters]('%','" + input.COMPANYID + "','" + input.DEPTID + "')";

                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new _GET_PackingMasters_
                        {
                            PCAID = reader.GetString("PCAID"),
                            PAID = reader.GetString("PAID"),
                            PCTITLE = reader.GetString("PCTITLE"),
                            PCSERIAL = reader.GetString("PCSERIAL"),
                            PCQUANTITYUOM = reader.GetString("PCQUANTITYUOM"),
                            PCQUANTITYAMOUNT = (float)reader.GetDouble("PCQUANTITYAMOUNT"),
                           
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<_GET_PackingMasters_>> GetByGroupAsync(PackingDoomain input)
        {
            List<_GET_PackingMasters_> list = new List<_GET_PackingMasters_>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())

            {
                command.CommandText = "SELECT * FROM [dbo].[GET_PackingMasters]('" + input.PCAID + "' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')";

              
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_PackingMasters_
                        {
                            PCAID = reader.GetString("PCAID"),
                            PAID = reader.GetString("PAID"),
                            PCTITLE = reader.GetString("PCTITLE"),
                            PCSERIAL = reader.GetString("PCSERIAL"),
                            PCQUANTITYUOM = reader.GetString("PCQUANTITYUOM"),
                            PCQUANTITYAMOUNT = (float)reader.GetDouble("PCQUANTITYAMOUNT"),
                           
                        });
                    }
                }
            }
            return list;
        }
    }
}
