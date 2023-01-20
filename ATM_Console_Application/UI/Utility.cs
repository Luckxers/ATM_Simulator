using ATM_Console_Application.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Console_Application.UI
{
    public static class Utility
    {
        private static long tranID;

        public static long getTransactionID()
        {
            return ++tranID;
        }

        private static CultureInfo culture = new CultureInfo("en-US");
        //Change the color for error
        public static void PrintMessage(string msg, bool success = true)
        {
            if (!success)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
            PressEnterToContinue();
        }

        public static string GetUser(string user)
        {
            Console.WriteLine(user);
            return Console.ReadLine();
        }

        //Method to not repet code
        public static void PressEnterToContinue()
        {
            Console.WriteLine("Press Enter To continue");
            Console.ReadLine();
        }

        public static string GetSecretPass(string pass)
        {
            bool isPass = true;

            string asterics = "";

            //enable asterics

            StringBuilder input = new StringBuilder();

            while (true)
            {
                if (isPass)
                    Console.WriteLine(pass);
                    ConsoleKeyInfo inputKey = Console.ReadKey(true);
                isPass = false;
                if(inputKey.Key == ConsoleKey.Enter)
                {
                    if (input.Length == 6)
                    {
                        break;
                    }
                    else
                    {
                        PrintMessage("\nPlease enter 6 digits", false);
                        input.Clear();
                        isPass = true;
                        continue;
                    }
                    
                }

                if (inputKey.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input.Remove(input.Length - 1, 1);
                }
                else if(inputKey.Key != ConsoleKey.Backspace)
                {
                    input.Append(inputKey.KeyChar);
                    Console.Write(asterics + "*");
                }

            }
            return input.ToString();
        }
  
        public static string FormatAmount(float amt)
        {
            return String.Format(culture, "{0:C2}", amt);
        }
    
    
    }
}
