using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SM.ClubManager.Library.Base.Infrastructure
{    
    //Have to have lowercase first letter so that when we conver to string it will allow the section in theconfig to be refreshed
    //bcause its case sensitive
    public enum ConfigurationSection
    {
        appSettings = 1,
        connectionStrings = 2
    }

    public enum DataGridViewColumnType 
    {
        TextBox,
        CheckBox,
        Number,
        ComboBox
    }

   

    public enum FormMode 
    {
        ReadOnly = 1,
        ReadWrite = 2
    }

    public enum DatabaseType
    {
        SQLIte,
        MSSQL
    }

    public enum ErrorLevel
    {        
        Information = 1,
        Warning = 2,
        Critical = 3
    }
}
