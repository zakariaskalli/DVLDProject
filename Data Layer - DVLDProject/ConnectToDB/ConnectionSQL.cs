using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionToSQL
{
    static class ConnectionSQL
    {
        public static string connectionStarting = @"Server = .;
                                        DataBase = DVLD;
                                        User Id = sa;
                                       Password = 'sa123456'";
        
        //public static string connectionStarting = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
    }
}


