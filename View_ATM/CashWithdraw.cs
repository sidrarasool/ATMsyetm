using System;
using BLL_ATM;
using Customer_ATM;
using Trasaction_ATM;
using System.Collections.Generic;
using System.Text;

namespace View_ATM
{
    internal class CashWithdraw
    {
        //shows options for cash withdrawing
        public void CashWithdrawMenu(string CustomerLoginID)
        {
        giveAnotheroption:
            Console.WriteLine("\n\na) Fast Cash" +
                  "\nb) Normal Cash" +
                  "\nPlease select a mode of withdrawal(a or b):");

            string input = StringInput().ToLower();

            switch (input)
            {
                case "a":

                    FastCash(CustomerLoginID);
                    break;
                case "b":
                EnterAnotherAmount:
                    Console.WriteLine("\n\nEnter the withdrawal amount: ");

                    int amount = IntegerInput();
                    if (amount > 20_000)
                    {
                        Console.WriteLine("\n\nYou Cannot Withdaw More than Rs. 20,000 in a day.");
                        Console.WriteLine("Please Enter Another Amount.");
                        goto EnterAnotherAmount;
                    }
                    cashwithdraw(amount, CustomerLoginID);
                    break;
                default:
                    Console.WriteLine("\n\nInvalid Option \nPlease Select an option Again.");
                    goto giveAnotheroption;

            }








            //Function to select cash in multiples of 500

            void FastCash(string CustomerLoginID)
            {

            selectoptionagain:
                Console.WriteLine("\n\n1----500" +
                    "\n2----1000" +
                    "\n3----2000" +
                    "\n4----5000" +
                    "\n5----10000" +
                    "\n6----15000" +
                    "\n7----20000");
                Console.WriteLine("\n\nSelect one of the denominations of money:");
                int option = IntegerInput();
                int Amount = 0;
                switch (option)
                {
                    case 1:
                        Amount = 500;
                        break;
                    case 2:
                        Amount = 1000;
                        break;
                    case 3:
                        Amount = 2000;
                        break;
                    case 4:
                        Amount = 5000;
                        break;
                    case 5:
                        Amount = 10000;
                        break;
                    case 6:
                        Amount = 15000;
                        break;
                    case 7:
                        Amount = 20000;
                        break;
                    default:
                        Console.WriteLine("\n\nInvalid Option \nPlease Select an option Again.");
                        goto selectoptionagain;

                }
                cashwithdraw(Amount, CustomerLoginID);
            }









            //generic function to withdraw cash and make changes in business logic and DAL.
            void cashwithdraw(int Amount, string CustomerLoginID)
            {
                //object created to call functions of business logic
                BLL obj = new BLL { };
                Console.WriteLine($"\n\nAre you sure you want to withdraw Rs.{Amount} (Y/N)?");
                string Cofirmation = StringInput().ToLower();
                Customer CurrCus = obj.getCustomer(CustomerLoginID);
                if (Cofirmation == "y")
                {
                    //checks if the Ballance is more than the amount to be withdrawn
                    Boolean SufficientBalance = obj.SuficientBalance(Amount, CustomerLoginID);
                    if (SufficientBalance == true)
                    {
                        // Boolean AmountWithdrawnToday = obj.AmountWithdrawnToday(Amount, CustomerLoginID);

                        //if (AmountWithdrawnToday == true)
                        //{
                        Transactions t=new Transactions { };
                        t.SenderAccNo = CurrCus.AccountNO;
                        t.ReceiverAccNo = -1;
                        t.Amount = Amount;
                        t.TransType = "Cash Withdraw";
                        t.Date = DateTime.Today;
                        obj.SaveTransaction(t);
                            Console.WriteLine("\n\nCash Successfully Withdrawn!" +
                            "\nDo you wish to print a receipt(Y / N) ? ");

                            string input = StringInput().ToLower();
                            if (input == "y")
                            {
                                Receipt(CustomerLoginID, Amount, "Withdrawn");
                            }

                            else
                            {
                                Console.WriteLine("Thank You!");
                                System.Environment.Exit(0);
                            }

                        //}
                        //else
                        //{
                        //    //Console.WriteLine("\n\nEither The Amount you have Entered is more than Rs. 20,000" +
                        //    //  "\nOR" +);
                        //    Console.WriteLine("\nYou Have Already withdrawn Rs. 20,000 Today." +
                        //        "\nSo, You can not withraw more cash until tomorrow. ");
                        //    Console.WriteLine("Thank YOU!");
                        //    System.Environment.Exit(0);

                        //}
                    }
                    else
                    {
                        Console.WriteLine("\n\nYour Balance is insufficient to Withdraw this amount. ");
                        Console.WriteLine("Thank YOU!");
                        System.Environment.Exit(0);
                    }

                }
                else if (Cofirmation == "n")
                {

                    Console.WriteLine("\n\nTo Select Another Amount, Press 1." +
                    "\nTo exit ATM SYSTEM, press anykey.");


                    int input = IntegerInput();
                    if (input == 1)
                    {
                        CashWithdrawMenu(CustomerLoginID);
                    }


                }
                else
                {
                    Console.WriteLine("\n\nInvalid Option \nPlease Select an option Again.");
                    FastCash(CustomerLoginID);
                }
            }
        }

        //Receipt. 
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
