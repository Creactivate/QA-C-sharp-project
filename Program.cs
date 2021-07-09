using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Base_Project_C_Sharp
{
    class Program
    {
        private static DBInteraction db = new DBInteraction();
        static void Main(string[] args)
        {
            string menuOneEntry = "";
            string wholeMenuEntry = "";
            string menuTwoEntry = "";

            while (menuOneEntry != "q")
            {
                Console.WriteLine("Press 1 to insert into table.");
                Console.WriteLine("Press 2 to view table or view table filters.");
                Console.WriteLine("Press q to exit this menu.");

                menuOneEntry = Console.ReadLine();
                Console.Clear();

                if (menuOneEntry == "1")
                {
                    Program.createEntry();
                }

                if (menuOneEntry == "2")
                {
                    while (menuTwoEntry != "q")
                    {
                        Console.WriteLine("Press 1 to view whole sales table.");
                        Console.WriteLine("Press 2 to view filter.");
                        Console.WriteLine("Press q to exit this menu.");

                        menuTwoEntry = Console.ReadLine();
                        Console.Clear();

                        switch (menuTwoEntry)
                        {
                            case "1": db.printTable("SELECT * FROM nbs"); break;
                        }
                    }
                }
            }
        }

        public static void createEntry()
        {
            string productName = "";
            float price = 0;
            int qty = 0;

            Console.WriteLine("Please enter product name: ");
            productName = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Please enter product price: ");
            price = float.Parse(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Please enter product quantity: ");
            qty = Int32.Parse(Console.ReadLine());
            Console.Clear();

            DBInteraction db = new DBInteraction();
            db.insertIntoTable(productName, price, qty);

            Console.WriteLine("Success!");
            Console.ReadLine();
        }
    }
}
