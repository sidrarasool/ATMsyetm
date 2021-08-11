
using Customer_ATM;
using DAL_ATM;
using Administrator_ATM;
using Trasaction_ATM;
using System;
using System.Collections.Generic;
using System.Transactions;
using Microsoft.VisualBasic;

namespace BLL_ATM
{
    public class BLL
    {
        //this variabel keeps record of the amount of cash withdrawn in a day
        static int CashWithdrawn;
        //keeps record of the date to check the amount of cash withdrawn on hat date.
        static DateTime CashWithdrawDate = DateTime.Today;

        //temporary project to use functions fo DAL
        DAL temp = new DAL { };


        public bool verifyCustomerCredentials(Customer obj)
        {
             
            List<Customer> ListOfAllCustomers = temp.ReadCustomer();
            foreach (Customer s in ListOfAllCustomers)
            {
                if (obj.CustomerLoginID == s.CustomerLoginID && obj.CustomerPinCode == s.CustomerPinCode)
                    return true;
            }
            return false;

        }
        public bool verifyAdminCredenials(Admin obj)
        {
            List<Admin> ListOfAllAdmins = temp.ReadAdmin();
            foreach (Admin a in ListOfAllAdmins)
            {
                if (obj.AdminLoginID == a.AdminLoginID && obj.AdminPinCode == a.AdminPinCode)
                    return true;
            }
            return false;
        }

   
        
        // checks if WithDraw Amount less than Balance
        public bool SuficientBalance(int amount, string customerLoginID)
        {
          
            Customer customer = getCustomer(customerLoginID);
            
            if (amount < customer.Balance)
            {
                customer.Balance = customer.Balance - amount;
                ModifyAndSaveData(customer);
                return true;
            }
            return false;


        }

