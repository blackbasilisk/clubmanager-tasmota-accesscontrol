using SM.ClubManager.AccessControl.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace SM.ClubManager.AccessControl.Database
{
    public class MainContext : DbContext
    {

        public static string ConnectionStringCache;
        // Your context has been configured to use a 'MainContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
       
        // If you wish to target a different database and/or database provider, modify the 'MainContext' 
        // connection string in the application configuration file.
        public MainContext() : base("name=MainContext")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<MainContext, SM.ClubManager.AccessControl.Database.Migrations.Configuration>());
            System.Data.Entity.Database.SetInitializer(new CreateDatabaseIfNotExists<MainContext>());
            //System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseAlways<MainContext>());
            // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MainContext>());
            //Database.SetInitializer<MainContext>(new MigrateDatabaseToLatestVersion<MainContext, SM.ClubManager.AccessControl.Database.Migrations.Configuration>());
//            System.Data.Entity.Database.SetInitializer<MainContext>(null);
        }

        public static MainContext Create()
        {
            return new MainContext();
        }     

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();                       
        }

     
        public virtual DbSet<ApplicationConfiguration> ApplicationConfigurations { get; set; }


    }
}