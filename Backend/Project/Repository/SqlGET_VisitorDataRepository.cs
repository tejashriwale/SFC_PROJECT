using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_VisitorDataRepository : IGET_VisitorDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VisitorDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_VisitorData>> getasync(GET_VisitorDataDomain input)
        {
            List<GET_VisitorData> list = new List<GET_VisitorData>();
            using (var commmand = dbContext.Database.GetDbConnection().CreateCommand())
            {
                commmand.CommandText = "SELECT  * FROM [GET_VisitorData](@p0,@p1,@p2)";
                { var commmand_p0 = commmand.CreateParameter(); commmand_p0.ParameterName = "@p0"; commmand_p0.Value = (object)(Convert.ToString(input.VAID) ?? ""); commmand.Parameters.Add(commmand_p0); }
                { var commmand_p1 = commmand.CreateParameter(); commmand_p1.ParameterName = "@p1"; commmand_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); commmand.Parameters.Add(commmand_p1); }
                { var commmand_p2 = commmand.CreateParameter(); commmand_p2.ParameterName = "@p2"; commmand_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); commmand.Parameters.Add(commmand_p2); }
                dbContext.Database.OpenConnection();

                using (var reader = await commmand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VisitorData
                        {
                            VAID = reader.GetString("VAID"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            VISITINGDATE = reader.GetDateTime("VISITINGDATE"),
                            PHONENUMBER = reader.GetString("PHONENUMBER"),
                            LOCATION = reader.GetString("LOCATION"),
                            UAID = reader.GetString("UAID"),
                            COMPANYID = reader.GetString("COMPANYID"),
                            DEPTID = reader.GetString("DEPTID"),
                            EXPECTEDHARVESTINGDATE = reader.GetDateTime("EXPECTEDHARVESTINGDATE"),
                            EXPECTEDPRODUCTION = reader.GetString("EXPECTEDPRODUCTION"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            VDAID = reader.GetString("VDAID"),
                            CROPTYPE = reader.GetString("CROPTYPE"),
                            CROPVARIETY = reader.GetString("CROPVARIETY"),
                            PHOTOES = reader.GetString("PHOTOES"),
                            ADVANCEMONEY = reader.GetDouble("ADVANCEMONEY"),
                            CROPAREA = reader.GetString("CROPAREA"),
                            FNAME = reader.GetString("FNAME"),
                            RATE = reader.GetDouble("RATE"),
                            ADVAMT = reader.GetDouble("ADVAMT"),







                        });
                    }
                }
                return list;
            }
        }
    }
}
