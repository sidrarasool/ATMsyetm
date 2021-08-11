using Customer_ATM;
using Administrator_ATM;
using BLL_ATM;
using System;
using System.Collections.Generic;
using System.Net.Sockets;


namespace View_ATM
{
    public class View 
    {
        //static int NoOfWrongInputs =0;
        public void SelectUser()
        {
        TakeInputAgain:
            Console.WriteLine("\n\n\nSelect one of the options." +
                "\nAdmin Users Press:        1" +
                "\nCustomer Users Press:     2");
            int UserOption = IntegerInput();
            if (UserOption == 1)
            {
                
                AdminLogin();
            }
            else if (UserOption == 2)
            {
                CustomerLogin();
            }
            else
            {
                Console.WriteLine("Invalid Option. Select an option again.");
                goto TakeInputAgain;
            }
        }






        // handle 3 times wrong input
        // hanlde capitalisation
        public void CustomerLogin()
        {
            //take input from the customer
            Console.WriteLine("\n\n\nEnter Login ID:");
            String LoginID = Console.ReadLine().ToLower();

            
            Console.WriteLine("Enter PIN CODE:");
            int PinCode = IntegerInput();
            Customer input = new Customer { CustomerLoginID = LoginID.ToLower(), CustomerPinCode=PinCode};

            //verify if the input credentials are valud or not.
            BLL obj = new BLL { };
            bool verification = obj.verifyCustomerCredentials(input);
            if (verification == true)
            {
                
                CustomerMenu temp=new CustomerMenu { }; 
                temp.showCustomerMenu(LoginID);
            }
            else
            {
                
                Console.WriteLine("Invalid Credentials. \nPlease Start over.");
                SelectUser();
            }

        }

        // hanlde capitalisation
        public void AdminLogin()
        {
            //take input from the customer
            Console.WriteLine("\n\n\nEnter Login ID:");
            String LoginID = Console.ReadLine().ToLower();
            Console.WriteLine("Enter PIN CODE:");
            //handle exception here
            int PinCode = IntegerInput();
            Admin input = new Admin { AdminLoginID =LoginID.ToLower(), AdminPinCode = PinCode };

            //verify if the input credentials are valud or not.
            BLL obj = new BLL { };
            bool verification = obj.verifyAdminCredenials(input);
            if (verification == true)
            {
                AdminMenu temp = new AdminMenu{ };
                temp.showAdminMenu();
            }
            else
            {
                Console.WriteLine("Invalid Credentials. \nPlease Start over.");
                SelectUser();
            }
        }




        //Takes input in strings and converts it into integer and handles all exceptions.
        int IntegerInput()
        {
            int input;
        enterAmountAgain:
            try
            {
                string tempInput = Console.ReadLine();
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





