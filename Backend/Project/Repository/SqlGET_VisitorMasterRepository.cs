using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_VisitorMasterRepository : IGET_VisitorMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VisitorMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ;
        }
        public async Task<List<GET_VisitorMaster>> getasync(GET_VisitorMasterDomain input)
        {
            List<GET_VisitorMaster> list = new List<GET_VisitorMaster>();
            using (var commmand = dbContext.Database.GetDbConnection().CreateCommand())
            {
                commmand.CommandText = "SELECT Top 20 * FROM [GET_VisitorMaster](@p0,@p1,@p2,@p3)";
                { var commmand_p0 = commmand.CreateParameter(); commmand_p0.ParameterName = "@p0"; commmand_p0.Value = (object)(Convert.ToString(input.VAID) ?? ""); commmand.Parameters.Add(commmand_p0); }
                { var commmand_p1 = commmand.CreateParameter(); commmand_p1.ParameterName = "@p1"; commmand_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); commmand.Parameters.Add(commmand_p1); }
                { var commmand_p2 = commmand.CreateParameter(); commmand_p2.ParameterName = "@p2"; commmand_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); commmand.Parameters.Add(commmand_p2); }
                { var commmand_p3 = commmand.CreateParameter(); commmand_p3.ParameterName = "@p3"; commmand_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); commmand.Parameters.Add(commmand_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await commmand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VisitorMaster
                        {
                            VAID = reader.GetString("VAID"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            VISITINGDATE = reader.GetString("VISITINGDATE"),
                            PHONENUMBER = reader.GetString("PHONENUMBER"),
                            LOCATION = reader.GetString("LOCATION"),
                            UAID = reader.GetString("UAID"),
                            COMPANYID = reader.GetString("COMPANYID"),
                            DEPTID = reader.GetString("DEPTID"),
                            FNAME = reader.GetString("FNAME"),
                            ISCOMPLITED = reader.GetString("ISCOMPLITED"),
                            ADVAMT = reader.GetDouble("ADVAMT")






                        });
                    }
                }
                return list;
            }
        }

        public async Task<List<GET_VisitorMaster>> getkeyword(GET_VisitorMasterDomain input)
        {
            List<GET_VisitorMaster> list = new List<GET_VisitorMaster>();
            using (var commmand = dbContext.Database.GetDbConnection().CreateCommand())
            {
                commmand.CommandText = "SELECT Top 20 * FROM [GET_VisitorMaster](@p0,@p1,@p2,@p3)";
                { var commmand_p0 = commmand.CreateParameter(); commmand_p0.ParameterName = "@p0"; commmand_p0.Value = (object)(Convert.ToString(input.VAID) ?? ""); commmand.Parameters.Add(commmand_p0); }
                { var commmand_p1 = commmand.CreateParameter(); commmand_p1.ParameterName = "@p1"; commmand_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); commmand.Parameters.Add(commmand_p1); }
                { var commmand_p2 = commmand.CreateParameter(); commmand_p2.ParameterName = "@p2"; commmand_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); commmand.Parameters.Add(commmand_p2); }
                { var commmand_p3 = commmand.CreateParameter(); commmand_p3.ParameterName = "@p3"; commmand_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); commmand.Parameters.Add(commmand_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await commmand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VisitorMaster
                        {
                            VAID = reader.GetString("VAID"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            VISITINGDATE = reader.GetString("VISITINGDATE"),
                            PHONENUMBER = reader.GetString("PHONENUMBER"),
                            LOCATION = reader.GetString("LOCATION"),
                            UAID = reader.GetString("UAID"),
                            COMPANYID = reader.GetString("COMPANYID"),
                            DEPTID = reader.GetString("DEPTID"),
                            FNAME = reader.GetString("FNAME"),
                            ISCOMPLITED = reader.GetString("ISCOMPLITED"),
                            ADVAMT = reader.GetDouble("ADVAMT")


                        });
                    }
                }
                return list;
            }
        }
    }
}
