using System.Data.Entity;

namespace SM.ClubManager.AccessControl.Database
{

    public class DatabaseInitializer : CreateDatabaseIfNotExists<MainContext>
    {
        protected override void Seed(MainContext context)
        {
           
            //context.User.AddOrUpdate(
            //user => user.Username,
            //new User { Username = "Admin", Password = "admin", FirstLast = "Administrator", LastName = "Admin", RoleId = 1 });
            base.Seed(context);
        }

        //public void InitializeDatabase(DataContext context)
        //{
        //    if (!context.Database.Exists())
        //    {
        //        if (!context.Database.CompatibleWithModel(true))
        //        {
        //            context.Database.Delete();
        //        }
        //    }
        //}
        
    }
}
