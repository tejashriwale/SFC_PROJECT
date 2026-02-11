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
                commmand.CommandText = "SELECT Top 20 * FROM [GET_TyreCustomerPartners]('" + input.CAID + "' , '" + input.CTYPE + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
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
                commmand.CommandText = "SELECT * FROM [GET_TyreCustomerPartners]('" + input.CAID + "' , '" + input.CTYPE + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
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
