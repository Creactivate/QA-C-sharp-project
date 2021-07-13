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
                        Console.WriteLine("Press 1 to view a specific year's sales");
                        Console.WriteLine("Press 2 to view a specific month's sales in a specific year");
                        Console.WriteLine("Press 3 to view a specific year's total sales");
                        Console.WriteLine("Press 4 to view a specific month's total sales in a specific year");
                        Console.WriteLine("Press q to exit this menu.");

                        menuTwoEntry = Console.ReadLine();
                        Console.Clear();

                        int year = 1000;
                        string month = "";

                        switch (menuTwoEntry)
                        {
                            case "1":
                                year = getYear();
                                db.printTable($"SELECT LPAD(CONVERT(SaleID, CHAR), 15, ' ') AS 'SaleID', LPAD(Product_Name, 15, ' ') AS 'Product Name', LPAD(CONVERT(Qty, CHAR),15,' ') AS 'Quantity', LPAD(CONVERT(Price, CHAR),15,' ') as 'Price', LPAD(CONVERT(DATE(Sale_Date), CHAR), 15, ' ') as 'Sale Date' FROM sales WHERE year(Sale_Date)={year}");
                                break;

                            case "2":
                                year = getYear();
                                month = getMonth();
                                db.printTable($"SELECT LPAD(CONVERT(SaleID, CHAR), 15, ' ') AS 'SaleID', LPAD(Product_Name, 15, ' ') AS 'Product Name', LPAD(CONVERT(Qty, CHAR),15,' ') AS 'Quantity', LPAD(CONVERT(Price, CHAR),15,' ') as 'Price', LPAD(CONVERT(DATE(Sale_Date), CHAR), 15, ' ') as 'Sale Date' FROM sales WHERE year(Sale_Date)={year} AND MONTHNAME(Sale_Date)='{month}'");
                                break;
                            case "3":
                                year = getYear();
                                db.printTable($"SELECT LPAD(SUM(Qty*Price), 15, ' ') AS '{year} Sales' FROM sales WHERE year(Sale_Date)={year}");
                                break;
                            case "4":
                                year = getYear();
                                month = getMonth();
                                db.printTable($"SELECT LPAD(SUM(Qty*Price), 15, ' ') AS '{month} {year}' FROM sales WHERE year(Sale_Date)={year} AND MONTHNAME(Sale_Date)='{month}'");
                                break;
                        }
                    }
                    menuTwoEntry = "";
                }
            }
        }

        public static void createEntry()
        {
            string productName = "";
            float price = 0;
            int qty = 0;
            DateTime date = new DateTime();

            Console.WriteLine("Please enter product name: ");
            productName = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Please enter product price: ");
            price = float.Parse(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Please enter product quantity: ");
            qty = Int32.Parse(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Please enter date sold in (DD/MM/YYYY) format: ");
            date = DateTime.Parse(Console.ReadLine());
            Console.Clear();

            DBInteraction db = new DBInteraction();
            //db.insertIntoTable(productName, price, qty);
            db.insertIntoTableDate(productName, price, qty, date);

            Console.WriteLine("Success!");
            Console.ReadLine();
            Console.Clear();
        }

        public static int getYear()
        {
            int input = 0;
            bool validity = false;
            while (validity == false)
            {
                try
                {
                    Console.WriteLine("Please enter the year needed:");
                    input = Int32.Parse(Console.ReadLine());
                    if (input >= 1000 && input <= 9999)
                    {
                        validity = true;
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid input please try again");
                        validity = false;
                        continue;
                    }
                }
                catch
                {
                    Console.WriteLine("An error occured, please try again");
                    validity = false;
                    continue;
                }
            }
            return input;
        }

        public static string getMonth()
        {
            string input = "";
            bool validity = false;
            while (validity == false)
            {
                try
                {
                    Console.WriteLine("Please enter the name of the month needed:");
                    input = Console.ReadLine().ToLower();
                    string[] months = { "january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november", "december" };
                    if (Array.IndexOf(months, input) == -1) 
                    {
                        Console.Clear();
                        validity = false;
                        Console.WriteLine("Invalid entry, please try again");
                    } else
                    {
                        validity = true;
                        Console.Clear();
                    }
                }
                catch
                {
                    Console.WriteLine("An error occured, please try again");
                    validity = false;
                    continue;
                }
            }
            return input.Substring(0,1).ToUpper() + input.Substring(1);
        }
    }
}
