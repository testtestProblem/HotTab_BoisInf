using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;

namespace GlobalVar
{

    public class GlobalVariable
    {
        public struct DEVICE_STATUS
        {
            public bool Active;
            public byte Device;
            public byte BitSel;
            public string Default;
        }

        public struct FUNCTION_KEY_STATUS
        {
            public bool Active;
            public string Short;
            public string Long;
        }

        public struct DEVICE_STATUS_STRUCT
        {
            public DEVICE_STATUS Wifi;
            public DEVICE_STATUS D3G;
            public DEVICE_STATUS Gps;
            public DEVICE_STATUS Bluttooth;
            public DEVICE_STATUS Camera;
            public DEVICE_STATUS Ant_WWAN;
            public DEVICE_STATUS BarCode;
            public DEVICE_STATUS CameraFront;
            public DEVICE_STATUS RFID;
            public DEVICE_STATUS ComPort;
            public DEVICE_STATUS Usb;
            public DEVICE_STATUS Ant_GPS;        //  0: Auto (change to  external when plug on Docking)  1: Alway Internal (Default)   2:  Always external
        }

        public struct DEVICE_SUPPORT_STRUCT
        {
            public bool Barcode;
            public bool Camera;
            public bool RotationLock;
        }

        public struct FUNCTION_KEY_STATUS_STRUCT
        {
            public FUNCTION_KEY_STATUS F1;
            public FUNCTION_KEY_STATUS F2;
            public FUNCTION_KEY_STATUS F3;
            public FUNCTION_KEY_STATUS F4;
        }

        #region Const Declare

        public const int DEV_NULL = -1;
        public const int DEV_WIFI = 0;
        public const int DEV_3G = 1;
        public const int DEV_GPS = 2;
        public const int DEV_BLUETOOTH = 3;
        public const int DEV_CAMERA = 4;


        public const int DEV_BARCODE = 0;
        //public const int DEV_CAMERAFRONT = 1;
        public const int DEV_RFID = 1;
        public const int DEV_RFID_CONFIG = 2;
        public const int DEV_COMPORT = 3;
        public const int DEV_USB = 4;
        public const int DEV_ANT_GPS = 5;
        public const int DEV_Ant_WWAN = 6;
        public const int DEV_KEYBOARD = 7;
        public const int DEV_TOUCH_SET = 8;

        public const int DEV_ORIENTATION_0 = 0;
        public const int DEV_ORIENTATION_90 = 1;
        public const int DEV_ORIENTATION_180 = 2;
        public const int DEV_ORIENTATION_270 = 3;



        #endregion

        //HotTab Version
        //public static string sHotTabVersion = "40.2.24";
        //BIOS Version
        public static string sBIOSVersion = "";
        //EC Version
        public static string sECVersion = "";
        //EC MB Version
        public static string sECMBVersion = "";
        //SMBIOS Units SN
        public static string sUnitsSN = "";
        //MainBoard Ver.
        public static string sMainBoardVersion = "";
        //OS Ver.
        public static string OSVersion = "";

        //HottabCfg.ini >>
        public static string HotTabBoardName = "IB80";
        public static string HotTabCfgVersion = "0";
        public static uint bDebug = 0;                              //0:disable debug message, 1:enable debug message
        public static uint FlashControlType = 1;                    //0:Null, 1:USB, 2:GPIO, 3:USB and GPIO
        public static uint DeviceCount = 2;                         //1:Device1, 2:Device1,Device2
        public static uint DeviceControl = 0;                       //0:default, 1:only wifi and bluetooth control
        public static uint StartOrientation = DEV_ORIENTATION_0;    //0:0 deg, 1:90 deg, 2:180 deg, 3: 270 deg
        public static uint CameraForceOrientation = 0;              //0:Null, 1:0 deg, 2:90 deg, 3:180 deg, 4:270 deg
        public static uint BatteryType = 0;                         //0:Left,Right, 1:Main,Second, 2:Main

