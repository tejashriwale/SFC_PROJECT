using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_PRequisitionMasterRepositroy : IGET_PRequisitionMasterRepositroy
    {
        private readonly ApplicationDbContext dbContext;

        public SqlGET_PRequisitionMasterRepositroy(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_PRequisitionMaster>> getasync(GET_PRequisitionMasterDomain input)
        {
            List<GET_PRequisitionMaster> list = new List<GET_PRequisitionMaster>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_PRequisitionMaster]('"+input.PRAID+"', '"+input.KEYWORD+"' ,'" + input.COMPANYID + "','" + input.DEPTID + "')order by PRQDATE1 desc";
                dbContext.Database.OpenConnection();

                using(var reader = await  command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_PRequisitionMaster
                        {
                            PRAID = reader.GetString("PRAID"),
                            PRQDATE = reader.GetString("PRQDATE"),
                            PREQNO = reader.GetString("PREQNO"),
                            PREXPCDATE = reader.GetString("PREXPCDATE"),
                            PRCATEGORY = reader.GetString("PRCATEGORY"),
                            PRVANDORID = reader.GetString("PRVANDORID"),
                            PRNARRATION = reader.GetString("PRNARRATION"),
                            PRTERMANDCONDITION = reader.GetString("PRTERMANDCONDITION"),
                            PRMDATE = reader.GetString("PRMDATE"),
                            PRNO = reader.GetString("PRNO"),
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_PRequisitionMaster>> getkeyword(GET_PRequisitionMasterDomain input)
        {
            List<GET_PRequisitionMaster> list = new List<GET_PRequisitionMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PRequisitionMaster]('" + input.PRAID + "', '"+input.KEYWORD+"' ,'" + input.COMPANYID + "','" + input.DEPTID + "')order by PRQDATE1 desc";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_PRequisitionMaster
                        {
                            PRAID = reader.GetString("PRAID"),
                            PRQDATE = reader.GetString("PRQDATE"),
                            PREQNO = reader.GetString("PREQNO"),
                            PREXPCDATE = reader.GetString("PREXPCDATE"),
                            PRCATEGORY = reader.GetString("PRCATEGORY"),
                            PRVANDORID = reader.GetString("PRVANDORID"),
                            PRNARRATION = reader.GetString("PRNARRATION"),
                            PRTERMANDCONDITION = reader.GetString("PRTERMANDCONDITION"),
                            PRMDATE = reader.GetString("PRMDATE"),
                            PRNO = reader.GetString("PRNO"),
                        });
                    }
                }
            }
            return list;
        }
            
    }
}
