using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CropBillRepository : IGET_CropBillRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CropBillRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CropBill>> getasync(GET_CropBillDomain input)
        {
            List<GET_CropBill> list = new List<GET_CropBill>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_CropBill](@p0 , @p1, @p2) ORDER BY VISITINGDATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.CROPID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CropBill
                        {
                            CROPID = reader.GetString("CROPID"),
                            CBKID = reader.GetString("CBKID"),
                            VISITINGDATE = reader.GetDateTime("VISITINGDATE"),
                            NARR = reader.GetString("NARR"),
                            PHONENUMBER = reader.GetString("PHONENUMBER"),
                            CTYPE = reader.GetString("CTYPE"),
                            CVARIETY = reader.GetString("CVARIETY"),
                            CAREA = reader.GetString("CAREA"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            RATE = reader.GetDouble("RATE"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            CARET = reader.GetDouble("CARET"),
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            ADVANCE = reader.GetDouble("ADVANCE"),
                            FAID = reader.GetString("FAID"),
                            CROPNAME = reader.GetString("CROPNAME"),
                            CDAID = reader.GetString("CDAID"),
                            VDAID = reader.GetString("VDAID"),
                            BILLSTATUS = reader.GetString("BILLSTATUS")



                        });
                    }
                }
            }
            return list;
        }
    }
}
