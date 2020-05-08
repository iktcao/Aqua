using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Aqua.SEUIF97
{
    public static class IF97
    {
        static IF97()
        {
            var myPath = new Uri(typeof(IF97).Assembly.CodeBase).LocalPath;
            var myFolder = Path.GetDirectoryName(myPath);

            var is64 = IntPtr.Size == 8;
            var subfolder = is64 ? "\\thirdparty\\SEUIF97\\win64\\" : "\\thirdparty\\SEUIF97\\win32\\";

            LoadLibrary(myFolder + subfolder + "libseuif97.dll");
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllToLoad);


        [DllImport("libseuif97.dll", EntryPoint = "seupt", CallingConvention = CallingConvention.StdCall)]
        public static extern double PT(double p, double t, int wid);

        [DllImport("libseuif97.dll", EntryPoint = "seuph", CallingConvention = CallingConvention.StdCall)]
        public static extern double PH(double p, double h, int wid);

        [DllImport("libseuif97.dll", EntryPoint = "seups", CallingConvention = CallingConvention.StdCall)]
        public static extern double PS(double p, double s, int wid);

        [DllImport("libseuif97.dll", EntryPoint = "seupv", CallingConvention = CallingConvention.StdCall)]
        public static extern double PV(double p, double v, int wid);

        [DllImport("libseuif97.dll", EntryPoint = "seupx", CallingConvention = CallingConvention.StdCall)]
        public static extern double PX(double p, double x, int wid);

        [DllImport("libseuif97.dll", EntryPoint = "seuth", CallingConvention = CallingConvention.StdCall)]
        public static extern double TH(double t, double h, int wid);

        [DllImport("libseuif97.dll", EntryPoint = "seuts", CallingConvention = CallingConvention.StdCall)]
        public static extern double TS(double t, double s, int wid);

        [DllImport("libseuif97.dll", EntryPoint = "seutv", CallingConvention = CallingConvention.StdCall)]
        public static extern double TV(double t, double v, int wid);

        [DllImport("libseuif97.dll", EntryPoint = "seutx", CallingConvention = CallingConvention.StdCall)]
        public static extern double TX(double t, double x, int wid);

        [DllImport("libseuif97.dll", EntryPoint = "seuhs", CallingConvention = CallingConvention.StdCall)]
        public static extern double HS(double t, double s, int wid);

        // Thermodynamic Process of Aqua Turbine
        [DllImport("libseuif97.dll", EntryPoint = "seuishd", CallingConvention = CallingConvention.StdCall)]
        public static extern double Ishd(double pi, double ti, double pe);

        [DllImport("libseuif97.dll", EntryPoint = "seuief", CallingConvention = CallingConvention.StdCall)]
        public static extern double Ief(double pi, double ti, double pe, double te);

    }
}

