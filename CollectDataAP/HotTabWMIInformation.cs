using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Windows.Forms;
using GlobalVar;
using Microsoft.Win32;

namespace Win8Hottab
{
    class HotTabWMIInformation
    {
        private ConnectionOptions connectionOptions;
        private ManagementScope managementScope;

        public HotTabWMIInformation()
        {
            InitializeConnectionOptions();
            InitializeManagementScope();
        }

        private void InitializeConnectionOptions()
        {
            connectionOptions = new ConnectionOptions();
            connectionOptions.Impersonation = ImpersonationLevel.Impersonate;
            connectionOptions.Authentication = AuthenticationLevel.Default;
            connectionOptions.EnablePrivileges = true;
        }

        private void InitializeManagementScope()
        {
            managementScope = new ManagementScope();
            managementScope.Path = new ManagementPath(@"\\" + Environment.MachineName + @"\root\CIMV2");
            managementScope.Options = connectionOptions;
        }

        
        public String GetWMI_OSName()
        {
            String OSName = "";

            SelectQuery selectQuery = new SelectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher managementObjectSearch = new ManagementObjectSearcher(managementScope, selectQuery);
            ManagementObjectCollection managementObjectCollection = managementObjectSearch.Get();

            foreach (ManagementObject managementObject in managementObjectCollection)
            {
                OSName = (string)managementObject["Name"];
            }

            //if (OSName.ToUpper().IndexOf("XP") != -1) OSName = "XP";
            //else if (OSName.IndexOf("2000") != -1) OSName = "2000";
            //else OSName = "VISTA";

            return OSName;
        }

        public string GetWMI_OS_Caption()
        {
            String OSName = string.Empty;

            try
            {
                SelectQuery selectQuery = new SelectQuery("SELECT * FROM Win32_OperatingSystem");
                ManagementObjectSearcher managementObjectSearch = new ManagementObjectSearcher(managementScope, selectQuery);
                ManagementObjectCollection managementObjectCollection = managementObjectSearch.Get();

                foreach (ManagementObject managementObject in managementObjectCollection)
                {
                    OSName = (string)managementObject["Caption"];
                }
            }
            catch
            {
                OSName = "";
            }

            return OSName;
        }

        public bool IsWindows8_81()
        {
            bool bRet = false;
            string tmp = GetWMI_OS_Caption();

            if (tmp.ToUpper().IndexOf("8.1") != -1)
            {
                bRet = true;
            }

            if (tmp.ToUpper().IndexOf("8.0") != -1)
            {
                bRet = true;
            }

            if (tmp.ToUpper().IndexOf("Embedded 8") != -1)
            {
                bRet = true;
            }

            if (tmp.ToUpper().IndexOf("Windows 8") != -1)
            {
                bRet = true;
            }

            return bRet;
        }

        public string GetWMI_OS_BuildNumber()
        {
            String OSBuildNumber = string.Empty;

            try
            {
                SelectQuery selectQuery = new SelectQuery("SELECT * FROM Win32_OperatingSystem");
                ManagementObjectSearcher managementObjectSearch = new ManagementObjectSearcher(managementScope, selectQuery);
                ManagementObjectCollection managementObjectCollection = managementObjectSearch.Get();

                foreach (ManagementObject managementObject in managementObjectCollection)
                {
                    OSBuildNumber = (string)managementObject["BuildNumber"];
                }
            }
            catch
            {
                OSBuildNumber = "";
            }

            return OSBuildNumber;
        }

        public bool IsWindows10_1709()
        {
            bool bRet = false;
            string tmp = GetWMI_OS_BuildNumber();
            int iTmp = 0;

            if (!int.TryParse(tmp, out iTmp)) iTmp = 0;

            if (iTmp >= 16299)
                bRet = true;
           
            return bRet;
        }

        public String Get_OSName()
        {
            string OSName = "";

            if (IsWindows8 || IsWindows7)
                OSName = "VISTA";//"WINDOWS 8";

            return OSName;
        }

        public static bool IsWindows7 //OS version: 6.1.9200 build 9200
        {
            get
            {
                return (Environment.OSVersion.Platform == PlatformID.Win32NT) && (Environment.OSVersion.Version.Major == 6) && (Environment.OSVersion.Version.Minor == 1);
            }
        }

        public static bool IsWindows8 //OS Win8 version: 6.2.9200 build 9200; Win8.1 version:6.3.9600 build 9600
        {
            get
            {
                return (Environment.OSVersion.Platform == PlatformID.Win32NT) && (Environment.OSVersion.Version.Major == 6) && (Environment.OSVersion.Version.Minor >= 2);
            }
        }
        public static bool IsWindows10 //OS Win10 version: 10.0.1024 build 1024
        {
            get
            {
                return (Environment.OSVersion.Platform == PlatformID.Win32NT) && (Environment.OSVersion.Version.Major == 10) && (Environment.OSVersion.Version.Minor >= 0);
            }
        }
        public String GetWMI_BIOSVersion()
        {
            bool bflag = false;
            int iCount = 0;
            String BIOSVersion = "";

            while ((!bflag) && (iCount < 15))
            {
                try
                {
                    iCount++;

                    SelectQuery selectQuery = new SelectQuery("SELECT * FROM Win32_BIOS");
                    ManagementObjectSearcher managementObjectSearch = new ManagementObjectSearcher(managementScope, selectQuery);
                    ManagementObjectCollection managementObjectCollection = managementObjectSearch.Get();

                    foreach (ManagementObject managementObject in managementObjectCollection)
                    {
                        BIOSVersion = (string)managementObject["SMBIOSBIOSVersion"];
                    }
                    bflag = true;
                }
                catch
                {
                    HotTabDLL.Sleep(1000);
                }
            }

            return BIOSVersion;
        }

