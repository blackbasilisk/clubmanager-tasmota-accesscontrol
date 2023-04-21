using SM.ClubManager.AccessControl.SDK;

namespace SDK_Sample
{
    internal class Program
    {
        static void Main(string[] args)
        {            
            SimplySwitch simplySwitch = new SimplySwitch();
            simplySwitch.SerialPort = "COM5";
            simplySwitch.SerialBaudRate = 115200;

            simplySwitch.SConnect();
            simplySwitch.SOpen();
        }
    }
}