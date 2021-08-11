using Customer_ATM;
using BLL_ATM;
using Trasaction_ATM;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Net.Http.Headers;
using System.Transactions;

namespace View_ATM
{
    class AdminMenu
    {
        //created to interact with the business logic layer and use its functions
        BLL obj = new BLL { };
        
        internal void showAdminMenu()
        {
            Console.WriteLine("\n\n\n1----Create New Account." +
                "\n2----Delete Existing Account." +
                "\n3----Update Account Information." +
                "\n4----Search for Account." +
                "\n5----View Reports." +
                "\n6----Exit" +
                "\n\nPlease select one of the above options: ");

        //handle exception if the input is not in int
        selectoptionagain:
            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    CreateAccount NewAccount = new CreateAccount { };
                    NewAccount.CreateNewAccount();
                    break;
                case 2:
                    DeleteAccount();
                    break;
                case 3:
                    UpdateAccount();
                    break;
                case 4:
                    Search();
                    break;
                case 5:
                    ViewReport();
                    break;
                case 6:
                    System.Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("\n\nInvalid Option \nPlease Select an option Again.");
                    goto selectoptionagain;

            }
        }




        void ViewReport()
        {
            Console.WriteLine("\n\n1---Accounts By Amount " +
                "\n2---Accounts By Date");
            optionAgain:
            int option = IntegerInput();
            switch (option)
            {
                case 1:
                    ByAmount();
                    break;
                case 2:
                    ByDate();
                    break;
                default:
                    Console.WriteLine("Invalid Option. \nSelect an option again:");
                    goto optionAgain;
            }
               
        }

        void ByAmount()
        {
            Console.WriteLine("\n\nEnter the minimum amount:");
            int minBalance = IntegerInput();
            Console.WriteLine("\nEnter the maximum amount: ");
            int maxBalance = IntegerInput();

            List<Customer> list = obj.SearchbyAmount(minBalance,maxBalance);
            PrintResult(list);

        }
        void ByDate()
        {
            DateTime mindate = new DateTime();
            DateTime maxdate = new DateTime();
            Console.WriteLine("\n\nEnter the Account Number: ");
            int AccountNo = IntegerInput();

            Console.WriteLine("\nEnter the starting date: ");
            mindate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("\nEnter the starting date: ");
            maxdate = DateTime.Parse(Console.ReadLine());

            List<Transactions> list = obj.SearchbyDate(mindate,maxdate,AccountNo);

            Console.WriteLine("\n\n==== SEARCH RESULTS ======");
            string s = String.Format("{0,-20}{1,-15}{2,-13}{3,-10}{4,15}\n", "Transaction Type", "Login ID", "Holders Name", "Amount", "Date");
            Console.WriteLine(s);

            foreach(Transactions t in list)
            {
                string s1;
                Customer sender = obj.getCustomerAcc(t.SenderAccNo);
                Customer Receiver = obj.getCustomerAcc(t.ReceiverAccNo);
                if (t.ReceiverAccNo == -1)
                {
                    s1 = String.Format("{0,-20}{1,-15}{2,-13}{3,-10}{4,15}\n", $"{t.TransType}", $"{sender.AccountNO}", $"{sender.Holders_name}", $"{t.Amount}", $"{t.Date}");
                    Console.WriteLine(s1);
                }
                else
                {
                    s1 = String.Format("{0,-20}{1,-15}{2,-13}{3,-10}{4,15}\n", $"{t.TransType}", $"{Receiver.AccountNO}", $"{Receiver.Holders_name}", $"{t.Amount}", $"{t.Date}");
                    Console.WriteLine(s1);
                }
                
            }
            System.Environment.Exit(0);
        }






        //Update Account

        void UpdateAccount()
        {
           Console.WriteLine("\n\nEnter the Account Number:");
           int AccountNo = IntegerInput();
           Customer customer = obj.getCustomerAcc(AccountNo);
            if (customer == null)
            {
                Console.WriteLine("\n\nNo Account with this Account Number Exists.");
                Console.WriteLine("\nThank You!");
                System.Environment.Exit(0);
            }

            Console.WriteLine($"\n\nAccount # {customer.AccountNO}" +
               $"\nType: {customer.Type}" +
               $"\nHolder: Mr./Ms. {customer.Holders_name}" +
               $"\nBalance: {customer.Balance}" +
               $"\nStatus: {customer.Status}");
           Console.WriteLine("Please enter in the fields you wish to update (leave \nblank otherwise):");

            Customer NewCustomer = customer ;

            LoginInput(ref NewCustomer, customer);
            StatusInput(ref NewCustomer, customer);
            PincodeInput(ref NewCustomer, customer);
            HoldersNameInput(ref NewCustomer, customer);


            obj.ModifyAndSaveData(NewCustomer);
            Console.WriteLine("\nYour account has been successfully been updated.");
            System.Environment.Exit(0); 
        }



