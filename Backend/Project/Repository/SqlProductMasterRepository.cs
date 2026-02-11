using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlProductMasterRepository : IProductMasterRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlProductMasterRepository(ApplicationDbContext dbContext)
        {
           this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(ProductMaster productMaster)
        {
           List<ApiResponseMessage> result = new List<ApiResponseMessage>();

            using(var command = dbContext.Database.GetDbConnection().CreateCommand() )
            {
                command.CommandText = "[SP_AddUpdProductMaster]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@PAID";
                parameter.Value = productMaster.PAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PCODE";
                parameter.Value = productMaster.PCODE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PNAME";
                parameter.Value = productMaster.PNAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PTYPE";
                parameter.Value = productMaster.PTYPE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PCATEGORY";
                parameter.Value = productMaster.PCATEGORY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PSUBCATEGORY";
                parameter.Value = productMaster.PSUBCATEGORY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PDESCRIPTION";
                parameter.Value = productMaster.PDESCRIPTION;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PSALES";
                parameter.Value = productMaster.PSALES;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PTYPESOFSALES";
                parameter.Value = productMaster.PTYPESOFSALES;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PPURCHASEUOM";
                parameter.Value = productMaster.PPURCHASEUOM;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PHSN";
                parameter.Value = productMaster.PHSN;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PREORDERLEVEL";
                parameter.Value = productMaster.PREORDERLEVEL;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PFROMOFPRODUCT";
                parameter.Value = productMaster.PFROMOFPRODUCT;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PRETURNPOLICY";
                parameter.Value = productMaster.PRETURNPOLICY;

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PPRODUCTUSE";
                parameter.Value = productMaster.PPRODUCTUSE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PIMAGES";
                parameter.Value = productMaster.PIMAGES;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PIGST";
                parameter.Value = productMaster.PIGST;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PSGST";
                parameter.Value = productMaster.PSGST;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PCESS";
                parameter.Value = productMaster.PCESS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@pcgst";
                parameter.Value = productMaster.pcgst;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@COMPANYID";
                parameter.Value = productMaster.COMPANYID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DEPTID";
                parameter.Value = productMaster.DEPTID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PRODUCTID";
                parameter.Value = productMaster.PRODUCTID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PRODUCTNAME";
                parameter.Value = productMaster.PRODUCTNAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@BRANDNAME";
                parameter.Value = productMaster.BRANDNAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@RETAILP";
                parameter.Value = productMaster.RETAILP;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DEALERP";
                parameter.Value = productMaster.DEALERP;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@RATE";
                parameter.Value = productMaster.RATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@RANGE";
                parameter.Value = productMaster.RANGE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DISCOUNTTYPE";
                parameter.Value = productMaster.DISCOUNTTYPE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SUBCATEGORY";
                parameter.Value = productMaster.SUBCATEGORY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CATEGORY";
                parameter.Value = productMaster.CATEGORY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SALETYPE";
                parameter.Value = productMaster.SALETYPE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PRODUCTTYPE";
                parameter.Value = productMaster.PRODUCTTYPE;
                command.Parameters.Add(parameter);



                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new ApiResponseMessage
                        {
                            IsSuccessful = reader.GetInt32("IsSuccessful"),
                            ResponseCode = reader.GetString("ResponseCode"),
                            ResponseMessage = reader.GetString("ResponseMessage"),
                            ResponseValues = reader.GetString("ResponseValues")
                        });
                    }
                }
            }
            return result;
        }
    }
}
