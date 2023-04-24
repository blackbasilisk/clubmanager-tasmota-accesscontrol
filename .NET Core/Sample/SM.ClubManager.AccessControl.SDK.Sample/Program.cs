namespace SM.ClubManager.AccessControl.SDK.Sample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create new instance of SimplySwitch");
            SimplySwitch sw = new SimplySwitch();
            sw.SerialBaudRate = 115200;
            sw.SerialPort = "COM5";

            sw.OnConnected += Sw_OnConnected;
            sw.OnDisconnected += Sw_OnDisconnected;
            sw.OnLogMessage += Sw_OnLogMessage;          

            sw.SConnect();
            Console.WriteLine("Activating the unit (closing relay)");            
            sw.SActivate();
            Console.WriteLine("Pausing 2 seconds...");
            Thread.Sleep(2000);
            Console.WriteLine("Deactivating the unit (opening relay)");            
            sw.SRelease();
            Console.WriteLine("Disconnecting... ");
            sw.SDisconnect();
            Console.WriteLine("Press any key to dispose object and exit");
            Console.ReadLine();
            sw.Dispose();
        }

        private static void Sw_OnLogMessage(object? sender, SSLogMessage e)
        {
            if(e.IsDebug)
            {
                Console.WriteLine("[DEBUG] " + e.Message);
            }
            else
            {
                Console.WriteLine("[INFO] " + e.Message);
            }           
        }

        private static void Sw_OnDisconnected(object? sender, EventArgs e)
        {
            Console.WriteLine("SimplySwitch disconnected");
        }

        private static void Sw_OnConnected(object? sender, EventArgs e)
        {
            Console.WriteLine("SimplySwitch connected");
        }
    }
}