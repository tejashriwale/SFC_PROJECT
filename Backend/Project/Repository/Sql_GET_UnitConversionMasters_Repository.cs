using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class Sql_GET_UnitConversionMasters_Repository : I_GET_UnitConversionMasters_Repository
    {
        private readonly ApplicationDbContext dbContext;

        public Sql_GET_UnitConversionMasters_Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<_GET_UnitConversionMasters_>> getasync(getByIdInput input)
        {
            List<_GET_UnitConversionMasters_> list = new List<_GET_UnitConversionMasters_> ();

            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select Top 20 * from[GET_UnitConversionMasters]('%' , '%','" + input.COMPANYID + "' , '" + input.DEPTID + "')";

                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_UnitConversionMasters_
                        {
                            UAID = reader.GetString("UAID"),
                            FROMUNITQUANTITY = reader.GetDouble("FROMUNITQUANTITY"),
                            FROMUNITUOM = reader.GetString("FROMUNITUOM"),
                            TOUNITQUANTITY = reader.GetDouble("TOUNITQUANTITY"),
                            TOUNITUOM = reader.GetString("TOUNITUOM"),
                            utype = reader.GetString("utype"),
                            

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<_GET_UnitConversionMasters_>> GetByGroupAsync(getByIdInput input)
        {
            List<_GET_UnitConversionMasters_> list = new List<_GET_UnitConversionMasters_>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())

            {
                command.CommandText = "select Top 20 * from [GET_UnitConversionMasters]('" + input.PKID + "' , '" + input.KEYWORD + "','" + input.COMPANYID + "' , '" + input.DEPTID + "')";

                

                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_UnitConversionMasters_
                        {
                            UAID = reader.GetString("UAID"),
                            FROMUNITQUANTITY = reader.GetDouble("FROMUNITQUANTITY"),
                            FROMUNITUOM = reader.GetString("FROMUNITUOM"),
                            TOUNITQUANTITY = reader.GetDouble("TOUNITQUANTITY"),
                            TOUNITUOM = reader.GetString("TOUNITUOM"),
                            utype = reader.GetString("utype"),
                           
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<_GET_UnitConversionMasters_>> searchasync(getByIdInput input)
        {
            List<_GET_UnitConversionMasters_> list = new List<_GET_UnitConversionMasters_>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())

            {
                command.CommandText = "select  * from [GET_UnitConversionMasters]('" + input.PKID + "' , '" + input.KEYWORD + "','" + input.COMPANYID + "' , '" + input.DEPTID + "')";

                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_UnitConversionMasters_
                        {
                            UAID = reader.GetString("UAID"),
                            FROMUNITQUANTITY = reader.GetDouble("FROMUNITQUANTITY"),
                            FROMUNITUOM = reader.GetString("FROMUNITUOM"),
                            TOUNITQUANTITY = reader.GetDouble("TOUNITQUANTITY"),
                            TOUNITUOM = reader.GetString("TOUNITUOM"),
                            utype = reader.GetString("utype"),
                          
                        });
                    }
                }
            }
            return list;
        }
    }
}
