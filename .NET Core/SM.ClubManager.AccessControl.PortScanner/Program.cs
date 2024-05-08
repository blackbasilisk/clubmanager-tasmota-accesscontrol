using SerialPortLib;
using static System.Net.Mime.MediaTypeNames;
using System.Text;

namespace SM.ClubManager.AccessControl.PortScanner
{    
    internal class Program
    {

        string messageBuffer = "";

        static string messageToSend = "status\r\n";
        static string messageResponseExpected = "CMD: status";

        static void Main(string[] args)
        {
            Scanner portScanner = new Scanner();
            List<string> ports = new List<string>() { "COM1", "COM2", "COM3", "COM5", "COM7" };
            string port = portScanner.FindDeviceByPort(ports, messageToSend, messageResponseExpected);

            Console.WriteLine("Port found: " + port);
            Console.ReadLine();
        }



    }


}
