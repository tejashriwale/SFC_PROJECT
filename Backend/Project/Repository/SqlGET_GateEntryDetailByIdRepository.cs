using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public class SqlGET_GateEntryDetailByIdRepository : IGET_GateEntryDetailByIdRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_GateEntryDetailByIdRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_GateEntryDetailById>> getasync(GET_GateEntryDetailByIdDomain inupt)
        {
            List<GET_GateEntryDetailById> list = new List<GET_GateEntryDetailById>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_GateEntryDetailById]('" + inupt.VEHNO + "' ,'" + inupt.UID +"' ,'" + inupt.COMPANYID + "' , '" + inupt.DEPTID + "' , '" + inupt.DATE + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_GateEntryDetailById
                        {
                            DPKID = reader.GetString("DPKID"),
                           
                            MAID = reader.GetString("MAID"),
                            TOKNNO = reader.GetString("TOKNNO"),
                            FULLNAME = reader.GetString("FULLNAME"),
                            CARETS_COUNT = reader.GetString("CARETS_COUNT"),
                            VILLAGE = reader.GetString("VILLAGE"),                        
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            VEHNO = reader.GetString("VEHNO"),
                            AADHARNO = reader.GetString("AADHARNO"),
                            MOBILENO = reader.GetString("MOBILENO"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            DATE = reader.GetString("DATE"),
                            UID = reader.GetString("UID"),
                          
                            ISCOMPLETED = reader.GetBoolean("ISCOMPLETED"),
                        });
                    }
                }
            }
            return list;
        }

      
    }
}