        public static uint SmallBatteryUseAtoD = 0;                 //0:use smbus information, 1:use voltage
        public static uint SmallBatteryPowerSettingBrightnessLimit = 0; //brightness limit 0 - 100
        public static uint SmallBatteryPowerSettingProcessorLimit = 50;  //processor limit 0% - 100%
        public static uint SmallBatteryDisplay = 0;                 //0:small battery not display, 1:display small battery

        public static uint BarcodeType = 0;                         //0:Normal(BS523/MDI3100/M3/), 1:MDL-1000, 2:ISDC-RS, 3:Moto(SE4500DL)
        public static string BarcodeCOMLocation = "COM15";
        public static uint BarcodeVisible = 0;                      //0:not dispaly, 1:display
        public static uint BarcodeIdentifierCode = 1;               //0:removed, 1:reserved

        public static uint SoftwareHotSwapSupport = 0;              //0:not support, 1:support
        public static uint OemSpecialVersion = 0;                   //0:Normal

        public static uint TouchSetSupport = 0;                     //0:not support 1: support
        public static uint TouchModeDefault = 0;                    //0:Hand/Rain Mode, 1:Stylus Mode, 2:Glove Mode

        public static uint AutoBrightnessSupport = 0;               //0:Disable 1: Enable
        public static uint AutoRotationSupport = 0;                 //0:Disable 1: Enable
        public static uint SensorHubExist = 0;                      //0:Disable 1: Enable

        public static bool autoRotationFlag = true;

        public static uint UserPermissionLock = 0;                   //0:unlock 1:lock

        public static bool HidenShowFormFlag = false;
        public static bool RotationFlagFormDevice = false;
        public static bool RotationFlagFormShortCut = false;
        public static bool RotationFlagFormMain = false;
        public static bool RotationFlagFormSetting = false;
        public static bool RotationFlagFormSettingFun1S = false;
        public static bool RotationFlagFormSettingFun1L = false;
        public static bool RotationFlagFormSettingFun2S = false;
        public static bool RotationFlagFormSettingFun2L = false;
        public static bool RotationFlagFormSettingFun3S = false;
        public static bool RotationFlagFormSettingFun3L = false;
        public static bool rotationFlag = false;
        public static bool RotationFlagFormCamera = false;

        //public static bool Angle0RotationLock = false;
        //public static bool Angle90RotationLock = false;
        //public static bool Angle180RotationLock = false;
        //public static bool Angle270RotationLock = false;

        public static uint GPSLoadDefaultSupport = 0;                 //0:not support 1: support

        //Camera >>
        public static uint CameraRotateSupport = 0;                 //0:Disable, 1: Enable
        public static uint FixedCameraRotate = 0;                   //0:Disable, 1: Enable
        public static string CameraBackVideoInput = "none";
        public static string CameraFrontVideoInput = "none";
        public static uint NewCameraSupport = 0;                    //0:Disable 1: Enable
        public static DEVICE_STATUS_STRUCT Device;
        public static DEVICE_SUPPORT_STRUCT DeviceSupport;

        public static uint FKeyCount = 2;
        public static FUNCTION_KEY_STATUS_STRUCT FnKey;

        public static byte Device1AlwaysOnBits = 0x00;
        public static byte Device2AlwaysOnBits = 0x00;

        public static uint TouchLockKeySupport = 0;                 //0:not support 1: support

        //HOttabCfg.ini <<

        public static int[] DeviceOrder = { DEV_WIFI, DEV_3G, DEV_GPS, DEV_BLUETOOTH, DEV_CAMERA, DEV_NULL, DEV_NULL, DEV_NULL };
        public static int[] DeviceOrder2 = { DEV_BARCODE, DEV_RFID, DEV_RFID_CONFIG, DEV_COMPORT, DEV_USB, DEV_ANT_GPS, DEV_Ant_WWAN, DEV_KEYBOARD, DEV_TOUCH_SET };