        void Search()
        {
            Console.WriteLine("\n\nSearch Menu");
            Customer customer = new  Customer { };
            customer.CustomerLoginID = "";
            customer.AccountNO = -1;
            customer.Balance = -1;
            customer.CustomerPinCode = -1;
            customer.Holders_name = "";
            customer.Status = "";
            customer.Type = "";

            //sets value in the new variable according to the user input.
            Customer NewCustomer = new Customer { };
            AccountNoInput(ref NewCustomer, customer);
            LoginInput(ref NewCustomer, customer);
            HoldersNameInput(ref NewCustomer, customer);
            TypeInput(ref NewCustomer, customer);
            BalanceInput(ref NewCustomer, customer);
            StatusInput(ref NewCustomer, customer);

            //Console.WriteLine($"{NewCustomer.AccountNO},{NewCustomer.CustomerLoginID},{NewCustomer.Holders_name},{NewCustomer.Type},{NewCustomer.Balance},{NewCustomer.Status}");
            List<Customer> list = obj.Search(NewCustomer);
            PrintResult(list);
          
            System.Environment.Exit(0);
        }

        //Print Results of Search Function and Report Function
        void PrintResult(List<Customer> list)
        {
            Console.WriteLine("\n\n==== SEARCH RESULTS ======");
            string s = String.Format("{0,-11}{1,-15}{2,-13}{3,-9}{4,-10}{5,-8}\n", "Account No", "Login ID", "Holders Name", "Type", "Balance", "Status");
            Console.WriteLine("\n\n" + s);
            foreach (Customer c in list)
            {
                string s1 = String.Format("{0,-11}{1,-15}{2,-13}{3,-9}{4,-10}{5,-8}\n", $"  {c.AccountNO}", $"{c.CustomerLoginID}", $"{c.Holders_name}", $"{c.Type}", $"{c.Balance}", $"{c.Status}");
                Console.WriteLine(s1);
            }
        }



