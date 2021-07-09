using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Base_Project_C_Sharp
{
    class DBInteraction
    {
        public DBInteraction()
        {
            // Constructor: setup database connection
            Con = new MySqlConnection("server=localhost;user=root;password=root;database=nbs");
            Con.Open();
        }

        private MySqlConnection Con;

        private MySqlCommand cmd;

        public void printTable(string query)
        {
            cmd = new MySqlCommand(query,Con);
            MySqlDataReader data = cmd.ExecuteReader();
            while (data.Read())
            {
                Console.WriteLine("headings");
                Console.WriteLine("_____________________________________________");
                for (int i = 0; i < data.FieldCount;i++)
                {
                    if (i == data.FieldCount - 1)
                    {
                        Console.WriteLine($"|  {data[i]}  |");
                    }
                    else
                    {
                        Console.Write($"|  {data[i]}  ");
                    }
                    
                }
                Console.WriteLine("_____________________________________________");
            }
            Console.ReadLine();
            Console.Clear();
        }

        public void insertIntoTable(string productName, float price, int qty)
        {
            //Insert values into table
            cmd = new MySqlCommand($"INSERT INTO sales(Product_Name, Qty, Price) VALUES('{productName}', {qty}, {price})",Con);
            cmd.ExecuteNonQuery();
        }
    }
}
