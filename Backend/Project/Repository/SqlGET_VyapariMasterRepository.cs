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
                command.CommandText = "SELECT Top 20 * FROM [GET_VyapariMaster](@p0 , @p1 , @p2 , @p3)order by CONVERT(date, VREGISTRATION, 106) desc";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.VPAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
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
                command.CommandText = "SELECT * FROM [GET_VyapariMaster](@p0 , @p1 , @p2 , @p3)order by CONVERT(date, VREGISTRATION, 106) desc";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.VPAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
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
