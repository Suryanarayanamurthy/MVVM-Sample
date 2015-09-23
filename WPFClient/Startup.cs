using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

/// <summary>
/// todo : double UI both launch with both console and WPF.
/// uncomment the below code and in the properties of the project, change the startup object to this class.
/// </summary>
namespace interSalesAssignment
{
    // not able to do this console and wpf double startup as of now 
    class Startup
    { 
    //{
    //    [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
    //    static extern bool FreeConsole();

    //    [STAThread]
    //    public static int Main(string[] args)
    //    {
    //        if (args != null && args.Length > 0)
    //        {
    //            //Console code.
    //            // not able to do this console part of the assignment.
    //            Console.WriteLine("Hello world. ");
    //            Console.ReadLine();
    //            return 0;
    //        }
    //        else
    //        {
    //            FreeConsole();
    //            var app = new App();
    //            return app.Run();
    //        }
    //    }
    }
}
