using System;
using View_ATM;
using Customer_ATM;
using DAL_ATM;
using Administrator_ATM;
using System.Collections.Generic;
namespace ATM
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\t\t------------------");
            Console.WriteLine("\t\t\t\t  Welcome to ATM");
            Console.WriteLine("\t\t\t\t------------------");


            View obj = new View { };
            obj.SelectUser();

            //Customer c1 = new Customer { };
            //c1.CustomerLoginID = "eshaal";
            //c1.CustomerPinCode = 12345;
            //c1.Holders_name = "eshaal";
            //c1.Type = "Savings";
            //c1.Balance = 26000;
            //c1.Status = "Active";
            //c1.AccountNO = 1;

            //Customer c2 = new Customer { };
            //c2.CustomerLoginID = "rayyan";
            //c2.CustomerPinCode = 12345;
            //c2.Holders_name = "rayyan";
            //c2.Type = "Current";
            //c2.Balance = 25000;
            //c2.Status = "Active";
            //c2.AccountNO = 2;

            //Customer c3 = new Customer { };
            //c3.CustomerLoginID = "sidra";
            //c3.CustomerPinCode = 12345;
            //c3.Holders_name = "sidra";
            //c3.Type = "Savings";
            //c3.Balance = 30000;
            //c3.Status = "Active";
            //c3.AccountNO = 3;

            //Customer c4 = new Customer { };
            //c4.CustomerLoginID = "ishaan";
            //c4.CustomerPinCode = 12345;
            //c4.Holders_name = "ishaan";
            //c4.Type = "Current";
            //c4.Balance = 60000;
            //c4.Status = "Active";
            //c4.AccountNO = 4;

            //Customer c5 = new Customer { };
            //c5.CustomerLoginID = "ali";
            //c5.CustomerPinCode = 12345;
            //c5.Holders_name = "Ali";
            //c5.Type = "Current";
            //c5.Balance = 78000;
            //c5.Status = "Active";
            //c5.AccountNO = 5;

            //DAL save = new DAL { };
            ////int s = save.getAccountNo();
            ////Console.WriteLine(s);
            //save.SaveCustomer(c1);
            //save.SaveCustomer(c2);
            //save.SaveCustomer(c3);
            //save.SaveCustomer(c4);
            //save.SaveCustomer(c5);


            //Admin a1 = new Admin { AdminLoginID = "bashir", AdminPinCode = 67890, AdminName = "bashir" };

            //Admin a2 = new Admin { AdminLoginID = "Shazia", AdminPinCode = 67890, AdminName = "Shazia" };
            //Admin a3 = new Admin { AdminLoginID = "Karam", AdminPinCode = 67890, AdminName = "Karam" };

            //save.SaveAdmin(a1);
            //save.SaveAdmin(a2);
            //save.SaveAdmin(a3);



            //Console.WriteLine("Input string:");
            //string s = Console.ReadLine();
            //Console.WriteLine(save.EncodeDecode(s));



        }
    }
}
