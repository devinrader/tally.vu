using System.Configuration;
using System.Data.Entity;

using tallyvu.DataAccess;
using tallyvu.Website.App_Start;

[assembly: WebActivator.PreApplicationStartMethod(typeof(DataAccess), "InitializeDatabase")]

namespace tallyvu.Website.App_Start
{
    public static class DataAccess
    {
        static bool IsAppHarbor
        {
            get { return !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["appharbor.commit_id"]); }
        }

        public static void InitializeDatabase()
        {
            IDatabaseInitializer<DataContext> initializer;

            //if (IsAppHarbor)
                initializer = new MigrateDatabaseToLatestVersion<DataContext, tallyvu.Migrations.Configuration>();
            //else
            //    initializer = new DropCreateDatabaseAlways<DataContext>();

            Database.SetInitializer(new DataContext.Initializer(initializer));

            using (var context = new DataContext())
            {
                context.Database.Initialize(false);
            }
        }
    }
}
