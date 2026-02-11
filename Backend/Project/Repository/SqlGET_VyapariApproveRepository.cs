using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VyapariApproveRepository : IGET_VyapariApproveRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariApproveRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_VyapariApprove>> getasync(GET_VyapariApproveDomain input)
        {
            List<GET_VyapariApprove> list = new List<GET_VyapariApprove>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_VyapariApprove]('" + input.BAID + "' ,  '" + input.KEYWORD + "' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY ISVYAPARIVERIFIED, DATE DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VyapariApprove
                        {
                            BAID = reader.GetString("BAID"),
                            MAID = reader.GetString("MAID"),
                            FNAME = reader.GetString("FNAME"),
                            ISVYAPARIVERIFIED = reader.GetBoolean("ISVYAPARIVERIFIED"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            VNAME = reader.GetString("VNAME"),
                            RATE = reader.GetDouble("RATE"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            WEIGHTMINMAX = reader.GetString("WEIGHTMINMAX"),
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            DATE = reader.GetString("DATE"),
                            BDAID = reader.GetString("BDAID"),
                            BILLNO = reader.GetInt32("BILLNO"),
                           

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_VyapariApprove>> getkeyword(GET_VyapariApproveDomain input)
        {
            List<GET_VyapariApprove> list = new List<GET_VyapariApprove>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_VyapariApprove]('" + input.BAID + "' ,  '" + input.KEYWORD + "' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY ISVYAPARIVERIFIED ,DATE DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VyapariApprove
                        {
                            BAID = reader.GetString("BAID"),
                            MAID = reader.GetString("MAID"),
                            FNAME = reader.GetString("FNAME"),
                            ISVYAPARIVERIFIED = reader.GetBoolean("ISVYAPARIVERIFIED"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            VNAME = reader.GetString("VNAME"),
                            RATE = reader.GetDouble("RATE"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            WEIGHTMINMAX = reader.GetString("WEIGHTMINMAX"),
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            DATE = reader.GetString("DATE"),
                            BILLNO = reader.GetInt32("BILLNO"),


                        });
                    }
                }
            }
            return list;
        }
    }
}
