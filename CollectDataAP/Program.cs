using GlobalVar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Win8Hottab;
using Windows.ApplicationModel;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;

namespace CollectDataAP
{
    class Program
    {
        static private AppServiceConnection connection = null;

        static void Main(string[] args)
        {
            Console.ReadLine();

            InitializeWMIHandler();
            InitializeAppServiceConnection();
            InitGlobalVariable();
            

            string tempForUWP = "", tempForOutput = "";

            string sModelName = "";
            if (GlobalVariable.sECMBVersion == "IBWH")
            {
                sModelName = "A8XV1";
            }
            else if (GlobalVariable.sECMBVersion == "ID8H")
            {
                sModelName = "A7";
            }
            else if (GlobalVariable.sECMBVersion == "ID82")
            {
                sModelName = "A10XV1";
            }
            else if (GlobalVariable.sECMBVersion == "IB10")
            {
                if ((GlobalVariable.sECVersion[0] == '0') || (GlobalVariable.sECVersion[0] == '1'))
                {
                    sModelName = "A10XV2";
                }
                else
                {
                    sModelName = "A10XV3";
                }
            }
            else
            {
                sModelName = "";
            }

            if ((GlobalVariable.sBIOSVersion[0] < 0x30) || (GlobalVariable.sBIOSVersion[0] > 0x39))
                GlobalVariable.sBIOSVersion = GlobalVariable.sBIOSVersion.Substring(1, GlobalVariable.sBIOSVersion.Length - 1);

            Console.WriteLine(tempForOutput = ("BIOS Ver.: " + sModelName + "(" + GlobalVariable.sBIOSVersion + ")"));
            tempForUWP += tempForOutput + "\n";
            Console.WriteLine(tempForOutput = ("EC Ver.: " + sModelName + "(" + GlobalVariable.sECVersion + ")"));
            tempForUWP += tempForOutput + "\n";

            if ((GlobalVariable.sECVersion[0] == '0') || (GlobalVariable.sECVersion[0] == '1'))
            {
                Console.WriteLine(tempForOutput = ("HotTab Ver.: " + "A10XV2" + "(" + GetApplicationVersion() + ")"));
                tempForUWP += tempForOutput + "\n";
            }
            else
            {
                Console.WriteLine(tempForOutput = ("HotTab Ver.: " + "A10XV3" + "(" + GetApplicationVersion() + ")"));
                tempForUWP += tempForOutput + "\n";
            }
            Console.WriteLine(tempForOutput = ("OS Ver.: " + GlobalVariable.OSVersion));
            tempForUWP += tempForOutput + "\n";
            Console.WriteLine(tempForOutput = ("Units Ver.: " + GlobalVariable.sUnitsSN));
            tempForUWP += tempForOutput + "\n";

            Console.WriteLine("");
            Console.WriteLine("Send data to UWP: yes/no");

            if (Console.ReadLine() == "yes")
            {
                SendData2UWP(tempForUWP);
            }
        }

        static private async void SendData2UWP(string data)
        {
            // ask the UWP to calculate d1 + d2
            ValueSet request = new ValueSet();
            request.Add("D1", (string)data);
            //request.Add("D2", (double)2);
            AppServiceResponse response = await connection.SendMessageAsync(request);
            //double result = (double)response.Message["RESULT"];
        }

        static private async void InitializeAppServiceConnection()
        {
            connection = new AppServiceConnection();
            connection.AppServiceName = "SampleInteropService";
            connection.PackageFamilyName = Package.Current.Id.FamilyName;
            //connection.RequestReceived += Connection_RequestReceived;
            //connection.ServiceClosed += Connection_ServiceClosed;

            AppServiceConnectionStatus status = await connection.OpenAsync();
            if (status != AppServiceConnectionStatus.Success)
            {
                // something went wrong ...
                Console.WriteLine(status.ToString());
                Console.ReadLine();
                //this.IsEnabled = false;
            }
        }



        static private void InitializeWMIHandler()
        {
            HotTabWMIInformation wmiHandler = new HotTabWMIInformation();
            try
            {
                //OK1 BIOS Version
                GlobalVariable.sBIOSVersion = wmiHandler.GetWMI_BIOSVersion();

                //OK1 OS Name
                HotTabDLL.OSName = wmiHandler.Get_OSName();
                bool IsWin8 = HotTabWMIInformation.IsWindows8;
                bool IsWin8_81 = wmiHandler.IsWindows8_81();
                bool IsWin10_1709 = wmiHandler.IsWindows10_1709();

                //Units SN
                GlobalVariable.sUnitsSN = wmiHandler.GetWMI_BIOSSerialNumber();

                //MainBoardVersion
                GlobalVariable.sMainBoardVersion = wmiHandler.GetWMI_BIOSMainBoard();

                //OS Version
                //HotTabRegistry.RegistryRead("SOFTWARE", "OSVersion", ref GlobalVariable.OSVersion);

                if (GlobalVariable.sBIOSVersion == "")
                {
                    Environment.Exit(1);
                }

                //GlobalVariable.sBIOSVersion = GlobalVariable.sBIOSVersion.Trim().Remove(0, 0);

                HotTabDLL.WinIO_GetECMBVersion(out GlobalVariable.sECMBVersion);
                HotTabDLL.WinIO_GetECVersion(out GlobalVariable.sECVersion);

            }
            catch
            {
                //Do Nothing
            }

            //OK1 Processor Name

            //winmate brian modify not use wmi get processor name ++
            //ProcessorName = wmiHandler.GetWMI_ProcessorName();
            string ProcessorName = wmiHandler.Get_ProcessorName();
            //winmate brian modify not use wmi get processor name --

        }

        static public void InitGlobalVariable()
        {
            if (!GlobalVariable.Load_HottabCfg())
            {
                Console.WriteLine("HottabCfg.ini not exist!");

                Console.ReadLine();
                SendData2UWP("Error! Please restart.");
                Console.ReadLine();
                Environment.Exit(1);
            }
        }

        static public string GetApplicationVersion()
        {
            char[] cdelimiterChars2 = { '.' };
            string[] words2 = Assembly.GetExecutingAssembly().GetName().Version.ToString().Split(cdelimiterChars2);

            return words2[0].ToString() + "." + words2[1].ToString() + "." + words2[2].ToString();
        }
    }
}
