using Microsoft.EntityFrameworkCore;
using SM.ClubManager.AccessControl.Model;

using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace SM.ClubManager.AccessControl.Database
{
    public class MainContext : DbContext
    {
        public static string dbPath
        { 
            get 
            { 
                return _dbPath; 
            } 
            set 
            { 
                _dbPath = value; 
            } 
        }

        private static string _dbPath = ".\\data.db";

        public static string ConnectionStringCache;
        // Your context has been configured to use a 'MainContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 

        //// If you wish to target a different database and/or database provider, modify the 'MainContext' 
        //// connection string in the application configuration file.
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
            string connectionString = "Data Source=" + _dbPath;
            // connect to sqlite database
            options.UseSqlite(connectionString);            
        }

        public static MainContext Create(string databasePath)
        {
            if(!string.IsNullOrEmpty(databasePath))
            {
                _dbPath = databasePath;
            }
                
            MainContext dbContext = new MainContext();
            // Uncomment the line below to start fresh with a new database.
            //dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            // string str = dbContext.Database.GetConnectionString();
            return dbContext;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //string p = Environment.SpecialFolder.CommonApplicationData
                string p = @"C:\";
            //string vspeConfigPath = Path.Combine(Environment.SpecialFolder.CommonApplicationData.ToString(), "Simply Switch Manager") ;
            string vspeConfigPath = p;

            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<SMApplicationConfigurationItem>().HasData(
            new SMApplicationConfigurationItem { Id = 1, Key = "SerialPort1Name", Value = "COM1" },                        
            new SMApplicationConfigurationItem { Id = 2, Key = "SerialPort2Name", Value = "COM2" },
            new SMApplicationConfigurationItem { Id = 3, Key = "SerialPortPairBaudRate", Value = "9600" },
            new SMApplicationConfigurationItem { Id = 4, Key = "SerialPortSimplySwitchName", Value = "COM3" },
            new SMApplicationConfigurationItem { Id = 5, Key = "SerialPortSimplySwitchBaudRate", Value = "115200" },
            new SMApplicationConfigurationItem { Id = 6, Key = "InchingDelay", Value = "0" },
            new SMApplicationConfigurationItem { Id = 7, Key = "WirelessDeviceIPAddress", Value = "192.168.1.1" },
            new SMApplicationConfigurationItem { Id = 8, Key = "WirelessDevicePort", Value = "80" },
            new SMApplicationConfigurationItem { Id = 9, Key = "IsTargetWireless", Value = "False" },
            new SMApplicationConfigurationItem { Id = 10, Key = "VSPEConfigPath", Value = vspeConfigPath },
            new SMApplicationConfigurationItem { Id = 11, Key = "isAutoConfigSimplySwitchPort", Value = "True" });



        }

        public virtual DbSet<SMApplicationConfigurationItem> ApplicationConfigurations { get; set; }

    }
}