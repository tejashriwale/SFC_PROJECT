using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public class SqlGET_VyapariMasterRepository : IGET_VyapariMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_VyapariMaster>> getasync(GET_VyapariMasterDomain input)
        {
            List<GET_VyapariMaster> list = new List<GET_VyapariMaster> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand ())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_VyapariMaster]('" + input.VPAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')order by CONVERT(date, VREGISTRATION, 106) desc";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_VyapariMaster
                        {
                            VPAID = reader.GetString("VPAID"),
                            VNAME = reader.GetString("VNAME"),
                            VSHORTNAME = reader.GetString("VSHORTNAME"),
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
                            VYAPARITYPE = reader.GetString("VYAPARITYPE"),
                            VBRANCHNAME = reader.GetString("VBRANCHNAME"),
                            VREGISTRATION = reader.GetString("VREGISTRATION"),
                            VPPHOTO	 = reader.GetString("VPPHOTO"),
                            VPAADHAR = reader.GetString("VPAADHAR"),
                            VPPAN = reader.GetString("VPPAN"),
                            VPSCHEME = reader.GetString("VPSCHEME"),
                            OTP = reader.GetInt32("OTP"),
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_VyapariMaster>> getkeyword(GET_VyapariMasterDomain input)
        {
            List<GET_VyapariMaster> list = new List<GET_VyapariMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_VyapariMaster]('" + input.VPAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')order by CONVERT(date, VREGISTRATION, 106) desc";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VyapariMaster
                        {
                            VNAME = reader.GetString("VNAME"),
                            VTYPE = reader.GetString("VTYPE"),
                            VSHORTNAME = reader.GetString("VSHORTNAME"),
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
                            VYAPARITYPE = reader.GetString("VYAPARITYPE"),
                            VBRANCHNAME = reader.GetString("VBRANCHNAME"),
                            VREGISTRATION = reader.GetString("VREGISTRATION"),
                            VPPHOTO = reader.GetString("VPPHOTO"),
                            VPAADHAR = reader.GetString("VPAADHAR"),
                            VPPAN = reader.GetString("VPPAN"),
                            VPSCHEME = reader.GetString("VPSCHEME"),
                            OTP = reader.GetInt32("OTP"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
