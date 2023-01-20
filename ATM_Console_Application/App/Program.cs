using ATM_Console_Application.Domain.Entities;
using ATM_Console_Application.Domain.Entities.Interfaces;
using ATM_Console_Application.Domain.Enums;
using ATM_Console_Application.UI;
using System;
using System.Collections.Generic;
using System.Security;

namespace ATM_Console_Application.App
{
    public class Program : IUserLogin, IUserAccountActions, ITransaction
    {
        private List<UserAccount> UserAccountsList;
        private UserAccount selectedAccount;
        private List<Transaction> _listOfTransactions;
        private const int miniKeptAmount = 500;
        public void Run()
        {
            AppScreen.Welcome();
            CheckUserCardNumAndPass();
            AppScreen.WelcomeUser(selectedAccount.FullName);
            AppScreen.DisplayAppMenu();
            ProcessMenuOptions();
        }


        public void InitializeData()
        {
            UserAccountsList = new List<UserAccount>
            {
                new UserAccount
                {
                        Id = 1,
                        FullName = "Jesus David Varela Melendez",
                        AccountNumber = 123456,
                        CardNumber = 9876,
                        CardPin = 200520,
                        AccountBalance = 100000.10F,
                        IsLocked = false
                },

                 new UserAccount
                 {
                        Id = 2,
                        FullName = "Homer Jay Simpson",
                        AccountNumber = 654321,
                        CardNumber = 1234567899,
                        CardPin = 199900,
                        AccountBalance = 280000.00F,
                        IsLocked = false
                 },

                  new UserAccount
                  {
                        Id = 3,
                        FullName = "Walter Hartwell White",
                        AccountNumber = 123123,
                        CardNumber = 0123456789,
                        CardPin = 202222,
                        AccountBalance = 1000000.00F,
                        IsLocked = true
                  },

                   new UserAccount
                   {
                        Id = 4,
                        FullName = "Rick Daniel Sanchez",
                        AccountNumber = 321321,
                        CardNumber = 3213214568,
                        CardPin = 392023,
                        AccountBalance = 200.60F,
                        IsLocked = true
                   }
            };
            _listOfTransactions = new List<Transaction>();

        }

        public void CheckUserCardNumAndPass()
        {
            bool isCorrectLogin = false;
            while (isCorrectLogin == false)
            {
                UserAccount inputAccount = AppScreen.UserLoginForm();
                AppScreen.LoginProgress();
                foreach (UserAccount account in UserAccountsList)
                {
                    selectedAccount = account;
                    if (inputAccount.CardNumber.Equals(selectedAccount.CardNumber))
                    {
                        selectedAccount.TotalLogin++;
                        if (inputAccount.CardPin.Equals(selectedAccount.CardPin))
                        {
                            selectedAccount = account;
                            if (selectedAccount.IsLocked || selectedAccount.TotalLogin > 3)
                            {
                                //Print a lock message
                                AppScreen.PrintLockScreen();
                            }
                            else
                            {
                                selectedAccount.TotalLogin = 0;
                                isCorrectLogin = true;
                                break;
                            }
                        }
                    }

                    if (isCorrectLogin == false)
                    {
                        Utility.PrintMessage("\n Invalid card number or PIN", false);
                        selectedAccount.IsLocked = selectedAccount.TotalLogin == 3;
                        if (selectedAccount.IsLocked)
                        {
                            AppScreen.PrintLockScreen();
                        }

                    }
                    Console.Clear();

                }

            }

        }

        private void ProcessMenuOptions()
        {      
            switch(Validator.Convert<int>("Please choose an option:"))
            {
                case (int)AppMenu.CheckBalance:
                    CheckBalance();
                    break;

                case (int)AppMenu.PlaceDeposit:
                    PlaceDeposit();
                    break;

                case (int)AppMenu.MakeWithdrawal:
                    MakeWithdrawal();
                    break;
                case (int)AppMenu.Logout:
                    AppScreen.LogOut();
                    Utility.PrintMessage("You have successfully logged out. Please remove your card ", true);
                    Run();
                    break;

                default:
                    Utility.PrintMessage("Please enter a correct option", false);
                    break;
            }
        }

