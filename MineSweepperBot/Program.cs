using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Magic;

namespace MineSweepperBot
{
    public class Helper
    {
        private static readonly BlackMagic mineProcess;

        public static BlackMagic MineProcess
        {
            get { return mineProcess; }
        }

        static Helper()
        {
            mineProcess = new BlackMagic();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            if (!Helper.MineProcess.OpenProcessAndThread(SProcess.GetProcessFromWindowTitle("Minesweeper")))
            {
                Console.WriteLine("Minesweeper is not running.");
                Environment.Exit(1);
            }

            uint width = Helper.MineProcess.ReadUInt(0x1005334);
            uint height = Helper.MineProcess.ReadUInt(0x1005338);

            Console.WriteLine(width);
            Console.WriteLine(height);

            Helper.MineProcess.Close();
        }
    }
}
