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
                command.CommandText = " select * from [GET_PackingMasters]('%',@p0,@p1)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }

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
                command.CommandText = "SELECT * FROM [dbo].[GET_PackingMasters](@p0 ,@p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PCAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }

              
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
