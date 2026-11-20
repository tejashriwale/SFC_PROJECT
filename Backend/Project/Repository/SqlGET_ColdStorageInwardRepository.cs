using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_ColdStorageRepository : IGET_ColdStorageInwardRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ColdStorageRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_ColdStorageInward>> getasync(GET_ColdStorageInwardDomain input)
        {
            List<GET_ColdStorageInward> result = new List<GET_ColdStorageInward>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_ColdStorageInward](@p0 , @p1 , @p2 , @p3 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.CSID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_ColdStorageInward
                        {
                            CSID = reader.GetString("CSID"),
                            COLDSTORAGEID = reader.GetString("COLDSTORAGEID"),
                            LOTNO = reader.GetString("LOTNO"),
                            DATE = reader.GetString("DATE"),
                            CUSTOMERNAME = reader.GetString("CUSTOMERNAME"),
                            THIRDPARTYNAME = reader.GetString("THIRDPARTYNAME"),
                            ITEM = reader.GetString("ITEM"),
                          
                            TYPEOFVARITY = reader.GetString("TYPEOFVARITY"),
                            RACKPOSITION = reader.GetString("RACKPOSITION"),
                            CNAME = reader.GetString("CNAME"),
                            PRODUCT = reader.GetString("PRODUCT"),
                            VAJAN = reader.GetString("VAJAN"),
                            GROSSWEIGHT = reader.GetDouble("GROSSWEIGHT"),
                            NETWEIGHT = reader.GetDouble("NETWEIGHT"),
                            WEIGHTUNIT = reader.GetString("WEIGHTUNIT"),
                            WEIGHTINKG = reader.GetDouble("WEIGHTINKG"),
                            CARETWEIGHT = reader.GetDouble("CARETWEIGHT"),
                            
                        });
                    }
                }
            }
            return result;
        }

        public async Task<List<GET_ColdStorageInward>> getkeyword(GET_ColdStorageInwardDomain input)
        {
            List<GET_ColdStorageInward> result = new List<GET_ColdStorageInward>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_ColdStorageInward](@p0 , @p1 , @p2 , @p3 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.CSID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_ColdStorageInward
                        {
                            CSID = reader.GetString("CSID"),
                            COLDSTORAGEID = reader.GetString("COLDSTORAGEID"),
                            LOTNO = reader.GetString("LOTNO"),
                            DATE = reader.GetString("DATE"),
                            CUSTOMERNAME = reader.GetString("CUSTOMERNAME"),
                            THIRDPARTYNAME = reader.GetString("THIRDPARTYNAME"),
                            ITEM = reader.GetString("ITEM"),
                        
                            TYPEOFVARITY = reader.GetString("TYPEOFVARITY"),
                            RACKPOSITION = reader.GetString("RACKPOSITION"),
                            CNAME = reader.GetString("CNAME"),
                            PRODUCT = reader.GetString("PRODUCT"),
                            VAJAN = reader.GetString("VAJAN"),
                            GROSSWEIGHT = reader.GetDouble("GROSSWEIGHT"),
                            NETWEIGHT = reader.GetDouble("NETWEIGHT"),
                            WEIGHTUNIT = reader.GetString("WEIGHTUNIT"),
                            WEIGHTINKG = reader.GetDouble("WEIGHTINKG"),
                            CARETWEIGHT = reader.GetDouble("CARETWEIGHT"),

                        });
                    }
                }
            }
            return result;
        }
    }
}