        //checks if the total amout of cash withdrawn in one day is less than 20,000 returns true if yes.
        public Boolean AmountWithdrawnToday(int amount, string customerLoginID)
        {
            Customer customer = getCustomer(customerLoginID);
            DateTime currentDate = DateTime.Today;
            if (CashWithdrawDate == currentDate  )
            {
                if (amount + CashWithdrawn <= 20_000)
                {
                    CashWithdrawn = CashWithdrawn + amount;
                    customer.Balance = customer.Balance - amount;
                    ModifyAndSaveData(customer);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            CashWithdrawDate = DateTime.Today;
            CashWithdrawn = amount;
            return true;
            
        }


        //get Customer using loginID
        public Customer getCustomer(string loginID)
        {
            
            List<Customer> ListOfAllCustomers = temp.ReadCustomer();
            foreach (Customer a in ListOfAllCustomers)
            {
                if (a.CustomerLoginID == loginID)
                    return a;
            }
            return null;
        }

        //get Customer using AccountNumber
        public Customer getCustomerAcc(int AccountNo)
        {
            DAL data = new DAL { };
            List<Customer> ListOfAllCustomers = data.ReadCustomer();
            foreach (Customer a in ListOfAllCustomers)
            {
                if (AccountNo == a.AccountNO)
                    return a;
            }
            return null;
        }
       

        //CustomerLoginID is of the person tranferring the money. AccountNo belongs to the recipient.
        public void TranferCash(string CustomerLoginID, int AccountNo, int Amount)
        {

            Customer sender = getCustomer(CustomerLoginID);
            Customer receiver = getCustomerAcc(AccountNo);
            sender.Balance = sender.Balance - Amount;
            receiver.Balance = receiver.Balance + Amount;
            ModifyAndSaveData(sender);
            ModifyAndSaveData(receiver);

        }
        //Deposit Cash into a Customer's Account
        public void DepositeCash(string CustomerLoginID,int depositAmount)
        {
            Customer customer = getCustomer(CustomerLoginID);
            customer.Balance = customer.Balance + depositAmount;
            ModifyAndSaveData(customer);
        }




        //Finds object of customer with that same login id as that in the parameter and makes chnages
        public void ModifyAndSaveData(Customer cstmr)
        {
            List<Customer> list = temp.ReadCustomer();
            List<Customer> NewList = new List<Customer>();
            foreach (Customer s in list)
            {

                if (cstmr.AccountNO == s.AccountNO)
                {
                    s.CustomerLoginID = cstmr.CustomerLoginID.ToLower();
                    s.CustomerPinCode = cstmr.CustomerPinCode;
                    s.Holders_name = cstmr.Holders_name.ToUpper();
                    s.Status = cstmr.Status.ToLower();
                    s.Type = cstmr.Type.ToLower();
                    s.Balance = cstmr.Balance;
                    //string temp = $"{s.CustomerLoginID},{s.CustomerPinCode},{s.Holders_name},{s.Type},{s.Balance},{s.Status},{s.AccountNO}";
                    NewList.Add(s);

                }
                else
                {
                    //string temp = $"{s.CustomerLoginID},{s.CustomerPinCode},{s.Holders_name},{s.Type},{s.Balance},{s.Status},{s.AccountNO}";
                    NewList.Add(s);

                }
            }
           
            temp.ConvertCusToStr(NewList);

        }

        // store trasactio in the file
        public void SaveTransaction(Transactions t)
        {
            temp.SaveTransaction(t);
        }


















        //Admin Functions

        public List<Transactions> SearchbyDate(DateTime mindate, DateTime maxdate, int AccountNo)
        {
            List<Transactions> list = temp.ReadTransactions();
            List<Transactions> NewList = new List<Transactions>();

            foreach(Transactions t in list)
            {
                if(t.Date>=mindate && t.Date <= maxdate && t.SenderAccNo==AccountNo )
                {
                    NewList.Add(t);
                }
            }
            return NewList;
        }


        //Search accounts within the given balance range for report function
        public List<Customer> SearchbyAmount(int minBalance,int maxBalance)
        {
            List<Customer> list = temp.ReadCustomer();
            List<Customer> NewList = new List<Customer>();

            foreach (Customer c in list)
            { 
                if(c.Balance >= minBalance && c.Balance <= maxBalance)
                {
                    NewList.Add(c);
                }
            }
            return NewList;
        }


        //Returns List of the searched elements.
        public List<Customer> Search(Customer customer)
        {
            List<Customer> list = temp.ReadCustomer();
            List<Customer> NewList = new List<Customer>();


            foreach (Customer s in list)
            {
                Customer c = new Customer { };
                c.AccountNO = s.AccountNO;
                c.CustomerLoginID = s.CustomerLoginID;
                c.Holders_name = s.Holders_name;
                c.Type = s.Type;
                c.Status = s.Status;
                c.Balance = s.Balance;

                if (customer.AccountNO == -1) { c.AccountNO = -1; };
                if (customer.CustomerLoginID == "") { c.CustomerLoginID = ""; }
                if (customer.Holders_name == "") { c.Holders_name = ""; }
                if (customer.Type == "") { c.Type = ""; }
                if (customer.Balance == -1) { c.Balance = -1; }
                if (customer.Status == "") { c.Status = ""; }

                if (customer.AccountNO == c.AccountNO && customer.CustomerLoginID == c.CustomerLoginID
                  && customer.Holders_name == c.Holders_name && customer.Type == c.Type
                  && customer.Balance == c.Balance && customer.Status == c.Status
                  && !IsExist(NewList, s))
                {
                    NewList.Add(s);
                }
            }
            return NewList;
        }









        //checks if the given customer object exists in teh given list
        bool IsExist(List<Customer> list, Customer c)
        {
            foreach(Customer s in list)
            {
                if (c.AccountNO == s.AccountNO) { return true; }
            }
            return false;
        }

        //Deletes the account and returns true when the job is done
        public void DeleteAcccount(int AccountNo)
        {
            List<Customer> list = temp.ReadCustomer();
            List<Customer> Newlist = new List<Customer>();
            foreach (Customer c in list)
            {
                
                if (c.AccountNO == AccountNo) {  }
                else { Newlist.Add(c);  };
            }
           
           
            temp.ConvertCusToStr(Newlist);

        }


        //Save A new Customer Account into the file
        public void AddNewAccount(Customer NewAccount)
        {
            if (NewAccount.AccountNO == 0)
            { NewAccount.AccountNO = getAccountNo(); }
            DAL obj = new DAL { };
            obj.SaveCustomer(NewAccount);
        }

        int getAccountNo()
        {
            List<Customer> list = temp.ReadCustomer();
            if ( list.Count==0)
            {
                return 1;
            }
            int size = list.Count;
            int AccountNo = list[size - 1].AccountNO + 1;
            return AccountNo;
        }
        //checks if the login ID already exists
        public bool loginIDExist(string ID)
        {
            List<Customer> customers = temp.ReadCustomer();
            foreach(Customer c in customers)
            {
                if (c.CustomerLoginID == ID) { return true; }
            }
            return false;
        }
    }
}
