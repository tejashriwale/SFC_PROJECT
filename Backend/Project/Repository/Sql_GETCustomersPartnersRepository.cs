using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class Sql_GETCustomersPartnersRepository : IGET_CustomersPartnersRepository
    {
        private readonly ApplicationDbContext dbContext;
        public Sql_GETCustomersPartnersRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<_GET_CustomersPartners_>> getasync(CustomerDomain input)
        {
            List<_GET_CustomersPartners_> list =new List<_GET_CustomersPartners_>();

            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select Top 20 * from [GET_CustomersPartners]('%' ,'%' , '%', @p0,@p1)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {

                    while(await reader.ReadAsync())
                    {
                        list.Add(new _GET_CustomersPartners_
                        {
                            CAID = reader.GetString("CAID"),
                            CCOMPANYNAME = reader.GetString("CCOMPANYNAME"),
                            CTYPE = reader.GetString("CTYPE"),
                            CAREA = reader.GetString("CAREA"),
                            CLANDMARK = reader.GetString("CLANDMARK"),
                            CADDRESS = reader.GetString("CADDRESS"),
                            CCITY = reader.GetString("CCITY"),
                            CPINCODE = reader.GetString("CPINCODE"),
                            CTELEPHONENUMBER = reader.GetString("CTELEPHONENUMBER"),
                            CEMAIL = reader.GetString("CEMAIL"),
                            CWEBSITEURL = reader.GetString("CWEBSITEURL"),
                            CCONTACTPERSONNAME = reader.GetString("CCONTACTPERSONNAME"),
                            CCONTACTPERSONMOBILE = reader.GetString("CCONTACTPERSONMOBILE"),
                            CDESIGNATION = reader.GetString("CDESIGNATION"),
                            CGSTIN = reader.GetString("CGSTIN"),
                            CTRADENAME = reader.GetString("CTRADENAME"),
                            CTRADEADDRESS = reader.GetString("CTRADEADDRESS"),
                            CTRADECITY = reader.GetString("CTRADECITY"),
                            CTRADEPINCODE = reader.GetString("CTRADEPINCODE"),
                            CSTATE = reader.GetString("CSTATE"),
                            CDISTANCE = reader.GetString("CDISTANCE"),
                            CPAN = reader.GetString("CPAN"),
                            CBUSINESSTYPE = reader.GetString("CBUSINESSTYPE"),
                            CSTATUS = reader.GetString("CSTATUS"),
                            CIFSC = reader.GetString("CIFSC"),
                            CACCOUNTNAME = reader.GetString("CACCOUNTNAME"),
                            CACCOUNTNUMBER = reader.GetString("CACCOUNTNUMBER"),
                            CBANKNAME = reader.GetString("CBANKNAME"),
                            CBRANCHNAME = reader.GetString("CBRANCHNAME"),
                            CCATEGORY = reader.GetString("CCATEGORY"),
                            CTRADELANDMARK = reader.GetString("CTRADELANDMARK"),
                            CTRADEAREA = reader.GetString("CTRADEAREA"),
                            SCITY = reader.GetString("SCITY"),
                            SSTATE = reader.GetString("SSTATE"),
                            BUSINESSTYPE = reader.GetString("BUSINESSTYPE"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<_GET_CustomersPartners_>> GetByGroupAsync(CustomerDomain input)
        {
            List<_GET_CustomersPartners_> list = new List<_GET_CustomersPartners_>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select Top 20 * from [GET_CustomersPartners](@p0 ,@p1 , @p2, @p3,@p4)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.CTYPE) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p4); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_CustomersPartners_
                        {
                            CAID = reader.GetString("CAID"),
                            CCOMPANYNAME = reader.GetString("CCOMPANYNAME"),
                            CTYPE = reader.GetString("CTYPE"),
                            CAREA = reader.GetString("CAREA"),
                            CLANDMARK = reader.GetString("CLANDMARK"),
                            CADDRESS = reader.GetString("CADDRESS"),
                            CCITY = reader.GetString("CCITY"),
                            CPINCODE = reader.GetString("CPINCODE"),
                            CTELEPHONENUMBER = reader.GetString("CTELEPHONENUMBER"),
                            CEMAIL = reader.GetString("CEMAIL"),
                            CWEBSITEURL = reader.GetString("CWEBSITEURL"),
                            CCONTACTPERSONNAME = reader.GetString("CCONTACTPERSONNAME"),
                            CCONTACTPERSONMOBILE = reader.GetString("CCONTACTPERSONMOBILE"),
                            CDESIGNATION = reader.GetString("CDESIGNATION"),
                            CGSTIN = reader.GetString("CGSTIN"),
                            CTRADENAME = reader.GetString("CTRADENAME"),
                            CTRADEADDRESS = reader.GetString("CTRADEADDRESS"),
                            CTRADECITY = reader.GetString("CTRADECITY"),
                            CTRADEPINCODE = reader.GetString("CTRADEPINCODE"),
                            CSTATE = reader.GetString("CSTATE"),
                            CDISTANCE = reader.GetString("CDISTANCE"),
                            CPAN = reader.GetString("CPAN"),
                            CBUSINESSTYPE = reader.GetString("CBUSINESSTYPE"),
                            CSTATUS = reader.GetString("CSTATUS"),
                            CIFSC = reader.GetString("CIFSC"),
                            CACCOUNTNAME = reader.GetString("CACCOUNTNAME"),
                            CACCOUNTNUMBER = reader.GetString("CACCOUNTNUMBER"),
                            CBANKNAME = reader.GetString("CBANKNAME"),
                            CBRANCHNAME = reader.GetString("CBRANCHNAME"),
                            CCATEGORY = reader.GetString("CCATEGORY"),
                            CTRADELANDMARK = reader.GetString("CTRADELANDMARK"),
                            CTRADEAREA = reader.GetString("CTRADEAREA"),
                            SCITY = reader.GetString("SCITY"),
                            SSTATE = reader.GetString("SSTATE"),
                            BUSINESSTYPE = reader.GetString("BUSINESSTYPE"),
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<_GET_CustomersPartners_>> searchasync(CustomerDomain input)
        {
            List<_GET_CustomersPartners_> list = new List<_GET_CustomersPartners_>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select * from [GET_CustomersPartners](@p0 ,@p1 , @p2, @p3,@p4)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.CTYPE) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p4); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_CustomersPartners_
                        {
                            CAID = reader.GetString("CAID"),
                            CCOMPANYNAME = reader.GetString("CCOMPANYNAME"),
                            CTYPE = reader.GetString("CTYPE"),
                            CAREA = reader.GetString("CAREA"),
                            CLANDMARK = reader.GetString("CLANDMARK"),
                            CADDRESS = reader.GetString("CADDRESS"),
                            CCITY = reader.GetString("CCITY"),
                            CPINCODE = reader.GetString("CPINCODE"),
                            CTELEPHONENUMBER = reader.GetString("CTELEPHONENUMBER"),
                            CEMAIL = reader.GetString("CEMAIL"),
                            CWEBSITEURL = reader.GetString("CWEBSITEURL"),
                            CCONTACTPERSONNAME = reader.GetString("CCONTACTPERSONNAME"),
                            CCONTACTPERSONMOBILE = reader.GetString("CCONTACTPERSONMOBILE"),
                            CDESIGNATION = reader.GetString("CDESIGNATION"),
                            CGSTIN = reader.GetString("CGSTIN"),
                            CTRADENAME = reader.GetString("CTRADENAME"),
                            CTRADEADDRESS = reader.GetString("CTRADEADDRESS"),
                            CTRADECITY = reader.GetString("CTRADECITY"),
                            CTRADEPINCODE = reader.GetString("CTRADEPINCODE"),
                            CSTATE = reader.GetString("CSTATE"),
                            CDISTANCE = reader.GetString("CDISTANCE"),
                            CPAN = reader.GetString("CPAN"),
                            CBUSINESSTYPE = reader.GetString("CBUSINESSTYPE"),
                            CSTATUS = reader.GetString("CSTATUS"),
                            CIFSC = reader.GetString("CIFSC"),
                            CACCOUNTNAME = reader.GetString("CACCOUNTNAME"),
                            CACCOUNTNUMBER = reader.GetString("CACCOUNTNUMBER"),
                            CBANKNAME = reader.GetString("CBANKNAME"),
                            CBRANCHNAME = reader.GetString("CBRANCHNAME"),
                            CCATEGORY = reader.GetString("CCATEGORY"),
                            CTRADELANDMARK = reader.GetString("CTRADELANDMARK"),
                            CTRADEAREA = reader.GetString("CTRADEAREA"),
                            SCITY = reader.GetString("SCITY"),
                            SSTATE = reader.GetString("SSTATE"),
                            BUSINESSTYPE = reader.GetString("BUSINESSTYPE"),
                        });
                    }

                }

            }
            return list;
        }
    }
}