        public static bool Load_HottabCfg()
        {

            IniFile inifile = new IniFile();
            string patch = System.Windows.Forms.Application.StartupPath;
            //string patch = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            //inifile.path = patch + "\\HottabCfg.ini";
            inifile.path = "C:\\HottabCfg.ini";

            if (!File.Exists(inifile.path))
                return false;

            //Version
            HotTabBoardName = inifile.IniReadValue("Version", "ProjectName");
            HotTabCfgVersion = inifile.IniReadValue("Version", "CfgVersion");

            //OEM
            OemSpecialVersion = IniReadUIntValue("OEM", "OemSpecialVersion");

            //Function
            bDebug = Convert.ToUInt16(IniReadHexValue("Function", "DebugMessage"));
            FlashControlType = Convert.ToUInt16(IniReadHexValue("Function", "FlashType"));
            StartOrientation = Convert.ToUInt16(IniReadHexValue("Function", "StartOrientation"));
            CameraForceOrientation = Convert.ToUInt16(IniReadHexValue("Function", "CameraForceOrientation"));
            CameraRotateSupport = Convert.ToUInt16(IniReadHexValue("Function", "CameraRotateSupport"));
            FixedCameraRotate = Convert.ToUInt16(IniReadHexValue("Function", "FixedCameraRotate"));
            BatteryType = Convert.ToUInt16(IniReadHexValue("Function", "BatteryType"));

            SmallBatteryUseAtoD = Convert.ToUInt16(IniReadHexValue("SmallBatterySetting", "SmallBatteryUseAtoD"));
            SmallBatteryPowerSettingBrightnessLimit = IniReadUIntValue("SmallBatterySetting", "SmallBatteryPowerSettingBrightnessLimit");
            SmallBatteryPowerSettingProcessorLimit = IniReadUIntValue("SmallBatterySetting", "SmallBatteryPowerSettingProcessorLimit");
            SmallBatteryDisplay = Convert.ToUInt16(IniReadHexValue("SmallBatterySetting", "SmallBatteryDisplay"));
            
            AutoBrightnessSupport = Convert.ToUInt16(IniReadHexValue("Function", "AutoBrightnessSupport"));
            AutoRotationSupport = Convert.ToUInt16(IniReadHexValue("Function", "AutoRotationSupport"));
            NewCameraSupport = Convert.ToUInt16(IniReadHexValue("Function", "NewCameraSupport"));

            TouchSetSupport = Convert.ToUInt16(IniReadHexValue("Function", "TouchSetSupport"));
            TouchModeDefault = Convert.ToUInt16(IniReadHexValue("Function", "TouchModeDefault"));

            if (AutoRotationSupport == 0)
                autoRotationFlag = false;

            if (NewCameraSupport == 1)
                CameraRotateSupport = 0;

            SensorHubExist = Convert.ToUInt16(IniReadHexValue("Function", "SensorHubExist"));

            UserPermissionLock = Convert.ToUInt16(IniReadHexValue("Function", "UserPermissionLock"));

            //Camera
            CameraBackVideoInput = inifile.IniReadValue("Function", "CameraBackVideoInput");
            CameraFrontVideoInput = inifile.IniReadValue("Function", "CameraFrontVideoInput");

            GPSLoadDefaultSupport = Convert.ToUInt16(IniReadHexValue("Function", "GPSLoadDefaultSupport")); 
            
            //Barcode
            BarcodeType = IniReadUIntValue("Function", "BarcodeType");
            BarcodeCOMLocation = inifile.IniReadValue("Function", "BarcodeCOMLocation");
            if (BarcodeCOMLocation.ToUpper().IndexOf("COM") == -1) BarcodeCOMLocation = "COM15";


            SoftwareHotSwapSupport = Convert.ToUInt16(IniReadHexValue("Function", "SoftwareHotSwapSupport"));

            //Function Key
            FKeyCount = IniReadUIntValue("FunctionKey", "FKEYCount");

            FnKey.F1.Short = inifile.IniReadValue("FunctionKey", "F1S");
            FnKey.F1.Long = inifile.IniReadValue("FunctionKey", "F1L");
            FnKey.F2.Short = inifile.IniReadValue("FunctionKey", "F2S");
            FnKey.F2.Long = inifile.IniReadValue("FunctionKey", "F2L");
            FnKey.F3.Short = inifile.IniReadValue("FunctionKey", "F3S");
            FnKey.F3.Long = inifile.IniReadValue("FunctionKey", "F3L");
            /*
            if ((GlobalVariable.sECVersion[0] == '0') || (GlobalVariable.sECVersion[0] == '1'))
            {
                //For IB10X-1XX
                TouchLockKeySupport = 0;
            }
            else
            {
                //For IB10X-2XX
                TouchLockKeySupport = 1;
            }
            */
            /*
            switch (FKeyCount)
            {
                case 0:
                    FnKey.F1.Active = false;
                    FnKey.F2.Active = false;
                    FnKey.F3.Active = false;
                    break;
                case 1:
                    FnKey.F1.Active = true;
                    FnKey.F2.Active = false;
                    FnKey.F3.Active = false;
                    break;
                case 2:
                    FnKey.F1.Active = true;
                    FnKey.F2.Active = true;
                    FnKey.F3.Active = false;
                    break;
                case 3:
                    FnKey.F1.Active = true;
                    FnKey.F2.Active = true;
                    FnKey.F3.Active = true;
                    break;
                default:
                    FnKey.F1.Active = false;
                    FnKey.F2.Active = false;
                    FnKey.F3.Active = false;
                    break;
            }
            */
            //Device
            DeviceCount = Convert.ToUInt16(IniReadHexValue("Device", "DeviceCount"));    //1:Device1, 2:Device1,Device2

            //Device
            DeviceControl = Convert.ToUInt16(IniReadHexValue("Device", "DeviceControl"));    //0:default, 1:only wifi and bluetooth control


            Device1AlwaysOnBits = IniReadHexValue("Device", "Device1AlwaysOnBits");
            Device2AlwaysOnBits = IniReadHexValue("Device", "Device2AlwaysOnBits");

            //WIFI
            Device.Wifi.BitSel = IniReadHexValue("Device", "WifiActive");
            Device.Wifi.Device = IniReadHexValue("Device", "WifiDevice");
            Device.Wifi.Default = inifile.IniReadValue("Device", "WifiDefault");

            if (Device.Wifi.Device == 0)
                Device.Wifi.Active = false;
            else if (Device.Wifi.BitSel != 0)
                Device.Wifi.Active = true;
            else
                Device.Wifi.Active = false;

            //3G
            Device.D3G.BitSel = IniReadHexValue("Device", "D3GAcitve");
            Device.D3G.Device = IniReadHexValue("Device", "D3GDevice");
            Device.D3G.Default = inifile.IniReadValue("Device", "D3GDefault");

            if (Device.D3G.Device == 0)
                Device.D3G.Active = false;
            else if (Device.D3G.BitSel != 0)
                Device.D3G.Active = true;
            else
                Device.D3G.Active = false;

            //GPS
            Device.Gps.BitSel = IniReadHexValue("Device", "GPSActive");
            Device.Gps.Device = IniReadHexValue("Device", "GPSDevice");
            Device.Gps.Default = inifile.IniReadValue("Device", "GPSDefault");

            if (Device.Gps.Device == 0)
                Device.Gps.Active = false;
            else if (Device.Gps.BitSel != 0)
                Device.Gps.Active = true;
            else
                Device.Gps.Active = false;

            //GPS_Antenna
            Device.Ant_GPS.BitSel = IniReadHexValue("Device", "AntGPSActive");
            Device.Ant_GPS.Device = IniReadHexValue("Device", "AntGPSDevice");
            Device.Ant_GPS.Default = inifile.IniReadValue("Device", "AntGPSDefault");

            if (Device.Ant_GPS.Device == 0)
                Device.Ant_GPS.Active = false;
            else if (Device.Ant_GPS.BitSel != 0)
                Device.Ant_GPS.Active = true;
            else
                Device.Ant_GPS.Active = false;

            //BLUETOOTH
            Device.Bluttooth.BitSel = IniReadHexValue("Device", "BluetoothActive");
            Device.Bluttooth.Device = IniReadHexValue("Device", "BluetoothDevice");
            Device.Bluttooth.Default = inifile.IniReadValue("Device", "BluetoothDefault");

            if (Device.Bluttooth.Device == 0)
                Device.Bluttooth.Active = false;
            else if (Device.Bluttooth.BitSel != 0)
                Device.Bluttooth.Active = true;
            else
                Device.Bluttooth.Active = false;

            //Ant_WWAN
            Device.Ant_WWAN.BitSel = IniReadHexValue("Device", "Ant_WWANActive");
            Device.Ant_WWAN.Device = IniReadHexValue("Device", "Ant_WWANDevice");
            Device.Ant_WWAN.Default = inifile.IniReadValue("Device", "Ant_WWANDefault");

            if (Device.Ant_WWAN.Device == 0)
                Device.Ant_WWAN.Active = false;
            else if (Device.Ant_WWAN.BitSel != 0)
                Device.Ant_WWAN.Active = true;
            else
                Device.Ant_WWAN.Active = false;

            //BarCode
            Device.BarCode.BitSel = IniReadHexValue("Device", "BarCodeActive");
            Device.BarCode.Device = IniReadHexValue("Device", "BarCodeDevice");
            Device.BarCode.Default = inifile.IniReadValue("Device", "BarCodeDefault");

            if (Device.BarCode.Device == 0)
                Device.BarCode.Active = false;
            else if (Device.BarCode.BitSel != 0)
                Device.BarCode.Active = true;
            else
                Device.BarCode.Active = false;

            //RFID
            Device.RFID.BitSel = IniReadHexValue("Device", "RFIDActive");
            Device.RFID.Device = IniReadHexValue("Device", "RFIDDevice");
            Device.RFID.Default = inifile.IniReadValue("Device", "RFIDDefault");

            if (Device.RFID.Device == 0)
                Device.RFID.Active = false;
            else if (Device.RFID.BitSel != 0)
                Device.RFID.Active = true;
            else
                Device.RFID.Active = false;

            //COMPORT
            Device.ComPort.BitSel = IniReadHexValue("Device", "ComPortActive");
            Device.ComPort.Device = IniReadHexValue("Device", "ComPortDevice");
            Device.ComPort.Default = inifile.IniReadValue("Device", "ComPortDefault");

            if (Device.ComPort.Device == 0)
                Device.ComPort.Active = false;
            else if (Device.ComPort.BitSel != 0)
                Device.ComPort.Active = true;
            else
                Device.ComPort.Active = false;

            //USB
            Device.Usb.BitSel = IniReadHexValue("Device", "UsbActive");
            Device.Usb.Device = IniReadHexValue("Device", "UsbDevice");
            Device.Usb.Default = inifile.IniReadValue("Device", "UsbDefault");

            if (Device.Usb.Device == 0)
                Device.Usb.Active = false;
            else if (Device.Usb.BitSel != 0)
                Device.Usb.Active = true;
            else
                Device.Usb.Active = false;

            //CAMERA
            Device.Camera.BitSel = IniReadHexValue("Device", "CameraActive");
            Device.Camera.Device = IniReadHexValue("Device", "CameraDevice");
            Device.Camera.Default = inifile.IniReadValue("Device", "CameraDefault");

            //CAMERA FRONT
            Device.CameraFront.BitSel = IniReadHexValue("Device", "CameraFrontActive");
            Device.CameraFront.Device = IniReadHexValue("Device", "CameraFrontDevice");
            Device.CameraFront.Default = inifile.IniReadValue("Device", "CameraFrontDefault");
            if (Device.Camera.Device == 0)
            {
                Device.Camera.Active = false;
                DeviceSupport.Camera = false;
            }
            else if (Device.Camera.BitSel != 0)
            {
                Device.Camera.Active = true;
                DeviceSupport.Camera = true;
            }
            else
            {
                Device.Camera.Active = false;
                DeviceSupport.Camera = false;
            }
            if (Device.CameraFront.Device == 0)
                Device.CameraFront.Active = false;
            else if (Device.CameraFront.BitSel != 0)
                Device.CameraFront.Active = true;
            else
                Device.CameraFront.Active = false;

            InitDeviceOrder();
            InitDeviceOrder2();

            return true;
        }

