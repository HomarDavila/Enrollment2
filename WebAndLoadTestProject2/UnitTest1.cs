using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebAndLoadTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        private string cadenadeconexion => "data source=10.4.3.4;initial catalog=EnrollmentDB2;user id=EnrollmentSecurityDbUsr;password=Lb5fnfrAdlAn;";
        private TestUser GetUser()
        {
            using (var cn = new SqlConnection(cadenadeconexion))
            {
                var mydataadapter = new SqlDataAdapter("usp_getTestUser", cn);
                mydataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                var datatable = new DataTable();
                mydataadapter.Fill(datatable);
                return new TestUser()
                {
                    Id = int.Parse(datatable.Rows[0]["Id"].ToString()),
                    UserName = datatable.Rows[0]["UserName"].ToString(),
                    PasswordHash = datatable.Rows[0]["PasswordHash"].ToString(),
                    SecurityStamp = datatable.Rows[0]["SecurityStamp"].ToString(),
                };
            }
        }
    }
    public class TestUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
    }
}
