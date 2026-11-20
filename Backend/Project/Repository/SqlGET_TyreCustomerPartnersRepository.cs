using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_TyreCustomerPartnersRepository : IGET_TyreCustomerPartnersRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_TyreCustomerPartnersRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_TyreCustomerPartners>> getasync(GET_TyreCustomerPartnersDomain input)
        {
            List<GET_TyreCustomerPartners> list = new List<GET_TyreCustomerPartners>();
            using (var commmand = dbContext.Database.GetDbConnection().CreateCommand())
            {
                commmand.CommandText = "SELECT Top 20 * FROM [GET_TyreCustomerPartners](@p0 , @p1 , @p2 , @p3 , @p4)";
                { var commmand_p0 = commmand.CreateParameter(); commmand_p0.ParameterName = "@p0"; commmand_p0.Value = (object)(Convert.ToString(input.CAID) ?? ""); commmand.Parameters.Add(commmand_p0); }
                { var commmand_p1 = commmand.CreateParameter(); commmand_p1.ParameterName = "@p1"; commmand_p1.Value = (object)(Convert.ToString(input.CTYPE) ?? ""); commmand.Parameters.Add(commmand_p1); }
                { var commmand_p2 = commmand.CreateParameter(); commmand_p2.ParameterName = "@p2"; commmand_p2.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); commmand.Parameters.Add(commmand_p2); }
                { var commmand_p3 = commmand.CreateParameter(); commmand_p3.ParameterName = "@p3"; commmand_p3.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); commmand.Parameters.Add(commmand_p3); }
                { var commmand_p4 = commmand.CreateParameter(); commmand_p4.ParameterName = "@p4"; commmand_p4.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); commmand.Parameters.Add(commmand_p4); }
                dbContext.Database.OpenConnection();

                using (var reader = await commmand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_TyreCustomerPartners
                        {
                            CAID = reader.GetString("CAID"),
                            CTYPE = reader.GetString("CTYPE"),
                            CADDRESS = reader.GetString("CADDRESS"),
                            CCOMPANYNAME = reader.GetString("CCOMPANYNAME"),
                            CCONTACTPERSONMOBILE = reader.GetString("CCONTACTPERSONMOBILE"),
                            CEMAIL = reader.GetString("CEMAIL"),
                            CPINCODE = reader.GetString("CPINCODE"),
                            CCONTACTPERSONNAME = reader.GetString("CCONTACTPERSONNAME"),
                            CTRADEADDRESS = reader.GetString("CTRADEADDRESS")
                        });
                    }
                }
                return list;
            }
        }

        public async Task<List<GET_TyreCustomerPartners>> getkeyword(GET_TyreCustomerPartnersDomain input)
        {
            List<GET_TyreCustomerPartners> list = new List<GET_TyreCustomerPartners>();
            using (var commmand = dbContext.Database.GetDbConnection().CreateCommand())
            {
                commmand.CommandText = "SELECT * FROM [GET_TyreCustomerPartners](@p0 , @p1 , @p2 , @p3 , @p4)";
                { var commmand_p0 = commmand.CreateParameter(); commmand_p0.ParameterName = "@p0"; commmand_p0.Value = (object)(Convert.ToString(input.CAID) ?? ""); commmand.Parameters.Add(commmand_p0); }
                { var commmand_p1 = commmand.CreateParameter(); commmand_p1.ParameterName = "@p1"; commmand_p1.Value = (object)(Convert.ToString(input.CTYPE) ?? ""); commmand.Parameters.Add(commmand_p1); }
                { var commmand_p2 = commmand.CreateParameter(); commmand_p2.ParameterName = "@p2"; commmand_p2.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); commmand.Parameters.Add(commmand_p2); }
                { var commmand_p3 = commmand.CreateParameter(); commmand_p3.ParameterName = "@p3"; commmand_p3.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); commmand.Parameters.Add(commmand_p3); }
                { var commmand_p4 = commmand.CreateParameter(); commmand_p4.ParameterName = "@p4"; commmand_p4.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); commmand.Parameters.Add(commmand_p4); }
                dbContext.Database.OpenConnection();

                using (var reader = await commmand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_TyreCustomerPartners
                        {
                            CAID = reader.GetString("CAID"),
                            CTYPE = reader.GetString("CTYPE"),
                            CADDRESS = reader.GetString("CADDRESS"),
                            CCOMPANYNAME = reader.GetString("CCOMPANYNAME"),
                            CCONTACTPERSONMOBILE = reader.GetString("CCONTACTPERSONMOBILE"),
                            CEMAIL = reader.GetString("CEMAIL"),
                            CPINCODE = reader.GetString("CPINCODE"),
                            CCONTACTPERSONNAME = reader.GetString("CCONTACTPERSONNAME"),
                            CTRADEADDRESS = reader.GetString("CTRADEADDRESS")
                        });
                    }
                }
                return list;
            }
        }
    }
}