        public static void ReloadDeviceOrder()
        {
            InitDeviceOrder();
            InitDeviceOrder2();
        }

        public static byte IniReadHexValue(string Section, string Key)
        {
            IniFile inifile = new IniFile();
            string patch = System.Windows.Forms.Application.StartupPath;
            string src;
            string dest;
            byte value;

            inifile.path = patch + "\\HottabCfg.ini";
            src = inifile.IniReadValue(Section, Key);

            dest = src.Replace("0X", "");
            dest = dest.Replace("0x", "");

            if (dest == "")
                return 0;



            value = byte.Parse(dest, System.Globalization.NumberStyles.AllowHexSpecifier);

            return value;
        }

        public static uint IniReadUIntValue(string Section, string Key)
        {
            IniFile inifile = new IniFile();
            string patch = System.Windows.Forms.Application.StartupPath;
            string src;
            string dest;
            uint value;

            inifile.path = patch + "\\HottabCfg.ini";
            src = inifile.IniReadValue(Section, Key);

            dest = src.Replace("0X", "");
            dest = dest.Replace("0x", "");

            if (dest == "")
                return 0;

            value = Convert.ToUInt16(dest);

            return value;
        }

        public static int DeviceUp(int NowDevice)
        {
            int i = 0;
            int NowDeviceIndex = -1;
            int NextDevice = -1;
            int CurrentIndex = -1;
            int xx = -1;

            for (i = 0; i < DeviceOrder.Length; i++)
            {
                if (DeviceOrder[i] == NowDevice)
                    NowDeviceIndex = i;
            }

            for (i = 0; i < DeviceOrder.Length; i++)
            {
                CurrentIndex = NowDeviceIndex - (i + 1);
                if (CurrentIndex < 0)
                {
                    xx = (DeviceOrder.Length - 1) - NowDeviceIndex;
                    for (i = 0; i < xx; i++)
                    {
                        if (DeviceOrder[DeviceOrder.Length - (i + 1)] != DEV_NULL)
                        {
                            NextDevice = DeviceOrder[DeviceOrder.Length - (i + 1)];
                            break;
                        }
                    }
                    break;
                }
                else
                {
                    if (DeviceOrder[CurrentIndex] != DEV_NULL)
                    {
                        NextDevice = DeviceOrder[CurrentIndex];
                        break;
                    }
                }

            }

            return NextDevice;
        }

