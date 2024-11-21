using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw13.Configuration
{
    public static class Configuration
    {
        public static string ConnectionString { get; set; }
        static Configuration()
        {
            ConnectionString = @"Data Source=DESKTOP-78B19T2\SQLEXPRESSSQLEXPRESS;Initial Catalog=Hw13;User ID=SA;Password=123456;TrustServerCertificate=True;";
        }
    }
}
