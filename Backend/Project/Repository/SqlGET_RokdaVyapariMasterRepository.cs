using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public class SqlGET_RokdaVyapariMasterRepository : IGET_RokdaVyapariMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_RokdaVyapariMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_RokdaVyapariMaster>> getasync(GET_RokdaVyapariMasterDomain input)
        {
            List<GET_RokdaVyapariMaster> list = new List<GET_RokdaVyapariMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_RokdaVyapariMaster]('" + input.VPAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_RokdaVyapariMaster
                        {
                            VPAID = reader.GetString("VPAID"),
                            VREGISTRATION = reader.GetString("VREGISTRATION"),
                            VNAME = reader.GetString("VNAME"),
                            VTYPE = reader.GetString("VTYPE"),
                            VMOBLIE = reader.GetString("VMOBLIE"),
                            VEMAIL = reader.GetString("VEMAIL"),
                            VAADHAR = reader.GetString("VAADHAR"),
                            VPAN = reader.GetString("VPAN"),
                            VBIRTHDAYDATE = reader.GetString("VBIRTHDAYDATE"),
                            VANIVERSARYDATE = reader.GetString("VANIVERSARYDATE"),
                            VSTATUS = reader.GetString("VSTATUS"),
                            VADDRESS = reader.GetString("VADDRESS"),
                            VAREA = reader.GetString("VAREA"),
                            VPINCODE = reader.GetInt32("VPINCODE"),
                            VCITY = reader.GetString("VCITY"),
                            VSTATE = reader.GetString("VSTATE"),
                            VSAMEADDRESS = reader.GetBoolean("VSAMEADDRESS"),
                            VGSTIN = reader.GetString("VGSTIN"),
                            VCOMPNAME = reader.GetString("VCOMPNAME"),
                            VCOMPPAN = reader.GetString("VCOMPPAN"),
                            VCOMPADDRESS = reader.GetString("VCOMPADDRESS"),
                            VCOMPAREA = reader.GetString("VCOMPAREA"),
                            VCOMPPINCODE = reader.GetInt32("VCOMPPINCODE"),
                            VCOMPCITY = reader.GetString("VCOMPCITY"),
                            VCOMPSTATE = reader.GetString("VCOMPSTATE"),
                            VIFSCCODE = reader.GetString("VIFSCCODE"),
                            VACCOUNTNUMBER = reader.GetString("VACCOUNTNUMBER"),
                            VACCOUNTNAME = reader.GetString("VACCOUNTNAME"),
                            VBANKNAME = reader.GetString("VBANKNAME"),
                            VBRANCHNAME = reader.GetString("VBRANCHNAME"),
                            VYAPARITYPE = reader.GetString("VYAPARITYPE"),
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_RokdaVyapariMaster>> getkeyword(GET_RokdaVyapariMasterDomain input)
        {
            List<GET_RokdaVyapariMaster> list = new List<GET_RokdaVyapariMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_RokdaVyapariMaster]('" + input.VPAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_RokdaVyapariMaster
                        {
                            VPAID = reader.GetString("VPAID"),
                            VREGISTRATION = reader.GetString("VREGISTRATION"),
                            VNAME = reader.GetString("VNAME"),
                            VTYPE = reader.GetString("VTYPE"),
                            VMOBLIE = reader.GetString("VMOBLIE"),
                            VEMAIL = reader.GetString("VEMAIL"),
                            VAADHAR = reader.GetString("VAADHAR"),
                            VPAN = reader.GetString("VPAN"),
                            VBIRTHDAYDATE = reader.GetString("VBIRTHDAYDATE"),
                            VANIVERSARYDATE = reader.GetString("VANIVERSARYDATE"),
                            VSTATUS = reader.GetString("VSTATUS"),
                            VADDRESS = reader.GetString("VADDRESS"),
                            VAREA = reader.GetString("VAREA"),
                            VPINCODE = reader.GetInt32("VPINCODE"),
                            VCITY = reader.GetString("VCITY"),
                            VSTATE = reader.GetString("VSTATE"),
                            VSAMEADDRESS = reader.GetBoolean("VSAMEADDRESS"),
                            VGSTIN = reader.GetString("VGSTIN"),
                            VCOMPNAME = reader.GetString("VCOMPNAME"),
                            VCOMPPAN = reader.GetString("VCOMPPAN"),
                            VCOMPADDRESS = reader.GetString("VCOMPADDRESS"),
                            VCOMPAREA = reader.GetString("VCOMPAREA"),
                            VCOMPPINCODE = reader.GetInt32("VCOMPPINCODE"),
                            VCOMPCITY = reader.GetString("VCOMPCITY"),
                            VCOMPSTATE = reader.GetString("VCOMPSTATE"),
                            VIFSCCODE = reader.GetString("VIFSCCODE"),
                            VACCOUNTNUMBER = reader.GetString("VACCOUNTNUMBER"),
                            VACCOUNTNAME = reader.GetString("VACCOUNTNAME"),
                            VBANKNAME = reader.GetString("VBANKNAME"),
                            VBRANCHNAME = reader.GetString("VBRANCHNAME"),
                            VYAPARITYPE = reader.GetString("VYAPARITYPE"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
