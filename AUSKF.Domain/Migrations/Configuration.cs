namespace AUSKF.Domain.Migrations
{
    using Data;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<AUSKF.Domain.Data.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("System.Data.SqlClient", new CustomSqlServerMigrationSqlGenerator());
        }

        protected override void Seed(AUSKF.Domain.Data.DataContext context)
        {
            new EntityContextInitializer().InitializeDatabase(context);
        }
    }
}
