using System;
using Customer_ATM;
using BLL_ATM;
using System.Collections.Generic;
using System.Text;

namespace View_ATM
{
    class CreateAccount
    {
        BLL obj = new BLL { };
        internal void CreateNewAccount()
        {
            Customer NewAccount = new Customer { };

            Console.WriteLine("\n\nLogin:");
            NewAccount.CustomerLoginID = InputLoginID();

            Console.WriteLine("\nPin Code:");
            NewAccount.CustomerPinCode = NumbersOnly();

            Console.WriteLine("\nHolders Name:");
            NewAccount.Holders_name = InputHoldersName();

            Console.WriteLine("\nType (Savings,Current):");
            NewAccount.Type = InputType();

            Console.WriteLine("\nStarting Balance:");
            NewAccount.Balance = NumbersOnly();

            Console.WriteLine("\nStatus: ");
            NewAccount.Status = InputStatus();

            
            obj.AddNewAccount(NewAccount);
            


            //string temp = $"{NewAccount.CustomerLoginID},{NewAccount.CustomerPinCode},{NewAccount.Holders_name}" +
            //    $",{NewAccount.Type},{NewAccount.Balance},{NewAccount.Status},{NewAccount.AccountNO}";
            //Console.WriteLine(temp);
        }



        //returns a valid Status
        string InputStatus()
        {
        EnterStatusAgain:
            string Type = StringInput();
            if (AlphabetsOnly(Type.ToUpper()))
            {
                if (Type.ToLower() == "active" || Type.ToLower() == "disabled") { return Type.ToLower(); }
                else
                {
                    Console.WriteLine("\n\nWrong Input! Enter Either 'Active' or 'Disabled'." +
                    "\nEnter Status Again:");
                    goto EnterStatusAgain;
                }
            }
            else
            {
                Console.WriteLine("\n\nWrong Input! Enter Either 'Active' or 'Passive'." +
                    "\nEnter Status Again:");
                goto EnterStatusAgain;
            }
        }

        //returns a valid type
        string InputType()
        {
        EnterTypeAgain:
            string Type = StringInput().ToUpper();
            if (AlphabetsOnly(Type))
            {
                if (Type == "CURRENT" || Type == "SAVINGS")
                { return Type.ToLower(); }
                else
                {
                    Console.WriteLine("\n\nWrong Input! Enter Either 'Current' or 'Savings'." +
                    "\nEnter Type Again:");
                    goto EnterTypeAgain;
                }
            }
            else
            {
                Console.WriteLine("\n\nWrong Input! Enter Either 'Current' or 'Savings'." +
                     "\nEnter Type Again:");
                goto EnterTypeAgain;
            }
        }

        //returns a valid Holders Name
        string InputHoldersName()
        {
        EnterNameAgain:
            string Name = StringInput();
            if (AlphabetsOnly(Name.ToUpper()))
            {
                return Name.ToUpper();
            }
            else
            {
                Console.WriteLine("\n\nWrong Input! Enter Alphabets Only." +
                    "\nEnter Holder's Name Again:");
                goto EnterNameAgain;
            }
        }

        //returns a valid LoginID
        string InputLoginID()
        {
        EnterLoginIDAgain:
            string LoginID = StringInput();
            if (NumberOrAlphabet(LoginID.ToUpper()))
            {
               if(obj.loginIDExist(LoginID.ToLower())) {
                    Console.WriteLine("\nThis Login ID already Exists. Please Enter another Login ID.");
                    goto EnterLoginIDAgain;
                }
                return LoginID.ToLower();
            }
            else
            {
                Console.WriteLine("\n\nWrong Input! Enter Alphabets and Numbers Only." +
                    "\nEnter LoginID Again:");
                goto EnterLoginIDAgain;
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
        int NumbersOnly()
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
