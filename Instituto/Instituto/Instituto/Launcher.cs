using System;
using System.Data.SqlClient;

namespace Instituto
{
    class Launcher
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.Run();
        }
    }
}