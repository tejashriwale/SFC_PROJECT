using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_GateFarmerRepository : IGET_GateFarmerRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_GateFarmerRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_GateFarmer>> getasync(GET_GateFarmerDomain input)
        {
            List<GET_GateFarmer> list = new List<GET_GateFarmer> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_GateFarmer]('" + input.FAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY date DESC";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_GateFarmer
                        {
                            FAID = reader.GetString("FAID"),
                            FNAME = reader.GetString("FNAME"),
                            FCONTACTNO = reader.GetString("FCONTACTNO"),
                            FACCONAME = reader.GetString("FACCONAME"),
                            FBIRTHDATE = reader.GetString("FBIRTHDATE"),
                            FPANNO = reader.GetString("FPANNO"),
                            FADDHARNO = reader.GetString("FADDHARNO"),
                            FBANKNAME = reader.GetString("FBANKNAME"),
                            FBRANCHNAME = reader.GetString("FBRANCHNAME"),
                            FACCOUNTNO = reader.GetString("FACCOUNTNO"),
                            FIFSCCODE = reader.GetString("FIFSCCODE"),
                            FPINCODE = reader.GetString("FPINCODE"),
                            FSTATE = reader.GetString("FSTATE"),
                            FCITY = reader.GetString("FCITY"),
                            FCROPAREA = reader.GetString("FCROPAREA"),
                            FCROPTYPE = reader.GetString("FCROPTYPE"),
                            FSTATUS = reader.GetString("FSTATUS"),
                            FAddress = reader.GetString("FAddress"),
                            UID = reader.GetString("UID"),
                            FCMTOKEN = reader.GetString("FCMTOKEN"),
                            REFID = reader.GetString("REFID"),
                            FPROFILE = reader.GetString("FPROFILE"),
                            DATE = reader.GetString("DATE"),
                           
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_GateFarmer>> getkeyword(GET_GateFarmerDomain input)
        {
            List<GET_GateFarmer> list = new List<GET_GateFarmer>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_GateFarmer]('" + input.FAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY date DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_GateFarmer
                        {
                            FAID = reader.GetString("FAID"),
                            FNAME = reader.GetString("FNAME"),
                            FCONTACTNO = reader.GetString("FCONTACTNO"),
                            FACCONAME = reader.GetString("FACCONAME"),
                            FBIRTHDATE = reader.GetString("FBIRTHDATE"),
                            FPANNO = reader.GetString("FPANNO"),
                            FADDHARNO = reader.GetString("FADDHARNO"),
                            FBANKNAME = reader.GetString("FBANKNAME"),
                            FBRANCHNAME = reader.GetString("FBRANCHNAME"),
                            FACCOUNTNO = reader.GetString("FACCOUNTNO"),
                            FIFSCCODE = reader.GetString("FIFSCCODE"),
                            FPINCODE = reader.GetString("FPINCODE"),
                            FSTATE = reader.GetString("FSTATE"),
                            FCITY = reader.GetString("FCITY"),
                            FCROPAREA = reader.GetString("FCROPAREA"),
                            FCROPTYPE = reader.GetString("FCROPTYPE"),
                            FSTATUS = reader.GetString("FSTATUS"),
                            FAddress = reader.GetString("FAddress"),
                            UID = reader.GetString("UID"),
                            FCMTOKEN = reader.GetString("FCMTOKEN"),
                            REFID = reader.GetString("REFID"),
                            FPROFILE = reader.GetString("FPROFILE"),
                            DATE = reader.GetString("DATE"),
                           
                        });
                    }
                }
            }
            return list;
        }
    }
}
