using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_CompanyInfoRepository : IGET_CompanyInfoRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CompanyInfoRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CompanyInfo>> getasync(GET_CompanyInfoDomain input)
        {
            List<GET_CompanyInfo> list = new List<GET_CompanyInfo>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_CompanyInfo](@p0 , @p1)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.CINFOID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CompanyInfo
                        {
                            CINFOID		 = reader.GetString("CINFOID"),
                            COMPANYID	 = reader.GetString("COMPANYID"),
                            CNAME		 = reader.GetString("CNAME"),
                            CTELEPHONE	 = reader.GetString("CTELEPHONE"),
                            CEMAIL		 = reader.GetString("CEMAIL"),
                            CCONTACT	 = reader.GetString("CCONTACT"),
                            CPAN		 = reader.GetString("CPAN"),
                            CGSTNO		 = reader.GetString("CGSTNO"),
                           
                            CADDRESS	 = reader.GetString("CADDRESS"),
                           
                            INSTAGRAMID	 = reader.GetString("INSTAGRAMID"),
                            TWITTERID	 = reader.GetString("TWITTERID"),
                            FACEBOOKID = reader.GetString("FACEBOOKID"),
                            

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_CompanyInfo>> getkeyword(GET_CompanyInfoDomain input)
        {
            List<GET_CompanyInfo> list = new List<GET_CompanyInfo>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CompanyInfo](@p0 , @p1)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.CINFOID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CompanyInfo
                        {
                            CINFOID = reader.GetString("CINFOID"),
                            COMPANYID = reader.GetString("COMPANYID"),
                            CNAME = reader.GetString("CNAME"),
                            CTELEPHONE = reader.GetString("CTELEPHONE"),
                            CEMAIL = reader.GetString("CEMAIL"),
                            CCONTACT = reader.GetString("CCONTACT"),
                            CPAN = reader.GetString("CPAN"),
                            CGSTNO = reader.GetString("CGSTNO"),
                           
                            CADDRESS = reader.GetString("CADDRESS"),
                           
                            INSTAGRAMID = reader.GetString("INSTAGRAMID"),
                            TWITTERID = reader.GetString("TWITTERID"),
                            FACEBOOKID = reader.GetString("FACEBOOKID"),


                        });
                    }
                }
            }
            return list;
        }
    }
}
