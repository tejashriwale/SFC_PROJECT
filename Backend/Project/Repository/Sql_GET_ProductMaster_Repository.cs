using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class Sql_GET_ProductMaster_Repository : I_GET_ProductMaster_Repository
    {
        private readonly ApplicationDbContext dbContext;

        public Sql_GET_ProductMaster_Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<_GET_ProductMaster_>> getasync(productmasterDomain input)
        {
            List<_GET_ProductMaster_> list = new List<_GET_ProductMaster_>();

            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select Top 20 * from[GET_ProductMaster]('%' , '%',@p0 , @p1)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new _GET_ProductMaster_
                        {
                            PAID = reader.GetString("PAID"),
                            PCODE = reader.GetString("PCODE"),
                            PNAME = reader.GetString("PNAME"),
                            PTYPE = reader.GetString("PTYPE"),
                            PCATEGORY = reader.GetString("PCATEGORY"),
                            PSUBCATEGORY = reader.GetString("PSUBCATEGORY"),
                            PDESCRIPTION = reader.GetString("PDESCRIPTION"),
                            PSALES = reader.GetString("PSALES"),
                            PTYPESOFSALES = reader.GetString("PTYPESOFSALES"),
                            PPURCHASEUOM = reader.GetString("PPURCHASEUOM"),
                            PHSN = reader.GetString("PHSN"),
                            PREORDERLEVEL = reader.GetString("PREORDERLEVEL"),
                            PFROMOFPRODUCT = reader.GetString("PFROMOFPRODUCT"),
                            PRETURNPOLICY = reader.GetString("PRETURNPOLICY"),
                            PPRODUCTUSE = reader.GetString("PPRODUCTUSE"),
                            PIMAGES= reader.GetString("PIMAGES"),
                            PIGST = reader.GetDouble("PIGST"),
                            PSGST = reader.GetDouble("PSGST"),
                            PCESS = reader.GetDouble("PCESS"),
                            CTNAME = reader.GetString("CTNAME"),
                            SCTNAME = reader.GetString("SCTNAME"),
                            PRODUCTID     = reader.GetString("PRODUCTID"),
                            PRODUCTNAME   = reader.GetString("PRODUCTNAME"),
                            BRANDNAME     = reader.GetString("BRANDNAME"),
                            RETAILP       = reader.GetDouble("RETAILP"),
                            DEALERP       = reader.GetDouble("DEALERP"),
                            RATE          = reader.GetString("RATE"),
                            RANGE         = reader.GetString("RANGE"),
                            DISCOUNTTYPE  = reader.GetString("DISCOUNTTYPE"),
                            SUBCATEGORY   = reader.GetString("SUBCATEGORY"),
                            CATEGORY      = reader.GetString("CATEGORY"),
                            SALETYPE      = reader.GetString("SALETYPE"),
                            PRODUCTTYPE = reader.GetString("PRODUCTTYPE"),


                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<_GET_ProductMaster_>> GetByGroupAsync(productmasterDomain input )
        {
            List<_GET_ProductMaster_> list = new List<_GET_ProductMaster_>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())

            {
                command.CommandText = "select Top 20 *from[GET_ProductMaster](@p0 , @p1,@p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }

              
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_ProductMaster_
                        {
                            PAID = reader.GetString("PAID"),
                            PCODE = reader.GetString("PCODE"),
                            PNAME = reader.GetString("PNAME"),
                            PTYPE = reader.GetString("PTYPE"),
                            PCATEGORY = reader.GetString("PCATEGORY"),
                            PSUBCATEGORY = reader.GetString("PSUBCATEGORY"),
                            PDESCRIPTION = reader.GetString("PDESCRIPTION"),
                            PSALES = reader.GetString("PSALES"),
                            PTYPESOFSALES = reader.GetString("PTYPESOFSALES"),
                            PPURCHASEUOM = reader.GetString("PPURCHASEUOM"),
                            PHSN = reader.GetString("PHSN"),
                            PREORDERLEVEL = reader.GetString("PREORDERLEVEL"),
                            PFROMOFPRODUCT = reader.GetString("PFROMOFPRODUCT"),
                            PRETURNPOLICY = reader.GetString("PRETURNPOLICY"),
                            PPRODUCTUSE = reader.GetString("PPRODUCTUSE"),
                            PIMAGES = reader.GetString("PIMAGES"),
                            PIGST = reader.GetDouble("PIGST"),
                            PSGST = reader.GetDouble("PSGST"),
                            PCESS = reader.GetDouble("PCESS"),
                            CTNAME = reader.GetString("CTNAME"),
                            SCTNAME = reader.GetString("SCTNAME"),
                            PRODUCTID = reader.GetString("PRODUCTID"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                            BRANDNAME = reader.GetString("BRANDNAME"),
                            RETAILP = reader.GetDouble("RETAILP"),
                            DEALERP = reader.GetDouble("DEALERP"),
                            RATE = reader.GetString("RATE"),
                            RANGE = reader.GetString("RANGE"),
                            DISCOUNTTYPE = reader.GetString("DISCOUNTTYPE"),
                            SUBCATEGORY = reader.GetString("SUBCATEGORY"),
                            CATEGORY = reader.GetString("CATEGORY"),
                            SALETYPE = reader.GetString("SALETYPE"),
                            PRODUCTTYPE = reader.GetString("PRODUCTTYPE"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<_GET_ProductMaster_>> searchasync(productmasterDomain input)
        {
            List<_GET_ProductMaster_> list = new List<_GET_ProductMaster_>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select  * from[GET_ProductMaster](@p0 , @p1,@p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_ProductMaster_
                        {
                            PAID = reader.GetString("PAID"),
                            PCODE = reader.GetString("PCODE"),
                            PNAME = reader.GetString("PNAME"),
                            PTYPE = reader.GetString("PTYPE"),
                            PCATEGORY = reader.GetString("PCATEGORY"),
                            PSUBCATEGORY = reader.GetString("PSUBCATEGORY"),
                            PDESCRIPTION = reader.GetString("PDESCRIPTION"),
                            PSALES = reader.GetString("PSALES"),
                            PTYPESOFSALES = reader.GetString("PTYPESOFSALES"),
                            PPURCHASEUOM = reader.GetString("PPURCHASEUOM"),
                            PHSN = reader.GetString("PHSN"),
                            PREORDERLEVEL = reader.GetString("PREORDERLEVEL"),
                            PFROMOFPRODUCT = reader.GetString("PFROMOFPRODUCT"),
                            PRETURNPOLICY = reader.GetString("PRETURNPOLICY"),
                            PPRODUCTUSE = reader.GetString("PPRODUCTUSE"),
                            PIMAGES = reader.GetString("PIMAGES"),
                            PIGST = reader.GetDouble("PIGST"),
                            PSGST = reader.GetDouble("PSGST"),
                            PCESS = reader.GetDouble("PCESS"),
                            CTNAME = reader.GetString("CTNAME"),
                            SCTNAME = reader.GetString("SCTNAME"),
                            PRODUCTID = reader.GetString("PRODUCTID"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                            BRANDNAME = reader.GetString("BRANDNAME"),
                            RETAILP = reader.GetDouble("RETAILP"),
                            DEALERP = reader.GetDouble("DEALERP"),
                            RATE = reader.GetString("RATE"),
                            RANGE = reader.GetString("RANGE"),
                            DISCOUNTTYPE = reader.GetString("DISCOUNTTYPE"),
                            SUBCATEGORY = reader.GetString("SUBCATEGORY"),
                            CATEGORY = reader.GetString("CATEGORY"),
                            SALETYPE = reader.GetString("SALETYPE"),
                            PRODUCTTYPE = reader.GetString("PRODUCTTYPE"),

                        });
                    }
                }
            }
            return list;
        }

    }
}
