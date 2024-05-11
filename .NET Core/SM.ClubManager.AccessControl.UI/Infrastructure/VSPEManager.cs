using SM.ClubManager.AccessControl.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.AccessControl.UI.Infrastructure
{
    internal class VSPEManager
    {
        #region Internal Methods
        internal void StartVSPE(bool isKillProcess = false)
        {
            string processName = "VSPEmulator";
            //C:\Source\virtual-serial-ports-lib-test\config.vspe
            // Path to the executable
            string executablePath = @"C:\Program Files (x86)\Eterlogic.com\Virtual Serial Ports Emulator (x32)\VSPEmulator.exe";

            string path = ApplicationSettings.Instance.VSPEConfigPath;
            string fullPath = Path.Combine(path, "config.vspe");
            // Parameter to pass to the executable
            string p1 = fullPath;//@"C:\Source\virtual-serial-ports-lib-test\config.vspe";
            string p2 = "-minimize";

            if(isKillProcess)
            {
                // If it is running, kill it
                KillProcess(processName);
            }
            
            if(!IsProcessRunning(processName))
            {
                var parameters = new List<string>();
                parameters.Add(p1);
                parameters.Add(p2);

                // Start the process in a new process
                StartProcess(executablePath, parameters);
            }
            
            //Log("VSPE started");
        }

        internal static void KillProcess()
        {
            KillProcess("VSPEmulator");
        }


        internal bool IsVSPEConfigExists(string path)
        {
            string filePath = Path.Combine(path, "config.vspe");
            bool isExists = File.Exists(filePath);
            return isExists;
        }

        internal bool IsVSPEConfigMatchSoftware()
        {
            throw new NotImplementedException();
        }

        internal void CreateVSPEConfig(string port1, string port2, string configPath)
        {
            try
            {
                string p1 = port1.ToUpper().Replace("COM", "");
                string p2 = port2.ToUpper().Replace("COM", "");
                //check that
                uint iPort1 = Convert.ToUInt16(p1);
                uint iPort2 = Convert.ToUInt16(p2);

                CreateVSPEConfig(iPort1, iPort2, configPath);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Private Methods
        internal void CreateVSPEConfig(uint port1, uint port2, string configPath)
        {
            try
            {              
                if (port1 == 0 || port1 > 9 || port2 == 0 || port2 > 9)
                {
                    throw new Exception("Port 1 and port 2 need to be values from 1 to 9 each");
                }

                if (port1 == port2)
                {
                    throw new Exception("Port 1 and port 2 cannot be the same");
                }

                char charPort1 = Convert.ToChar(Convert.ToString(port1));
                char charPort2 = Convert.ToChar(Convert.ToString(port2));

                bool isExist = CheckFolderExists(configPath);

                // Log("Creating VSPE:" + configPath);
                GenerateVSPEFile(charPort1, charPort2, configPath);
                //Log("Creating VSPE config complete");
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
        internal bool IsProcessRunning()
        {
            string processName = "VSPEmulator";

            return IsProcessRunning(processName);
        }
        private bool IsProcessRunning(string processName)
        {
            //string processName = "VSPEmulator";
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length > 0;
        }

        private static void KillProcess(string processName)
        {
            //Log("Killing '" + processName + "'");
            // Check if any process with the given name is running
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length > 0)
            {
                //Log("Killing '" + processName + "'");
                // Kill all processes with the given name
                //Process[] processes = Process.GetProcessesByName(processName);
                foreach (Process process in processes)
                {
                    process.Kill();
                }
            }
           // Log("Killing '" + processName + "'...done");
        }

        private void StartProcess(string executablePath, List<string> parameters)
        {
           // Log("Starting VSPE");
            // Start a new process with the specified executable and parameter
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = executablePath;
            string parameterString = "";

            foreach (string line in parameters)
            {
                parameterString = parameterString + " " + line;
            }

            startInfo.Arguments = parameterString;

            Process process = Process.Start(startInfo);

            // Wait for the process to enter an idle state
            if (process != null)
            {
                process.WaitForInputIdle();
                process.Dispose();
            }
                            
            //Log("VSPE started");
        }
    
        private bool CheckFolderExists(string path)
        {
            bool result = false;
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
               // Log(ex.Message, true);
            }
            return result;
        }

        private void GenerateVSPEFile(char port1, char port2, string path)
        {
            string fileName = "config.vspe";
            //Log(string.Format("Generating VSPE config. Port 1: {0}, Port 2: {1}, File: {2}", port1, port2, fileName));

            // Define the content of the VSPE file
            byte[] content = {
            0x01, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, // [SOH][NUL][NUL][NUL][EOT][NUL][NUL][NUL]
            (byte)'P', (byte)'a', (byte)'i', (byte)'r', 0x04, 0x00, 0x00, 0x00,(byte)port1, (byte)';', (byte)port2, (byte)';', (byte)'0' // Pair[ENQ][NUL][NUL][NUL]5;6;0
        };
            // Write the content to the file
            File.WriteAllBytes(Path.Combine(path, fileName), content);

        }
    }
}
