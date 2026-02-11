using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_GateEntry_VTRepository : IGET_GateEntry_VTRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_GateEntry_VTRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_GateEntry_VT>> getasync(GET_GateEntry_VTDomain input)
        {
            List<GET_GateEntry_VT> list = new List<GET_GateEntry_VT>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_GateEntry_VT]('" + input.VEHNO + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "', '" + input.DATE + "')order by date desc";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_GateEntry_VT
                        {
                             DPKID = reader.GetString("DPKID"),
                             MAID = reader.GetString("MAID"),
                             VEHNO = reader.GetString("VEHNO"),
                             TOKNNO = reader.GetString("TOKNNO"),
                             DATE = reader.GetString("DATE"),
                             FULLNAME = reader.GetString("FULLNAME"),
                             AADHARNO = reader.GetString("AADHARNO"),
                             MOBILENO = reader.GetString("MOBILENO"),
                             VILLAGE = reader.GetString("VILLAGE"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            CARETS_COUNT = reader.GetString("CARETS_COUNT"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            ISCOMPLETED = reader.GetBoolean("ISCOMPLETED"),
                            CROPLABEL = reader.GetString("CROPLABEL"),
                            USERID = reader.GetString("USERID"),
                           
                        });
                    }
                }
            }
            return list;
        }
    }
}