        public static int DeviceDown(int NowDevice)
        {
            int i = 0;
            int NowDeviceIndex = -1;
            int NextDevice = -1;
            int CurrentIndex = -1;
            int xx = -1;

            for (i = 0; i < DeviceOrder.Length; i++)
            {
                if (DeviceOrder[i] == NowDevice)
                    NowDeviceIndex = i;
            }

            for (i = 0; i < DeviceOrder.Length; i++)
            {
                CurrentIndex = NowDeviceIndex + (i + 1);
                if (CurrentIndex >= DeviceOrder.Length)
                {
                    xx = NowDeviceIndex;
                    for (i = 0; i < xx; i++)
                    {
                        if (DeviceOrder[i] != DEV_NULL)
                        {
                            NextDevice = DeviceOrder[i];
                            break;
                        }
                    }
                    break;
                }
                else
                {
                    if (DeviceOrder[CurrentIndex] != DEV_NULL)
                    {
                        NextDevice = DeviceOrder[CurrentIndex];
                        break;
                    }
                }

            }


            return NextDevice;
        }

        public static int CountDevice()
        {
            int i = 0;
            int count = 0;

            for (i = 0; i < DeviceOrder.Length; i++)
            {
                if (DeviceOrder[i] != DEV_NULL)
                    count++;
            }

            return count;
        }