        public String GetWMI_BIOSSerialNumber()
        {
            String BIOSSerialNumber = "";

            SelectQuery selectQuery = new SelectQuery("SELECT * FROM Win32_BIOS");
            ManagementObjectSearcher managementObjectSearch = new ManagementObjectSearcher(managementScope, selectQuery);
            ManagementObjectCollection managementObjectCollection = managementObjectSearch.Get();

            foreach (ManagementObject managementObject in managementObjectCollection)
            {
                BIOSSerialNumber = (string)managementObject["SerialNumber"];
            }

            return BIOSSerialNumber;
        }

        public String GetWMI_BIOSMainBoard()
        {
            String BIOSMainBoard = "";

            SelectQuery selectQuery = new SelectQuery("SELECT * FROM Win32_ComputerSystemProduct");
            ManagementObjectSearcher managementObjectSearch = new ManagementObjectSearcher(managementScope, selectQuery);
            ManagementObjectCollection managementObjectCollection = managementObjectSearch.Get();

            foreach (ManagementObject managementObject in managementObjectCollection)
            {
                BIOSMainBoard = (string)managementObject["Name"];
            }

            return BIOSMainBoard;
        }

        public String GetWMI_ProcessorName()
        {
            String ProcessorName = "";

            SelectQuery selectQuery = new SelectQuery("SELECT * FROM Win32_Processor");
            ManagementObjectSearcher managementObjectSearch = new ManagementObjectSearcher(managementScope, selectQuery);
            ManagementObjectCollection managementObjectCollection = managementObjectSearch.Get();

            foreach (ManagementObject managementObject in managementObjectCollection)
            {
                ProcessorName = (string)managementObject["Name"];
            }

            if (ProcessorName.ToUpper().IndexOf("INTEL") != -1) ProcessorName = "INTEL";
            else if (ProcessorName.ToUpper().IndexOf("VIA") != -1) ProcessorName = "VIA";

            return ProcessorName;
        }

        //winmate brian modify not use wmi get processor name ++
        public String Get_ProcessorName()
        {
            String ProcessorName = "";

            ProcessorName = Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER");

            if (ProcessorName.ToUpper().IndexOf("INTEL") != -1) ProcessorName = "INTEL";
            else if (ProcessorName.ToUpper().IndexOf("VIA") != -1) ProcessorName = "VIA";

            return ProcessorName;
        }
        //winmate brian modify not use wmi get processor name --

        public List<String> GetWMI_BatteryInformation()
        {
            List<String> batteryInformation = new List<String>();
            String sTemp = "";

            SelectQuery selectQuery = new SelectQuery("SELECT * FROM Win32_Battery");
            ManagementObjectSearcher managementObjectSearch = new ManagementObjectSearcher(managementScope, selectQuery);
            ManagementObjectCollection managementObjectCollection = managementObjectSearch.Get();

            foreach (ManagementObject managementObject in managementObjectCollection)
            {
                try
                {
                    sTemp = managementObject["EstimatedChargeRemaining"].ToString();
                    batteryInformation.Add(sTemp);
                }
                catch
                {
                    //MessageBox.Show("Error");
                }
            }
            return batteryInformation;
        }

        public bool DetectIf64bit()
        {
            bool Is64Bit = false;
            String ProcessorName = "";

            SelectQuery selectQuery = new SelectQuery("SELECT * FROM Win32_Processor");
            ManagementObjectSearcher managementObjectSearch = new ManagementObjectSearcher(managementScope, selectQuery);
            ManagementObjectCollection managementObjectCollection = managementObjectSearch.Get();

            foreach (ManagementObject managementObject in managementObjectCollection)
            {
                ProcessorName = (string)managementObject["AddressWidth"].ToString().Trim();
            }

            if (ProcessorName.ToUpper().IndexOf("64") != -1)
                Is64Bit = true;
            else
                Is64Bit = false;


            return (Is64Bit);
        }

        public static bool SetWmiBrightness(byte targetBrightness)
        {
            GlobalVariable.DebugMessage("winmate", "SetWmiBrightness ==>", GlobalVariable.bDebug);

            bool bResult = false;

            ManagementScope scope = new ManagementScope("root\\WMI");
            SelectQuery query = new SelectQuery("WmiMonitorBrightnessMethods");
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
            {
                using (ManagementObjectCollection objectCollection = searcher.Get())
                {
                    try
                    {
                        foreach (ManagementObject mObj in objectCollection)
                        {
                            mObj.InvokeMethod("WmiSetBrightness", new Object[] { UInt32.MaxValue, targetBrightness });
                            bResult = true;
                            GlobalVariable.DebugMessage("winmate", "SetWmiBrightness [true]", GlobalVariable.bDebug);
                            break;
                        }
                    }
                    catch
                    {
                        GlobalVariable.DebugMessage("winmate", "SetWmiBrightness [false]", GlobalVariable.bDebug);
                    }
                }
            }

            GlobalVariable.DebugMessage("winmate", "SetWmiBrightness ==> " + bResult.ToString(), GlobalVariable.bDebug);

            return bResult;
        }
    }
}
