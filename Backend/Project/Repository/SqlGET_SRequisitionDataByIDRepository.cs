using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_SRequisitionDataByIDRepository : IGET_SRequisitionDataByIDRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SRequisitionDataByIDRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SRequisitionDataByID>> getsync(GET_SRequisitionDataByIDDomain input)
        {
            List<GET_SRequisitionDataByID> list = new List<GET_SRequisitionDataByID> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand ())
            {
                command.CommandText = "SELECT * FROM [GET_SRequisitionDataByID]('" + input.SRAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync ())
                {
                    while(await reader.ReadAsync ())
                    {
                        list.Add(new GET_SRequisitionDataByID
                        {
                            SRNO = reader.GetString("SRNO"),
                            SRVANDORID = reader.GetString("SRVANDORID"),
                            SRDPRODUCT = reader.GetString("SRDPRODUCT"),
                            SRDQUANTITY = reader.GetString("SRDQUANTITY"),
                            SRDUOM = reader.GetString("SRDUOM"),
                            UOMTITLE = reader.GetString("UOMTITLE"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            CONTACTNUMBER = reader.GetString("CONTACTNUMBER"),
                            EMAIL = reader.GetString("EMAIL"),
                            CSTATE = reader.GetString("CSTATE"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
