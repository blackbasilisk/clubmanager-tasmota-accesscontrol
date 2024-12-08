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
        [InterceptorGetAttribute]
        public bool IsInvertOpenClose { get; set; }

        [InterceptorGetAttribute]
        public bool isAutoConfigSimplySwitchPort { get; set; }       

        [InterceptorGetAttribute]
        public string VSPEConfigPath { get; set; }

        [InterceptorGetAttribute]
        public string SerialPort1Name { get; set; }
 
        [InterceptorGetAttribute]
        public string SerialPort2Name { get; set; }

        [InterceptorGetAttribute]
        public int SerialPortPairBaudRate { get; set; }

        [InterceptorGetAttribute]
        public string SerialPortSimplySwitchName { get; set; }

        [InterceptorGetAttribute]
        public int SerialPortSimplySwitchBaudRate { get; set; }

        [InterceptorGetAttribute]
        public string WirelessDeviceIPAddress { get; set; }
       
        [InterceptorGetAttribute]
        public string WirelessDevicePort { get; set; }

        [InterceptorGetAttribute]
        public bool IsTargetWireless { get; set; }

        [InterceptorGetAttribute]
        public int InchingDelay { get; set; }

        [InterceptorGetAttribute]
        public bool IsServiceMode { get; set; }
        #endregion
    }
}
