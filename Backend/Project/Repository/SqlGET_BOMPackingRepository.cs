using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_BOMPackingRepository : IGET_BOMPackingRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlGET_BOMPackingRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_BOMPacking>> getasync(GET_BOMPackingDomain input)
        {
           List<GET_BOMPacking> list = new List<GET_BOMPacking> ();

            using(var command = dbContext.Database.GetDbConnection().CreateCommand ())
            {
                command.CommandText = "SELECt Top 20 * FROM [GET_BOMPacking]('%', '"+input.KEYWORD+"' ,'" + input.COMPANYID + "','" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync ())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_BOMPacking
                        {
                            BAID = reader.GetString("BAID"),
                            PAID = reader.GetString("PAID"),
                            PCAID = reader.GetString("PCAID"),
                            BOMPACKING = reader.GetString("BOMPACKING"),
                            BRAWPRODUCTQUANTITY = reader.GetString("BRAWPRODUCTQUANTITY"),
                            BRAWPRODUCTQUANTITYUOM = reader.GetString("BRAWPRODUCTQUANTITYUOM"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),

                        });
                    }
                  
                }
            }
            return list;
        }

        public async Task<List<GET_BOMPacking>> GetByGroupAsync(GET_BOMPackingDomain input)
        {
            List<GET_BOMPacking> list = new List<GET_BOMPacking>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())

            {
                command.CommandText = "SELECT Top 20 * FROM [GET_BOMPacking]('" + input.PKID + "', '"+input.KEYWORD+"' ,'"+ input.COMPANYID +"' , '"+ input.DEPTID +"' )";

              

                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync ())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_BOMPacking
                        {
                            BAID = reader.GetString("BAID"),
                            PAID = reader.GetString("PAID"),
                            PCAID = reader.GetString("PCAID"),
                            BOMPACKING = reader.GetString("BOMPACKING"),
                            BRAWPRODUCTQUANTITY = reader.GetString("BRAWPRODUCTQUANTITY"),
                            BRAWPRODUCTQUANTITYUOM = reader.GetString("BRAWPRODUCTQUANTITYUOM"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),

                        });
                    }
                }

              

            }
            return list;
            
        }

        public async Task<List<GET_BOMPacking>> getkeyword(GET_BOMPackingDomain input)
        {
            List<GET_BOMPacking> list = new List<GET_BOMPacking>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECt * FROM [GET_BOMPacking]('"+input.PKID+"', '"+input.KEYWORD+"' , '" + input.COMPANYID + "','" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_BOMPacking
                        {
                            BAID = reader.GetString("BAID"),
                            PAID = reader.GetString("PAID"),
                            PCAID = reader.GetString("PCAID"),
                            BOMPACKING = reader.GetString("BOMPACKING"),
                            BRAWPRODUCTQUANTITY = reader.GetString("BRAWPRODUCTQUANTITY"),
                            BRAWPRODUCTQUANTITYUOM = reader.GetString("BRAWPRODUCTQUANTITYUOM"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),

                        });
                    }
                }

            }
            return list;
        }
           
    }
}
