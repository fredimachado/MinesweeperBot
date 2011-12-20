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

            uint result = 0;
            for (uint w = 1; w <= width; w++)
            {
                for (uint h = 1; h <= height; h++)
                {
                    byte cellValue = Helper.MineProcess.ReadByte(0x1005340 + (h << 5) + w);
                    if (cellValue == 0x0F) // No mine
                        Console.WriteLine(w + "," + h + " is safe.");
                    else
                        Console.WriteLine(w + "," + h + " WILL EXPLODE!!");
                }
            }

            Helper.MineProcess.Close();
        }
    }
}
