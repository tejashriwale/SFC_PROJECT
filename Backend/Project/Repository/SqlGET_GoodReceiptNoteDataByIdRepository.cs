using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_GoodReceiptNoteDataByIdRepository : IGET_GoodReceiptNoteDataByIdRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_GoodReceiptNoteDataByIdRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_GoodReceiptNoteDataById>> getasync(GET_GoodReceiptNoteDataByIdDomain input)
        {
            List<GET_GoodReceiptNoteDataById> list = new List<GET_GoodReceiptNoteDataById>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_GoodReceiptNoteDataById]('" + input.GRNAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_GoodReceiptNoteDataById
                        {
                            GRNAID = reader.GetString("GRNAID"),
                            GRNTRANSACTIONID = reader.GetString("GRNTRANSACTIONID"),
                            GRNDATE = reader.GetString("GRNDATE"),
                            GRNNUMBER = reader.GetString("GRNNUMBER"),
                            GRNCHALLANNO = reader.GetString("GRNCHALLANNO"),
                            GRNCHALLANDATE = reader.GetString("GRNCHALLANDATE"),
                            GRNVECHILENO = reader.GetString("GRNVECHILENO"),
                            GRNVENDOR = reader.GetString("GRNVENDOR"),
                            GRNCONSIGNER = reader.GetString("GRNCONSIGNER"),
                            GRNINSPECTEDBY = reader.GetString("GRNINSPECTEDBY"),
                            GRNNARRATION = reader.GetString("GRNNARRATION"),
                            PCREFF = reader.GetString("PCREFF"),
                            GRNDAID = reader.GetString("GRNDAID"),
                            GRNDPRODUCT = reader.GetString("GRNDPRODUCT"),
                            GRNDEPARTMENT = reader.GetString("GRNDEPARTMENT"),
                            GRNDUOM = reader.GetString("GRNDUOM"),
                            GRNDCHALLANQUANTITY = reader.GetDouble("GRNDCHALLANQUANTITY"),
                            GRNDSHORTQUANTITY = reader.GetDouble("GRNDSHORTQUANTITY"),
                            GRNDEXCESSQUANTITY = reader.GetDouble("GRNDEXCESSQUANTITY"),
                            GRNDREJECTEDQUANTITY = reader.GetDouble("GRNDREJECTEDQUANTITY"),
                            GRNDACCEPTEDQUANTITY = reader.GetDouble("GRNDACCEPTEDQUANTITY"),
                            GRNDRATE = reader.GetDouble("GRNDRATE"),
                            GRNDDISCOUNT = reader.GetDouble("GRNDDISCOUNT"),
                            GRNDTOTALVALUE = reader.GetDouble("GRNDTOTALVALUE"),
                            HSNCODE = reader.GetInt32("HSNCODE"),
                            UOMTITLE = reader.GetString("UOMTITLE"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            CONTACTNUMBER = reader.GetString("CONTACTNUMBER"),
                            EMAIL = reader.GetString("EMAIL"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                            BATCHNO = reader.GetString("BATCHNO"),
                            EXPIRYDATE = reader.GetString("EXPIRYDATE"),
                            GAAID = reader.GetString("GAAID"),
                            QCID = reader.GetString("QCID"),
                            GACONDITION = reader.GetBoolean("GACONDITION"),
                            GAAMT = reader.GetBoolean("GAAMT"),
                            GAEXPECTED = reader.GetBoolean("GAEXPECTED"),
                            GAPACKAGE = reader.GetBoolean("GAPACKAGE"),
                            GACONDITIONDESCRIPTION = reader.GetString("GACONDITIONDESCRIPTION"),
                            GAAMTDESCRIPTION = reader.GetString("GAAMTDESCRIPTION"),
                            GAEXPECTEDDESCRIPTION = reader.GetString("GAEXPECTEDDESCRIPTION"),
                            GAPACKAGEDESCRIPTION = reader.GetString("GAPACKAGEDESCRIPTION"),
                            
                        });
                    }
                }
            }
            return list;
        }
    }
}
