﻿
using SM.ClubManager.AccessControl.Database;
using SM.ClubManager.AccessControl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.AccessControl.Repository
{
    public class ApplicationConfigurationRepository : IDisposable
    {
        string dbPath = "";

        public ApplicationConfigurationRepository()
        {
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Simple Mode", "Simply Switch Manager", "data.db");

            dbPath = appDataPath;
        }

        public object GetByKey(string key)
        {
            object obj;
            using (MainContext context = MainContext.Create(dbPath))
            {
                try
                {
                    using (Repository<SMApplicationConfigurationItem> repo = new Repository<SMApplicationConfigurationItem>(context))
                    {
                        var val = repo.GetAll().FirstOrDefault(c => c.Key == key);
                        if (val == null)
                        {
                            val = new SMApplicationConfigurationItem()
                            {
                                Value = "",
                                Key = key
                            };

                            repo.InsertOrUpdate(val, true);
                        }
                            //throw new UnableToRetrieveConfigurationValueException(key);
                        repo.Reload(val);
                        obj = val.Value;
                    }


                }
                catch (Exception e)
                {
                    throw;
                }
            }


            return obj;
        }

        public void UpdateValueByKey(string key, object value)
        {
            try
            {
                using (Repository<SMApplicationConfigurationItem> repo = new Repository<SMApplicationConfigurationItem>(new MainContext()))
                {
                    var settingObj = repo.GetAll().FirstOrDefault(c => c.Key == key);
                    settingObj.Value = value.ToString();
                    repo.InsertOrUpdate(settingObj, true);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            
        }

        public void InsertOrUpdateValueByKey(string key, object value)
        {
            try
            {
                using (Repository<SMApplicationConfigurationItem> repo = new Repository<SMApplicationConfigurationItem>(new MainContext()))
                {

                    SMApplicationConfigurationItem settingObj = repo.GetAll().FirstOrDefault(c => c.Key == key) ?? new SMApplicationConfigurationItem
                    {
                        Key = key
                    };
                    settingObj.Value = value.ToString();
                    repo.InsertOrUpdate(settingObj, true);
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }


        ////public void InsertSettingg(string key, object value, Type type)
        ////{
        ////    using (Repository<ApplicationConfiguration> repo = new Repository<ApplicationConfiguration>(new MainContext()))
        ////    {
        ////        var settingObj = repo.GetAll().FirstOrDefault(c => c.Key == key);
        ////        if(settingObj != null)
        ////        {
        ////            UpdateValueByKey(key, value);
        ////            return;
        ////        }
        ////        else
        ////        {
        ////            ApplicationConfiguration configSetting = new ApplicationConfiguration();
        ////            configSetting.Key = key;
        ////            configSetting.Value = value.ToString(); 
        ////            configSettning.Type = type
        ////            settingObj.Value = value.ToString();
        ////            repo.InsertOrUpdate(settingObj, true);
        ////        }
               
        ////    }
        ////}
        public void Dispose() { }
    }

    public class InvalidConfigurationValueException : Exception
    {
        public InvalidConfigurationValueException(string configKeyName)
            : base("Invalid configuration value or invalid configuration key for " + configKeyName)
        {

        }
    }


    public class UnableToRetrieveConfigurationValueException : Exception
    {
        public UnableToRetrieveConfigurationValueException(string configKeyName)
            : base("Unable to retrieve the configuration value for " + configKeyName)
        {

        }
    }


    public class InvalidLocalInstanceConfigurationValueException : Exception
    {
        public InvalidLocalInstanceConfigurationValueException(string configKeyName)
            : base("Invalid app.config/web.config configuration key -> " + configKeyName)
        {

        }
    }
}