        public void CheckBalance()
        {
            Utility.PrintMessage($"Your account balance is: {Utility.FormatAmount(selectedAccount.AccountBalance)}");
        }

        public void PlaceDeposit()
        {
            Console.WriteLine("\nOnly multiples of 1000 and 500 dollars allowed.\n");
            var transaction_AMT = Validator.Convert<int>($"Amount {AppScreen.cur}");


            //simulate counting
            Console.WriteLine("\nChecking and Counting bank notes");
            AppScreen.Process();
            Console.WriteLine("");

            //Some gaurd clause
            if(transaction_AMT <= 0)
            {
                Utility.PrintMessage("Amount needs to be grater than 0. Try again later", false);
                return;
            }
            if(transaction_AMT % 500 != 0)
            {
                Utility.PrintMessage("Enter a value in multiples of 500 or 100.\nTry again.", false);
                return;
            }
            if (PreviewBankNotesCount(transaction_AMT) == false)
            {
                Utility.PrintMessage($"You've cancelled your action", false);
                return;
            }
            //Bind transaction details
            InsertTransaction(selectedAccount.Id, TransactionType.Deposit, transaction_AMT, "");

            //Update account balance
            selectedAccount.AccountBalance += transaction_AMT;

            //print success
            Utility.PrintMessage($"Your deposit of {Utility.FormatAmount(transaction_AMT)} was successful", true);
        }

        public void MakeWithdrawal()
        {
            var transaction_amt = 0;
            int selectedAmount = AppScreen.selectAmount();

            if (selectedAmount == -1)
            {
                selectedAmount = AppScreen.selectAmount();
            } 
            else if (selectedAmount != 0)
            {
                transaction_amt = selectedAmount;
            }
            else
            {
                transaction_amt = Validator.Convert<int>($"Amount{AppScreen.cur}"); 
            }

            if(transaction_amt <= 0)
            {
                Utility.PrintMessage("Amount needs to be greater than 0", false);
                return;
            }

            if(transaction_amt % 500 != 0)
            {
                Utility.PrintMessage("You can only withdraw amount in multiples of 500 or 1000", false);
                return;
            }

            if(transaction_amt > selectedAccount.AccountBalance) 
            {
                Utility.PrintMessage("Withdraw failed. Try again", false);
                return;
            }

            if ((selectedAccount.AccountBalance - transaction_amt) < miniKeptAmount)
            {
                Utility.PrintMessage($"Withdrawal failed. Your account needs to have minimun{Utility.FormatAmount(miniKeptAmount)}", false);
            }

            //bind
            InsertTransaction(selectedAccount.Id,TransactionType.Withdrawal, -transaction_amt,"");

            // Update account
            selectedAccount.AccountBalance -= transaction_amt;

            Utility.PrintMessage($"You've successfully withdrawn {Utility.FormatAmount(transaction_amt)}.", true);

        }

        private bool PreviewBankNotesCount(int amount)
        {
            int ThousandNotesCount = amount / 1000;
            int oneHundredNotesCount = (amount % 1000) / 500;
            Console.WriteLine("\nSummary");
            Console.WriteLine("-----------");
            Console.WriteLine($"{AppScreen.cur}1000 X {ThousandNotesCount} = {1000 * ThousandNotesCount}");
            Console.WriteLine($"{AppScreen.cur}500 X {oneHundredNotesCount} = {500 * oneHundredNotesCount}");
            Console.WriteLine($"Total amount: {Utility.FormatAmount(amount)}\n\n");
            int opti = Validator.Convert<int>("Enter 1 to confirm");
            return opti.Equals(1);
        }
        public void InsertTransaction(long _UserBankAccountId, TransactionType _tranType, decimal _tranAmount, string _desc)
        {
            //Create a new object
            var transaction = new Transaction()
            {
                TransactionID = Utility.getTransactionID(),
                UserBankAccountID = _UserBankAccountId,
                TransaactionDate = DateTime.Now,
                TransactionType = _tranType,
                TransactionAmount = _tranAmount,
                Description = _desc
            };
            //Add transaction obj
            _listOfTransactions.Add(transaction);
        }

        public void ViewTransaction()
        {

        }

    }
    
}
