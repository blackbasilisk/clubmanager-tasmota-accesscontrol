
    using SM.ClubManager.AccessControl.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

namespace SIS.REA.Actinium.Database.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SM.ClubManager.AccessControl.Database.MainContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SM.ClubManager.AccessControl.Database.MainContext context)
        {
          
            List<ApplicationConfiguration> appSettings = new List<ApplicationConfiguration>();
            appSettings.Add(new ApplicationConfiguration() { Id = 1, Key = "SerialInPort", Value = "COM9" });
            appSettings.Add(new ApplicationConfiguration() { Id = 2, Key = "SerialOutPort", Value = "COM3" });            
            appSettings.Add(new ApplicationConfiguration() { Id = 3, Key = "WirelessDeviceIPAddress", Value = "" });
            appSettings.Add(new ApplicationConfiguration() { Id = 4, Key = "WirelessDevicePort", Value = "80" });
            appSettings.Add(new ApplicationConfiguration() { Id = 5, Key = "IsTargetWireless", Value = "False" });

            context.ApplicationConfigurations.AddOrUpdate(x => x.Id, appSettings.ToArray());
            context.SaveChanges();
        }
    }
}
