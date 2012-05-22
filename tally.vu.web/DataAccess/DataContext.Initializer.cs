using System.Data.Entity;

namespace tallyvu.DataAccess
{
    public partial class DataContext
    {
        public class Initializer : IDatabaseInitializer<DataContext>
        {
            private readonly IDatabaseInitializer<DataContext> _initializer;

            public Initializer(IDatabaseInitializer<DataContext> initializer)
            {
                _initializer = initializer;
            }

            public void InitializeDatabase(DataContext context)
            {
                _initializer.InitializeDatabase(context);

                Seed(context);
                context.SaveChanges();
            }

            protected virtual void Seed(DataContext context)
            {
            }
        }
    }
}
