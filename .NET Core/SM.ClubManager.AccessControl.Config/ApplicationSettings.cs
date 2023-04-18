namespace SM.ClubManager.AccessControl.Config
{
    public class ApplicationSettings
    {
        #region Events

        #endregion

        #region Properties
        private static ApplicationSettings instance;

        public static ApplicationSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ApplicationSettings();
                }
                return instance;
            }
        }
        #endregion

        #region CTor
        private ApplicationSettings()
        {

        }
        #endregion

        #region Public methods


        #endregion

        #region Database properties    

        [ApplicationSettingGetSet]
        public string SerialInPort { get; set; }

        [ApplicationSettingGetSet]
        public int SerialInBaudRate { get; set; }

        [ApplicationSettingGetSet]
        public string SerialOutPort { get; set; }

        [ApplicationSettingGetSet]
        public int SerialOutBaudRate { get; set; }

        [ApplicationSettingGetSet]
        public string WirelessDeviceIPAddress { get; set; }
       
        [ApplicationSettingGetSet]
        public string WirelessDevicePort { get; set; }

        [ApplicationSettingGetSet]
        public bool IsTargetWireless { get; set; }

        [ApplicationSettingGetSet]
        public int InchingDelay { get; set; }

        [ApplicationSettingGetSet]
        public bool IsServiceMode { get; set; }
        #endregion
    }
}
