using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class Sql_GET_BarcodeSettings_Repository : I_GET_BarcodeSettings_Repository
    {
        private readonly ApplicationDbContext dbContext;

        public Sql_GET_BarcodeSettings_Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<_GET_BarcodeSettings_>> getasync(MasterInput input)
        {
           List<_GET_BarcodeSettings_> list = new List<_GET_BarcodeSettings_>();

            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select * from [GET_BarcodeSettings]('%',@p0,@p1)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await  reader.ReadAsync())
                    {
                        list.Add(new _GET_BarcodeSettings_
                        {
                            BAID = reader.GetString("BAID"),
                            BFONTTYPE = reader.GetString("BFONTTYPE"),
                            
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<_GET_BarcodeSettings_>> GetByGroupAsync(MasterInput input)
        {
            List< _GET_BarcodeSettings_ > list = new List<_GET_BarcodeSettings_> ();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())

            {
                command.CommandText = "SELECT * FROM [dbo].[GET_BarcodeSettings](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }

              

                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_BarcodeSettings_
                        {
                            BAID = reader.GetString("BAID"),
                            BFONTTYPE = reader.GetString("BFONTTYPE"),
                          
                            
                        });
                    }
                }

            }
            return list;
        }
    }
}
