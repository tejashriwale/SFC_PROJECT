using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_SChallanMasterRepository : IGET_SChallanMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SChallanMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_SChallanMaster>> getasync(GET_SChallanMasterDomain input)
        {
           List<GET_SChallanMaster> list = new List<GET_SChallanMaster> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_SChallanMaster](@p0 , @p1 , @p2 , @p3 , @p4)order by  SCDATEconvert Desc";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SCAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(input.STATUSID) ?? ""); command.Parameters.Add(command_p4); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_SChallanMaster
                        {
                            SCAID = reader.GetString("SCAID"),
                            SCTRNNO = reader.GetString("SCTRNNO"),
                            SCSELLER = reader.GetString("SCSELLER"),
                            SCCONSIGNER = reader.GetString("SCCONSIGNER"),
                            SCVNO = reader.GetString("SCVNO"),
                            SCPOSUPPLY = reader.GetString("SCPOSUPPLY"),
                            SCNARRATION = reader.GetString("SCNARRATION"),
                            SCDATE = reader.GetString("SCDATE"),
                            SCNAMT = reader.GetDouble("SCNAMT"),
                            SCGAMT = reader.GetDouble("SCGAMT"),
                            SOREFF = reader.GetString("SOREFF"),
                            PAYMENTMODE = reader.GetString("PAYMENTMODE"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            STATENAME = reader.GetString("STATENAME"),
                            SQNO = reader.GetString("SQNO"),
                            TRANSPORTER = reader.GetString("TRANSPORTER"),
                            SCDATEconvert = reader.GetString("SCDATEconvert"),



                        });
                    }
                }
            }
            return list;

        }

        public async Task<List<GET_SChallanMaster>> getkeyword(GET_SChallanMasterDomain input)
        {
            List<GET_SChallanMaster> list = new List<GET_SChallanMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_SChallanMaster](@p0 , @p1 , @p2 , @p3 , @p4)order by  SCDATEconvert Desc";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SCAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(input.STATUSID) ?? ""); command.Parameters.Add(command_p4); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_SChallanMaster
                        {
                            SCAID = reader.GetString("SCAID"),
                            SCTRNNO = reader.GetString("SCTRNNO"),
                            SCSELLER = reader.GetString("SCSELLER"),
                            SCCONSIGNER = reader.GetString("SCCONSIGNER"),
                            SCVNO = reader.GetString("SCVNO"),
                            SCPOSUPPLY = reader.GetString("SCPOSUPPLY"),
                            SCNARRATION = reader.GetString("SCNARRATION"),
                            SCDATE = reader.GetString("SCDATE"),
                            SCNAMT = reader.GetDouble("SCNAMT"),
                            SCGAMT = reader.GetDouble("SCGAMT"),
                            SOREFF = reader.GetString("SOREFF"),
                            PAYMENTMODE = reader.GetString("PAYMENTMODE"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            STATENAME = reader.GetString("STATENAME"),
                            SQNO = reader.GetString("SQNO"),
                            TRANSPORTER = reader.GetString("TRANSPORTER"),
                            SCDATEconvert = reader.GetString("SCDATEconvert"),



                        });
                    }
                }
            }
            return list;
        }
            
    }
}
