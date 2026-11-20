using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_TyreProductMasterRepository : IGET_TyreProductMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_TyreProductMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_TyreProductMaster>> getasync(GET_TyreProductMasterDomain input)
        {
            List<GET_TyreProductMaster> list = new List<GET_TyreProductMaster>();
            using (var commmand = dbContext.Database.GetDbConnection().CreateCommand())
            {
                commmand.CommandText = "SELECT Top 20 * FROM [GET_TyreProductMaster](@p0,@p1,@p2,@p3)";
                { var commmand_p0 = commmand.CreateParameter(); commmand_p0.ParameterName = "@p0"; commmand_p0.Value = (object)(Convert.ToString(input.PAID) ?? ""); commmand.Parameters.Add(commmand_p0); }
                { var commmand_p1 = commmand.CreateParameter(); commmand_p1.ParameterName = "@p1"; commmand_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); commmand.Parameters.Add(commmand_p1); }
                { var commmand_p2 = commmand.CreateParameter(); commmand_p2.ParameterName = "@p2"; commmand_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); commmand.Parameters.Add(commmand_p2); }
                { var commmand_p3 = commmand.CreateParameter(); commmand_p3.ParameterName = "@p3"; commmand_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); commmand.Parameters.Add(commmand_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await commmand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_TyreProductMaster
                        {
                            PAID = reader.GetString("PAID"),
                            PCODE = reader.GetString("PCODE"),
                            PNAME = reader.GetString("PNAME"),
                            PTYPE = reader.GetString("PTYPE"),
                            PCATEGORY = reader.GetString("PCATEGORY"),
                            PSUBCATEGORY = reader.GetString("PSUBCATEGORY"),
                            PDESCRIPTION = reader.GetString("PDESCRIPTION"),
                            PHSN = reader.GetString("PHSN"),
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
                            PRODUCTTYPE = reader.GetString("PRODUCTTYPE"),
                            SALETYPE = reader.GetString("SALETYPE"),
                            BRANDLABEL = reader.GetString("BRANDLABEL"), 
                            PSALESLABEL = reader.GetString("PSALESLABEL"),
                            PTYPELABEL = reader.GetString("PTYPELABEL"),
                            PCESS = reader.GetDouble("PCESS"),
                            PIGST = reader.GetDouble("PIGST"),
                            PSGST = reader.GetDouble("PSGST"),
                            PCGST = reader.GetDouble("PCGST"),
                            PSALES = reader.GetString("PSALES")

                        });
                    }
                }
                return list;
            }
        }

        public async Task<List<GET_TyreProductMaster>> getkeyword(GET_TyreProductMasterDomain input)
        {
            List<GET_TyreProductMaster> list = new List<GET_TyreProductMaster>();
            using (var commmand = dbContext.Database.GetDbConnection().CreateCommand())
            {
                commmand.CommandText = "SELECT * FROM [GET_TyreProductMaster](@p0,@p1,@p2,@p3)";
                { var commmand_p0 = commmand.CreateParameter(); commmand_p0.ParameterName = "@p0"; commmand_p0.Value = (object)(Convert.ToString(input.PAID) ?? ""); commmand.Parameters.Add(commmand_p0); }
                { var commmand_p1 = commmand.CreateParameter(); commmand_p1.ParameterName = "@p1"; commmand_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); commmand.Parameters.Add(commmand_p1); }
                { var commmand_p2 = commmand.CreateParameter(); commmand_p2.ParameterName = "@p2"; commmand_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); commmand.Parameters.Add(commmand_p2); }
                { var commmand_p3 = commmand.CreateParameter(); commmand_p3.ParameterName = "@p3"; commmand_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); commmand.Parameters.Add(commmand_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await commmand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_TyreProductMaster
                        {
                            PAID = reader.GetString("PAID"),
                            PCODE = reader.GetString("PCODE"),
                            PNAME = reader.GetString("PNAME"),
                            PTYPE = reader.GetString("PTYPE"),
                            PCATEGORY = reader.GetString("PCATEGORY"),
                            PSUBCATEGORY = reader.GetString("PSUBCATEGORY"),
                            PDESCRIPTION = reader.GetString("PDESCRIPTION"),
                            PHSN = reader.GetString("PHSN"),
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
                            PRODUCTTYPE = reader.GetString("PRODUCTTYPE"),
                            SALETYPE = reader.GetString("SALETYPE"),
                            BRANDLABEL = reader.GetString("BRANDLABEL"),
                            PSALESLABEL = reader.GetString("PSALESLABEL"),
                            PTYPELABEL = reader.GetString("PTYPELABEL"),
                            PCESS = reader.GetDouble("PCESS"),
                            PIGST = reader.GetDouble("PIGST"),
                            PSGST = reader.GetDouble("PSGST"),
                            PCGST = reader.GetDouble("PCGST"),
                            PSALES = reader.GetString("PSALES")


                        });
                    }
                }
                return list;
            }
        }
    }
}
