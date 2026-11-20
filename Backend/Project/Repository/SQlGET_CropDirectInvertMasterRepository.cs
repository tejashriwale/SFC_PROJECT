using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SQlGET_CropDirectInvertMasterRepository : IGET_CropDirectInvertMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SQlGET_CropDirectInvertMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CropDirectInvertMaster>> getasync(GET_CropDirectInvertMasterDomain input)
        {
            List<GET_CropDirectInvertMaster> list = new List<GET_CropDirectInvertMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20  * FROM [GET_CropDirectInvertMaster]( @p0, @p1  , @p2 , @p3) ORDER BY CRDATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.CROPID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CropDirectInvertMaster
                        {
                            SHAID = reader.GetString("SHAID"),
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            CAREA = reader.GetString("CAREA"),
                            BILLNO = reader.GetString("BILLNO"),
                            CARET = reader.GetDouble("CARET"),
                            CONTACTNO = reader.GetString("CONTACTNO"),
                            CRDATE = reader.GetString("CRDATE"),
                            CROPNAME = reader.GetString("CROPNAME"),
                            CVARIETY = reader.GetString("CVARIETY"),
                            FAID = reader.GetString("FAID"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            NARR = reader.GetString("NARR"),
                            RATE = reader.GetDouble("RATE"),
                            CROPID = reader.GetString("CROPID"),
                            CTYPE = reader.GetString("CTYPE"),
                            EPRODUCTION = reader.GetString("EPRODUCTION"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_CropDirectInvertMaster>> getkeyword(GET_CropDirectInvertMasterDomain input)
        {
            List<GET_CropDirectInvertMaster> list = new List<GET_CropDirectInvertMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT   * FROM [GET_CropDirectInvertMaster]( @p0, @p1  , @p2 , @p3) ORDER BY CRDATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.CROPID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CropDirectInvertMaster
                        {
                            SHAID = reader.GetString("SHAID"),
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            CAREA = reader.GetString("CAREA"),
                            BILLNO = reader.GetString("BILLNO"),
                            CARET = reader.GetDouble("CARET"),
                            CONTACTNO = reader.GetString("CONTACTNO"),
                            CRDATE = reader.GetString("CRDATE"),
                            CROPNAME = reader.GetString("CROPNAME"),
                            CVARIETY = reader.GetString("CVARIETY"),
                            FAID = reader.GetString("FAID"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            NARR = reader.GetString("NARR"),
                            RATE = reader.GetDouble("RATE"),
                            CROPID = reader.GetString("CROPID"),
                            CTYPE = reader.GetString("CTYPE"),
                            EPRODUCTION = reader.GetString("EPRODUCTION"),
                            WEIGHT = reader.GetDouble("WEIGHT"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
