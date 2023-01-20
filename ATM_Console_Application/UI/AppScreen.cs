using ATM_Console_Application.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using ATM_Console_Application.UI;

namespace ATM_Console_Application.UI
{
    public static class AppScreen
    {
        internal static void Welcome()
        {
            //     <Styles>
            //Clear everything before to start the app 
            Console.Clear();

            //Set the title for the application
            Console.Title = "ATM app";

            //Add colors to the text
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\n                                ------------------ WELCOME ------------------ \nPlease enter your ATM card and password and follow the instructions on the screen to continue with your transactions");
            Console.WriteLine("\nPress enter to continue\n");
            Console.ReadLine();
            Console.WriteLine("Please enter your ATM card ");

            Utility.PressEnterToContinue();
            //  </Styles>
        }
        internal static UserAccount UserLoginForm()
        {
            UserAccount tempUserAccount = new UserAccount();

            tempUserAccount.CardNumber = Validator.Convert<long>("Your card number.");

            //Remplace the input of cardPin for asterisk
            tempUserAccount.CardPin = Convert.ToInt32(Utility.GetSecretPass("Enter your password"));
            return tempUserAccount;


        }

        internal static void LoginProgress()
        {
            Console.WriteLine("\nChecking card number and PIN...");

            int timer = 10;

            for (int i = 0; i < timer; i++)
            {
                Console.WriteLine(".");
                Thread.Sleep(200);
            }

            Console.Clear();
        }
        internal static void Process(int timer = 19)
        {
            for (int i = 0; i < timer; i++)
            {
                Console.WriteLine(".");
                Thread.Sleep(200);
            }
        }
        internal static void PrintLockScreen()
        {
            Console.Clear();
            Utility.PrintMessage("Your account is locked.\nPlease go to the nearest bank to unlock tour account. Thank you :)", true);
            Utility.PressEnterToContinue();
            Environment.Exit(1);
        }

        internal static void WelcomeUser(string fullName)
        {
            Console.WriteLine($"Welcome back, {fullName}!");
        }

        internal static void DisplayAppMenu()
        {
            Console.Clear();
            Console.WriteLine("------------------ MY ATM MENU ------------------");
            Console.WriteLine("|1. Account Balance                             |");
            Console.WriteLine("|2. Cash Deposit                                |");
            Console.WriteLine("|3. Withdrawal                                  |");
            Console.WriteLine("|4. Log out                                     |");
            Console.WriteLine("-------------------------------------------------");
        }

        internal static void LogOut()
        {
            Console.WriteLine("Thank you for trust in ATM CONSOLE");
            Process();
            Console.Clear();
        }

        internal const string cur = "$ ";

        internal static int selectAmount()
        {
            Console.WriteLine("");
            Console.WriteLine(":1.{0}500        6.{0}3000",cur);
            Console.WriteLine(":2.{0}1000       7.{0}3500", cur);
            Console.WriteLine(":3.{0}1500       8.{0}4000", cur);
            Console.WriteLine(":4.{0}2000       9.{0}4500", cur);
            Console.WriteLine(":5.{0}2500       10.{0}5000", cur);
            Console.WriteLine(":0.Other");
            Console.WriteLine("");

            int selectdAmount = Validator.Convert<int>("Please choose an option:");
            switch (selectdAmount)
            {
                case 0: 
                    return 0; 
                    break;
                case 1: 
                    return 500; 
                    break;
                case 2: 
                    return 1000; 
                    break;
                case 3: 
                    return 1500; 
                    break;
                case 4: return 2000; 
                    break;
                case 5: 
                    return 2500; 
                    break;
                case 6: 
                    return 3000; 
                    break;
                case 7: 
                    return 3500; 
                    break;
                case 8: 
                    return 4000; 
                    break;
                case 9: 
                    return 4500; 
                    break;
                case 10: 
                    return 5000; 
                    break;
                default: 
                    Utility.PrintMessage("That is not an option", false);
                    selectAmount();
                    return -1;
                    break;
            }
        }
    }
}
