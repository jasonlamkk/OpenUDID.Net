using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsDesktopConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            ;
            Console.WriteLine("UDID:{0}", OpenUDIDCSharp.OpenUDID.value);
            Console.WriteLine("Press Entry To Continue ");
            Console.ReadLine();
        }
    }
}
