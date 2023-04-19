using Microsoft.EntityFrameworkCore;
using SM.ClubManager.AccessControl.Model;
using System.ComponentModel;
using System.Configuration;
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
        //public MainContext() : base("name=MainContext")
        //{
        //    //Database.SetInitializer(new MigrateDatabaseToLatestVersion<MainContext, SM.ClubManager.AccessControl.Database.Migrations.Configuration>());
        //    System.Data.Entity.Database.SetInitializer(new CreateDatabaseIfNotExists<MainContext>());
            
        //    //System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseAlways<MainContext>());
        //    // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MainContext>());
        //    //Database.SetInitializer<MainContext>(new MigrateDatabaseToLatestVersion<MainContext, SM.ClubManager.AccessControl.Database.Migrations.Configuration>());
        //    //            System.Data.Entity.Database.SetInitializer<MainContext>(null);
        //    //                        
        //}


       //// protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       //////=> optionsBuilder.UseSqlite("Data Source=data.db");
    

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            options.UseSqlite("Data Source=.\\data.db");
        }

        public static MainContext Create()
        {

             MainContext dbContext = new MainContext();
            // Uncomment the line below to start fresh with a new database.
            //dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

           // string str = dbContext.Database.GetConnectionString();
            return dbContext;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<SMApplicationConfigurationItem>().HasData(
            new SMApplicationConfigurationItem { Id = 1, Key = "SerialInPort", Value = "COM1" },
            new SMApplicationConfigurationItem { Id = 2, Key = "SerialInBaudRate", Value = "9600" },
            new SMApplicationConfigurationItem { Id = 3, Key = "InchingDelay", Value = "0" },
            new SMApplicationConfigurationItem { Id = 4, Key = "SerialOutPort", Value = "COM3" },
            new SMApplicationConfigurationItem { Id = 5, Key = "SerialOutBaudRate", Value = "115200" },
            new SMApplicationConfigurationItem { Id = 6, Key = "WirelessDeviceIPAddress", Value = "192.168.1.1" },
            new SMApplicationConfigurationItem { Id = 7, Key = "WirelessDevicePort", Value = "80" },
            new SMApplicationConfigurationItem { Id = 8, Key = "IsTargetWireless", Value = "False" });            
        }

        public virtual DbSet<SMApplicationConfigurationItem> ApplicationConfigurations { get; set; }

    }
}