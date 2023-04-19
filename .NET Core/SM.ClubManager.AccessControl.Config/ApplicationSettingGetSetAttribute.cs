
using SM.ClubManager.AccessControl.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;
using PostSharp.Serialization;

namespace SM.ClubManager.AccessControl.Config
{
    [PSerializable]
    public class ApplicationSettingGetSetAttribute : LocationInterceptionAspect
    {
        public sealed override void OnGetValue(LocationInterceptionArgs args)
        {
            base.OnGetValue(args);

            //first we read from db
            var key = args.LocationName;

            try
            {
                using (var appConfigRepo = new ApplicationConfigurationRepository())
                {
                    var f = args.Location.LocationType;
                    switch (f.ToString())
                    {
                        case "System.String":
                            var vString = appConfigRepo.GetByKey(key);
                            args.Value = vString != null ? vString.ToString() : default(string);
                            break;
                        case "System.Boolean":
                            args.Value = Convert.ToBoolean(appConfigRepo.GetByKey(key));
                            break;
                        case "System.Int32":
                            var vInt = appConfigRepo.GetByKey(key);
                            args.Value = vInt != null ? Convert.ToInt32(appConfigRepo.GetByKey(key)) : default(int);
                            break;
                        default:
                            throw new NotImplementedException("Configuration argument type parser not implemented");
                    }
                }
            }
            catch (Exception e)
            {
                throw new UnableToRetrieveConfigurationValueException(key);
            }
        }

        public sealed override void OnSetValue(LocationInterceptionArgs args)
        {
            //first we write to db
            base.OnSetValue(args);

            var key = args.LocationName;

            try
            {
                using (var appConfigRepo = new ApplicationConfigurationRepository())
                {
                    var val = appConfigRepo.GetByKey(key);                        
                        val = args.Value.ToString();
                        appConfigRepo.InsertOrUpdateValueByKey(key, val);                                      
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        
        ////private Setting CreateSetting(LocationInterceptionArgs args)
        ////{
        ////    string key = args.LocationName;
        ////    //value
        ////    Type type = null;
        ////    object value = args.Value;

        ////    using (var appConfigRepo = new ApplicationConfigurationRepository())
        ////    {
        ////        var f = args.Location.LocationType;
        ////        switch (f.ToString())
        ////        {
        ////            case "System.String":
        ////                type = typeof(String);
        ////                break;
        ////            case "System.Boolean":
        ////                type = typeof(bool);
        ////                break;
        ////            case "System.Int32":
        ////                type = typeof(int);
        ////                break;
        ////            default:
        ////                throw new NotImplementedException("Configuration argument type parser not implemented");
        ////        }
        ////    }
        ////    Setting setting = new Setting { Name = key, Type = type, Value = value };
        ////    return setting;
        ////}
 
    }
}
