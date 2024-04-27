using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.AccessControl.UI.Infrastructure
{
    internal class PortUtilities
    {
        internal static List<uint> GetComPortsInUseAsNumbers()
        {            
            List<uint> comPorts = new List<uint>();

            // Get a list of all available COM ports
            string[] ports = SerialPort.GetPortNames();

            // Add each port name to the list
            foreach (string port in ports)
            {
                string name = port;
                name = name.Trim();
                name = name.ToUpper();
                name = name.Replace("COM", "");
                uint num = Convert.ToUInt16(name);
                comPorts.Add(num);
            }

            return comPorts;
        }

        internal static List<string> GetComPortsInUseAsStrings()
        {
            List<string> comPorts = new List<string>();

            // Get a list of all available COM ports
            string[] ports = SerialPort.GetPortNames();

            // Add each port name to the list
            foreach (string port in ports)
            {               
                comPorts.Add(port);
            }

            return comPorts;
        }
    }
}
