using System;
using System.Configuration;
using System.IO;

namespace Linq2Sql
{
    public class Linq2SqlContextFactory
    {
        private static string _connectionString;
        public static Linq2SqlDataContext CreateContext()
        {
            if (_connectionString == null)
            {
                var c = ConfigurationManager.ConnectionStrings["CrutchContext"];
                if (c == null)
                {
                    throw new ConfigurationErrorsException();
                }
                var parentDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent;
                if (parentDirectory == null)
                {
                    throw new DirectoryNotFoundException();
                }
                _connectionString = c.ConnectionString.Replace("|DataDirectory|", parentDirectory.FullName);
            }
            return new Linq2SqlDataContext(_connectionString);
        } 
    }
}