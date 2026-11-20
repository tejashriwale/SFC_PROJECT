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
                command.CommandText = "SELECt Top 20 * FROM [GET_BOMPacking]('%', @p0 ,@p1,@p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
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
                command.CommandText = "SELECT Top 20 * FROM [GET_BOMPacking](@p0, @p1 ,@p2 , @p3 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }

              

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
                command.CommandText = "SELECt * FROM [GET_BOMPacking](@p0, @p1 , @p2,@p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
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
