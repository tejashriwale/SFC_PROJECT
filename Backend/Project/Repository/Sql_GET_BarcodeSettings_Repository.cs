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
                command.CommandText = "select * from [GET_BarcodeSettings]('%','" + input.COMPANYID + "','" + input.DEPTID + "')";
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
                command.CommandText = "SELECT * FROM [dbo].[GET_BarcodeSettings]('" + input.PKID + "' , '" +input.COMPANYID +"' , '" +input.DEPTID+ "')";

              

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
