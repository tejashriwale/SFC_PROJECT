using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_WarrantyRepository : IGET_WarrantyRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_WarrantyRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_Warranty>> get(GET_WarrantyDomain input)
        {
            List<GET_Warranty> list = new List<GET_Warranty> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand ())
            {
                command.CommandText = "SELECT * FROM [GET_Warranty]('" + input.WARTID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_Warranty
                        {
                            WARTID = reader.GetString("WARTID"),
                            PNAME = reader.GetString("PNAME"),
                            WARTDURATION = reader.GetString("WARTDURATION"),
                            WARTPERIODS = reader.GetString("WARTPERIODS"),
                            WARTDESCRIPTION = reader.GetString("WARTDESCRIPTION"),
                            WARTSTATUS = reader.GetString("WARTSTATUS"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_Warranty>> getasync(GET_WarrantyDomain input)
        {
            List<GET_Warranty> list = new List<GET_Warranty>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_Warranty]('" + input.WARTID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_Warranty
                        {
                            WARTID = reader.GetString("WARTID"),
                            PNAME = reader.GetString("PNAME"),
                            WARTDURATION = reader.GetString("WARTDURATION"),
                            WARTPERIODS = reader.GetString("WARTPERIODS"),
                            WARTDESCRIPTION = reader.GetString("WARTDESCRIPTION"),
                            WARTSTATUS = reader.GetString("WARTSTATUS"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