        //AccountNo Input for search
        void AccountNoInput(ref Customer NewCustomer, Customer customer)
        {
            Console.WriteLine("\nAccount No:");
            string temp = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(temp))
            {
                NewCustomer.AccountNO = customer.AccountNO;
            }
            else
            {
                NewCustomer.AccountNO = NumbersOnly(temp);
            }
        }


        //Balance Input for update and search
        void BalanceInput(ref Customer NewCustomer, Customer customer)
        {
            Console.WriteLine("\nBalance:");
            string temp = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(temp))
            {
                NewCustomer.Balance = customer.Balance;
            }
            else
            {
                NewCustomer.Balance = NumbersOnly(temp);
            }
        }


        //holder's Name Input for update and search
        void HoldersNameInput(ref Customer NewCustomer, Customer customer)
        {
        
        NameAgain:
            Console.WriteLine("\nHolder's Name:");
            string temp = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(temp))
            {
                NewCustomer.Holders_name = customer.Holders_name;
            }
            else
            {
                if (!AlphabetsOnly(temp.ToUpper()))
                {
                    Console.WriteLine("\n\nWrong Input! Enter Alphabets Only.");
                    goto NameAgain;
                }
                NewCustomer.Holders_name = temp.ToUpper();


            }
        }


        //Pin Code Input for update and search
        void PincodeInput(ref Customer NewCustomer, Customer customer)
        {
            Console.WriteLine("\nPin Code:");
            string temp = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(temp))
            {
                NewCustomer.CustomerPinCode = customer.CustomerPinCode;
            }
            else
            {
                NewCustomer.CustomerPinCode = NumbersOnly(temp);
            }
        }


        //Status Input for update and search
        void StatusInput(ref Customer NewCustomer, Customer customer)
        {
            string temp;
        
        StatusAgain:
            Console.WriteLine("\nStatus:");
            temp = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(temp))
            {
                NewCustomer.Status = customer.Status;
            }
            else
            {
                if (temp.ToLower() == "active" || temp.ToLower() == "disabled")
                {
                    NewCustomer.Status = temp.ToLower();

                }
                else
                {
                    Console.WriteLine("\n\nWrong Input! Enter Either 'Active' or 'Disabled'.");
                    goto StatusAgain;
                }

            }
        }

        //Type Input for update and search
        void TypeInput(ref Customer NewCustomer, Customer customer)
        {
            string temp;

        StatusAgain:
            Console.WriteLine("\nType(Current,Savings):");
            temp = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(temp))
            {
                NewCustomer.Type = customer.Type;
            }
            else
            {
                if (temp.ToLower() == "savings" || temp.ToLower() == "current")
                {
                    NewCustomer.Type = temp.ToLower();

                }
                else
                {
                    Console.WriteLine("\n\nWrong Input! Enter Either 'Savings' or 'Current'.");
                    goto StatusAgain;
                }

            }
        }


        //Login Input for update and search
        void LoginInput(ref Customer NewCustomer,Customer customer)
        {

            string temp;

        LoginAgain:
            Console.WriteLine("\nLogin:");
            temp = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(temp))
            {
                NewCustomer.CustomerLoginID = customer.CustomerLoginID;
            }
            else
            {
                if (!NumberOrAlphabet(temp.ToUpper()))
                {
                    Console.WriteLine("\n\nWrong Input! Enter Alphabets and Numbers Only.");
                    goto LoginAgain;
                }
                if (obj.loginIDExist(temp.ToLower()))
                {
                    Console.WriteLine("\nThis Login ID already Exists. Please Enter another Login ID.");
                    goto LoginAgain;
                }
                NewCustomer.CustomerLoginID = temp;

            }

        }


        //returns true if the string has alphabets only
        bool AlphabetsOnly(string s)
        {
            foreach (char c in s)
            {
                if (c < 65 || c > 90) { return false; }
            }
            return true;
        }

        //retruns true if the string has alphabets and numerics
        bool NumberOrAlphabet(string s)
        {
            foreach (char c in s)
            {
                if (c < 48 || (c > 57 && c < 65) || c > 90) { return false; }
            }
            return true;
        }

        //takes input and return it only when it has numbers only.
        int NumbersOnly(string input)
        {
            int output;
        enterInputAgain:
            try
            {
          
                output = Convert.ToInt32(input);
            }
            catch
            {
                Console.WriteLine("\n\nWrong Input! Enter Numbers Only.\n");
                goto enterInputAgain;
            }
            return output;
        }














        // Deletes The account based on the Account Number.
        void DeleteAccount()
        {
            EnterAccountNoAgain:
            Console.WriteLine("\n\nEnter the account number to which you want to delete:");
            int AccountNo = IntegerInput();

            Customer customer = obj.getCustomerAcc(AccountNo);
            if (customer == null)
            {
                Console.WriteLine("\n\nNo Account with this Account Number Exists.");
                Console.WriteLine("\nThank You!");
                System.Environment.Exit(0);
            }

            Console.WriteLine($"\n\nYou wish to delete the account held by Mr/Ms. {customer.Holders_name}; If " +
                $"this information is correct please \nre-enter the account " +
                $"number:");
            int confirmAccountNo = IntegerInput();
            if (AccountNo == confirmAccountNo)
            {
                obj.DeleteAcccount(AccountNo);
                Console.WriteLine("\n\nAccount Deleted Successfully.");
                Console.WriteLine("\nThank You!");
                System.Environment.Exit(0);
            }
            else
            {
                Console.WriteLine($"\n\nAccount Number {confirmAccountNo} does not belong to Mr./Ms.{customer.Holders_name}" +
                    $"\nIf you wish to Enter Account Number, Press 1" +
                    $"\nIf yout wish to Exit, Press any other key.");
                string input = StringInput();
                if (input == "1") { goto EnterAccountNoAgain; }
                else { System.Environment.Exit(0); }
            }
        }

        //takes input and return it only when it has numbers only.
        int IntegerInput()
        {
            int input;
        enterInputAgain:
            try
            {
                string tempInput = StringInput();
                input = Convert.ToInt32(tempInput);
            }
            catch
            {
                Console.WriteLine("\n\nWrong Input! Enter Numbers Only.\n");
                goto enterInputAgain;
            }
            return input;
        }

        //Takes String input from the User. handles all exceptions
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


    }
}
