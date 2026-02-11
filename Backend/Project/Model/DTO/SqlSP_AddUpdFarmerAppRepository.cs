using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Repository;

namespace Project.Model.DTO
{
    public class SqlSP_AddUpdFarmerAppRepository : ISP_AddUpdFarmerAppRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddUpdFarmerAppRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<SP_AddUpdFarmerApp>> getasync(SP_AddUpdFarmerAppDomain input)
        {
            List<SP_AddUpdFarmerApp> list = new List<SP_AddUpdFarmerApp> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[SP_AddUpdFarmerApp]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@FAID";
                parameter.Value = input.FAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@FNAME";
                parameter.Value = input.FNAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@FCONTACTNO";
                parameter.Value = input.FCONTACTNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@FACCONAME";
                parameter.Value = input.FACCONAME;
                command.Parameters.Add(parameter);


                parameter = command.CreateParameter();
                parameter.ParameterName = "@FBIRTHDATE";
                parameter.Value = input.FBIRTHDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@FPANNO";
                parameter.Value = input.FPANNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@FADDHARNO";
                parameter.Value = input.FADDHARNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@FBANKNAME";
                parameter.Value = input.FBANKNAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@FBRANCHNAME";
                parameter.Value = input.FBRANCHNAME;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@FACCOUNTNO";
                parameter.Value = input.FACCOUNTNO;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@FIFSCCODE";
                parameter.Value = input.FIFSCCODE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@FPINCODE";
                parameter.Value = input.FPINCODE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@FSTATE";
                parameter.Value = input.FSTATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@FCITY";
                parameter.Value = input.FCITY;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@FCROPAREA";
                parameter.Value = input.FCROPAREA;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@FCROPTYPE";
                parameter.Value = input.FCROPTYPE;
                command.Parameters.Add(parameter);



                parameter = command.CreateParameter();
                parameter.ParameterName = "@FSTATUS";
                parameter.Value = input.FSTATUS;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@COMPANYID";
                parameter.Value = input.COMPANYID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DEPTID";
                parameter.Value = input.DEPTID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@FAddress";
                parameter.Value = input.FAddress;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UID";
                parameter.Value = input.UID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@FCMTOKEN";
                parameter.Value = input.FCMTOKEN;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@REFID";
                parameter.Value = input.REFID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@UAID";
                parameter.Value = input.UAID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@FPROFILE";
                parameter.Value = input.FPROFILE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DATE";
                parameter.Value = input.DATE;
                command.Parameters.Add(parameter);

                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new SP_AddUpdFarmerApp
                        {
                            FAID = reader.GetString("FAID"),
                            FNAME = reader.GetString("FNAME"),
                            FADDHARNO = reader.GetString("FADDHARNO"),
                            COMPANYID = reader.GetString("COMPANYID"),
                            DEPTID = reader.GetString("DEPTID"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
