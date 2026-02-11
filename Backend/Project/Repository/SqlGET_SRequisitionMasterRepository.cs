using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_SRequisitionMasterRepository : IGET_SRequisitionMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SRequisitionMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_SRequisitionMaster>> getasync(GET_SRequisitionMasterDomain input)
        {
            List<GET_SRequisitionMaster> list = new List<GET_SRequisitionMaster>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_SRequisitionMaster]('" + input.SRAID + "' , '"+input.KEYWORD+"' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY SYSDT  desc ";
                dbContext.Database.OpenConnection();

                using(var reader = await  command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_SRequisitionMaster
                        {
                            SRAID = reader.GetString("SRAID"),
                            SRQDATE = reader.GetString("SRQDATE"),
                            SREQNO = reader.GetString("SREQNO"),
                            SREXPCDATE = reader.GetString("SREXPCDATE"),
                            SRCATEGORY = reader.GetString("SRCATEGORY"),
                            SRVANDORID = reader.GetString("SRVANDORID"),
                            SRNARRATION = reader.GetString("SRNARRATION"),
                            SRTERMANDCONDITION = reader.GetString("SRTERMANDCONDITION"),
                            SRMDATE = reader.GetString("SRMDATE"),
                            SRNO = reader.GetString("SRNO"),
                            ISCOMPLETED = reader.GetBoolean("ISCOMPLETED"),
                            SYSDT = reader.GetDateTime("SYSDT")
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_SRequisitionMaster>> getkeyword(GET_SRequisitionMasterDomain input)
        {
            List<GET_SRequisitionMaster> list = new List<GET_SRequisitionMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SRequisitionMaster]('" + input.SRAID + "' , '" + input.KEYWORD + "' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY SYSDT  desc ";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_SRequisitionMaster
                        {
                            SRAID = reader.GetString("SRAID"),
                            SRQDATE = reader.GetString("SRQDATE"),
                            SREQNO = reader.GetString("SREQNO"),
                            SREXPCDATE = reader.GetString("SREXPCDATE"),
                            SRCATEGORY = reader.GetString("SRCATEGORY"),
                            SRVANDORID = reader.GetString("SRVANDORID"),
                            SRNARRATION = reader.GetString("SRNARRATION"),
                            SRTERMANDCONDITION = reader.GetString("SRTERMANDCONDITION"),
                            SRMDATE = reader.GetString("SRMDATE"),
                            SRNO = reader.GetString("SRNO"),
                            ISCOMPLETED = reader.GetBoolean("ISCOMPLETED"),
                            SYSDT = reader.GetDateTime("SYSDT")
                        });
                    }
                }
            }
            return list;
        }
            
    }
}
