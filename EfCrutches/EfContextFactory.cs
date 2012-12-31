using System;
using System.Configuration;
using System.Data.Entity;
using System.IO;

namespace EfCrutches
{
    class EfContextFactory
    {
        private static string _connectionString;
        public static CrutchContext CreateContext()
        {
            if (_connectionString == null)
            {
                var c = ConfigurationManager.ConnectionStrings["CrutchContext"];

                if (c == null)
                    throw new ConfigurationErrorsException();

                var parent = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent;

                if (parent == null)
                    throw new DirectoryNotFoundException();

                _connectionString = c.ConnectionString.Replace("|DataDirectory|", parent.FullName);
            }
            return new CrutchContext(_connectionString);
        }

        public static void InitializeDatabase(IDatabaseInitializer<CrutchContext> databaseIntializer)
        {
            Database.SetInitializer(databaseIntializer);
            using (var context = CreateContext())
            {
                context.Database.Initialize(true);
            }
        }
    }
}