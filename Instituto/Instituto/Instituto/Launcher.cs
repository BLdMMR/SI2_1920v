using System;
using System.Data.SqlClient;

namespace Instituto
{
    class Launcher
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            Console.WriteLine("\n\n\t#######     ##    #     #######     #######     #######    #######    #     #    #######    #######\n\t   #        # #   #     #              #           #          #       #     #       #       #     #\n\t   #        #  #  #     #######        #           #          #       #     #       #       #     #\n\t   #        #   # #           #        #           #          #       #     #       #       #     #\n\t#######     #    ##     #######        #        #######       #       #######       #       #######\n\n");
            Console.ReadKey();
            menu.Run();
        }
    }
}

/* #######     ##    #     #######     #######     #######    #######    #     #    #######    #######
 *    #        # #   #     #              #           #          #       #     #       #       #     #
 *    #        #  #  #     #######        #           #          #       #     #       #       #     #
 *    #        #   # #           #        #           #          #       #     #       #       #     #
   #######     #    ##     #######        #        #######       #       #######       #       #######       */