        public static void InitDeviceOrder()
        {
            int i = 0;

            for (i = 0; i < DeviceOrder.Length; i++)
            {
                if ((DeviceOrder[i] == DEV_WIFI) && (!Device.Wifi.Active))
                    DeviceOrder[i] = DEV_NULL;

                if ((DeviceOrder[i] == DEV_3G) && (!Device.D3G.Active))
                    DeviceOrder[i] = DEV_NULL;

                if ((DeviceOrder[i] == DEV_GPS) && (!Device.Gps.Active))
                    DeviceOrder[i] = DEV_NULL;

                if ((DeviceOrder[i] == DEV_BLUETOOTH) && (!Device.Bluttooth.Active))
                    DeviceOrder[i] = DEV_NULL;

                if ((DeviceOrder[i] == DEV_CAMERA) && (!Device.Camera.Active))
                    DeviceOrder[i] = DEV_NULL;
            }
        }
        public static void InitDeviceOrder2()
        {
            int i = 0;

            for (i = 0; i < DeviceOrder2.Length; i++)
            {
                if ((DeviceOrder2[i] == DEV_BARCODE) && (!Device.BarCode.Active))
                    DeviceOrder2[i] = DEV_NULL;

                //if ((DeviceOrder2[i] == DEV_CAMERAFRONT) && (!Device.CameraFront.Active))
                //    DeviceOrder2[i] = DEV_NULL;

                if ((DeviceOrder2[i] == DEV_RFID) && (!Device.RFID.Active))
                    DeviceOrder2[i] = DEV_NULL;

                if ((DeviceOrder2[i] == DEV_COMPORT) && (!Device.ComPort.Active))
                    DeviceOrder2[i] = DEV_NULL;

                if ((DeviceOrder2[i] == DEV_USB) && (!Device.Usb.Active))
                    DeviceOrder2[i] = DEV_NULL;

                if ((DeviceOrder2[i] == DEV_ANT_GPS) && (!Device.Ant_GPS.Active))
                    DeviceOrder2[i] = DEV_NULL;

                if ((DeviceOrder2[i] == DEV_Ant_WWAN) && (!Device.Ant_WWAN.Active))
                    DeviceOrder2[i] = DEV_NULL;

                if ((DeviceOrder2[i] == DEV_RFID_CONFIG) && (!Device.RFID.Active))
                    DeviceOrder2[i] = DEV_NULL;

                if ((DeviceOrder2[i] == DEV_TOUCH_SET) && (TouchSetSupport != 1))
                    DeviceOrder2[i] = DEV_NULL;
            }
        }

        public static void DebugMessage(string token, string msg, uint en)
        {
            if (en == 1)
            {
                Trace.WriteLine(token + " ==> " + msg);
            }
        }

        public static float CurrentTimeSecond()
        {
            float tmp = ((float)(DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second) + (float)(DateTime.Now.Millisecond * 0.001));

            return tmp;
        }
    }


    public class IniFile
    {
        public string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
          string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
          string key, string def, StringBuilder retVal,
          int size, string filePath);

        public void InitFile(string INIPath)
        {
            path = INIPath;
        }

        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
            return temp.ToString();
        }
    }
}
