using Customer_ATM;
using Administrator_ATM;
using BLL_ATM;
using Trasaction_ATM;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace View_ATM
{
    public class CustomerMenu
    {
        internal void showCustomerMenu(string CustomerLoginID)
        {
        selectoptionagain:
            Console.WriteLine("\n\n\n1----Withdraw Cash" +
                "\n2----Cash Transfer" +
                "\n3----Deposit Cash" +
                "\n4----Display Balance" +
                "\n5----Exit" +
                "\n\nPlease select one of the above options: ");
            

            int option = IntegerInput();

            switch (option)
            {
                //cashWithdraw
                case 1:
                    CashWithdraw c = new CashWithdraw { };
                    c.CashWithdrawMenu(CustomerLoginID);
                    break;
                //Transfer Cash from 1 account to another
                case 2:
                    CashTransfer(CustomerLoginID);
                    break;
                //Deposite cash in the Account
                case 3:
                    CashDeposit(CustomerLoginID);
                    break;
                 // Display Balance
                case 4:
                    Receipt(CustomerLoginID,0,null);
                    break;
                case 5:
                    Console.WriteLine("\n\nThank You!");
                    System.Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("\n\nInvalid Option \nPlease Select an option Again.");
                    goto selectoptionagain;


            }

        }


        //Receipt. For Balance Receipt and to print Receipt after Cash wthdraw,transfer or deposit.
        void Receipt(string CustomerLoginID, int Amount, string option)
        {
            BLL obj = new BLL { };
            Customer temp = obj.getCustomer(CustomerLoginID);
            Console.WriteLine($"\n\nAccount #{temp.AccountNO}" +
                $"\nDate: {DateTime.Now}");
            if (option != null)
            {
                Console.WriteLine($"\n{option}: {Amount}");
            };
            Console.WriteLine($"Balance: {temp.Balance}");
        }







        //Cash Deposit
        void  CashDeposit(string CustomerLoginID)
        {
            //Created to use fucntions from business logic
            BLL temp = new BLL { };
            Console.WriteLine("\n\nEnter the cash amount to deposit:");
            int depositAmount = IntegerInput();
            temp.DepositeCash(CustomerLoginID, depositAmount);
            Console.WriteLine("\n\nCash Deposited Successfully." +
                "\nDo you wish to print a receipt(Y / N) ? ");
            string input = StringInput().ToLower();
            if (input == "y")
            {
                Receipt(CustomerLoginID, depositAmount, "Deposited:");
            }

            else
            {
                Console.WriteLine("\n\nThank You!");
                System.Environment.Exit(0);
            }
        }



        //CashTransfer
        void CashTransfer(string CustomerLoginID)
        {
            //Created to use fucntions from business logic
            BLL temp = new BLL { };
            enterAmountAgain:
            Console.WriteLine("\n\nEnter amount in multiples of 500: ");
            int Amount = IntegerInput();
            if (Amount % 500 != 0)
            {
                Console.WriteLine("\n\nThe given Amount is no a multiple of 500.");
                goto enterAmountAgain;
            }
            else
            {
                if (!temp.SuficientBalance(Amount,CustomerLoginID))
                {
                    Console.WriteLine("You Do not have Sufficient Balance to make this Transaction.");
                    Console.WriteLine("Thank You.");
                    System.Environment.Exit(0);
                }
                EnterAccountNoAgain:
                Console.WriteLine("\n\nEnter the account number to which you want to transfer:");
                int AccountNo = IntegerInput();
                Customer Receiver = temp.getCustomerAcc(AccountNo);
                Customer sender = temp.getCustomer(CustomerLoginID);
                if (Receiver!=null)
                {
                  Console.WriteLine($"\n\nYou wish to deposit Rs. {Amount} in account held by Mr./Mrs. " +
                  $"{Receiver.Holders_name} " +
                  $"If this information is correct please re-enter the account number: ");
                  int confirmAccountNo = IntegerInput();
                  if (AccountNo == confirmAccountNo) { 
                      temp.TranferCash(CustomerLoginID, AccountNo,Amount);
                        Transactions t = new Transactions { };
                        t.SenderAccNo = sender.AccountNO;
                        t.ReceiverAccNo = Receiver.AccountNO;
                        t.Amount = Amount;
                        t.TransType = "Cash Transfer";
                        t.Date = DateTime.Today;
                        temp.SaveTransaction(t);
                        Console.WriteLine("\n\nTransaction confirmed!");
                      Console.WriteLine("\n\nDo you wish to print a receipt (Y/N)? ");
                      string input = StringInput().ToLower();
                      if (input == "y")
                      {
                          Receipt(CustomerLoginID, Amount, "Amount Transferred:");
                      }

                      else
                      {
                         Console.WriteLine("\n\nThank You!");
                         System.Environment.Exit(0);
                      }
                  }
                  else
                  {
                      Console.WriteLine("\n\nThis Account Number is invalid." +
                      "\nIf you want to Enter another account Number enter 1." +
                       "\nIf you want to End this Program enter 2.");
                        int option = IntegerInput();
                        if (option == 1) { goto EnterAccountNoAgain; }
                        else { System.Environment.Exit(0); };
                  }
                    
                }
                else
                {
                    Console.WriteLine("\n\nThis Account Number is invalid." +
                    "\nIf you want to Enter another account Number enter 1." +
                    "\n If you want to End this Program enter 2.");
                    int option = IntegerInput();
                    if (option == 1) { goto EnterAccountNoAgain; }
                    else { System.Environment.Exit(0); };
                }


            }
           
        }
        //Takes String input. Handles all Exceptions
        string StringInput()
        {

        inputAgain:
            string s = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(s))
            {
                Console.WriteLine("\n\nYou cannot Leave this Space Blank." +
                    "\nEnter the Value here:");
                goto inputAgain;
            }

            return s;
        }


        //Takes input in strings and converts it into integer and handles all exceptions.
        int IntegerInput()
        {
            int input;
        enterAmountAgain:
            try
            {
                string tempInput = StringInput();
                input = Convert.ToInt32(tempInput);
            }
            catch
            {
                Console.WriteLine("\n\nWrong Input! Enter an integer Value.\n");
                goto enterAmountAgain;
            }
            return input;
        }

       

    }
}


