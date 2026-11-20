using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_MasterDetailInvertDataRepository : IGET_MasterDetailInvertDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_MasterDetailInvertDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ;
        }
        public async Task<List<GET_MasterDetailInvertData>> getasync(GET_MasterDetailInvertDataDomain input)
        {
            List<GET_MasterDetailInvertData> list = new List<GET_MasterDetailInvertData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20  * FROM [GET_MasterDetailInvertData](@p0 , @p1, @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.SHAID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_MasterDetailInvertData
                        {
                           SHAID = reader.GetString("SHAID"),
                           AMOUNT = reader.GetDouble("AMOUNT"),
                           SHDAID = reader.GetString("SHDAID"),
                           CAREA = reader.GetString("CAREA"),
                           CARET = reader.GetDouble("CARET"),
                           CDAID = reader.GetString("CDAID"),
                           CRATECOUNT = reader.GetString("CRATECOUNT"),
                           CONTACTNO = reader.GetString("CONTACTNO"),
                           CRDATE = reader.GetString("CRDATE"),
                           CVARIETY = reader.GetString("CVARIETY"),
                           FAID = reader.GetString("FAID"),
                           NARR = reader.GetString("NARR"),
                           RATE = reader.GetDouble("RATE"),
                           PACKER = reader.GetString("PACKER"),
                           SHSDATE = reader.GetString("SHSDATE"),
                           CROPID = reader.GetString("CROPID"),
                           CROPINVERT = reader.GetString("CROPINVERT"),
                           CTYPE = reader.GetString("CTYPE"),
                           PSIZE = reader.GetString("PSIZE"),
                           DWEIGHT = reader.GetDouble("DWEIGHT"),
                           EPRODUCTION = reader.GetString("EPRODUCTION"),
                           PQUNTITY = reader.GetString("PQUNTITY"),
                           VEHNO = reader.GetString("VEHNO"),
                           WEIGHT = reader.GetDouble("WEIGHT"),
                           WORKER = reader.GetString("WORKER"),
                           STAID = reader.GetString("STAID"),
                           FNAME = reader.GetString("FNAME"),
                           ITEMNE = reader.GetString("ITEMNE"),
                           ISDELETED = reader.GetBoolean("ISDELETED"),
                           CGRADE = reader.GetString("CGRADE")
                        });
                    }
                }
            }
            return list;
        }
    }
}
