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
                command.CommandText = "SELECT Top 20 * FROM [GET_SChallanMaster]('" + input.SCAID + "' , '"+input.KEYWORD+"' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' , '" + input.STATUSID + "')order by  SCDATEconvert Desc";
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
                command.CommandText = "SELECT  * FROM [GET_SChallanMaster]('" + input.SCAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' , '" + input.STATUSID + "')order by  SCDATEconvert Desc";
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
