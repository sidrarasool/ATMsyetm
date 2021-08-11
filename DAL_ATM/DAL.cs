using Customer_ATM;
using Administrator_ATM;
using Trasaction_ATM;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DAL_ATM
{
    public class DAL: BaseDAL
    {
        public void SaveCustomer(Customer cstmr)
        {
            
         
            string text = $"{cstmr.CustomerLoginID.ToLower()},{cstmr.CustomerPinCode}," +
                $"{cstmr.Holders_name.ToLower()},{cstmr.Type.ToLower()},{cstmr.Balance},{cstmr.Status.ToLower()}," +
                $"{cstmr.AccountNO}";
            text = EncodeDecode(text);
            Save(text, "CustomerData.csv");
        }

        //converts list of customers into list of strings BaseDal can store the list
        public void ConvertCusToStr(List<Customer> cstmr)
        {
            List<string> data = new List<string>();
            foreach (Customer s in cstmr)
            {
                 string temp = $"{s.CustomerLoginID},{s.CustomerPinCode},{s.Holders_name},{s.Type},{s.Balance},{s.Status},{s.AccountNO}";
                temp = EncodeDecode(temp);
                 data.Add(temp);
            }

            SaveList(data, "CustomerData.csv");

        }



        public List<Customer> ReadCustomer()
        {
            string s;
            List<String> stringList = Read("CustomerData.csv");
            List<Customer> CustomerList = new List<Customer>();
            foreach (string s1 in stringList)
            {
                s = EncodeDecode(s1);
                string[] data = s.Split(",");
                Customer c = new Customer();
                c.CustomerLoginID = data[0];
                c.CustomerPinCode = System.Convert.ToInt32(data[1]);
                c.Holders_name = data[2].ToUpper();
                c.Type = (data[3]).ToLower();
                c.Balance = System.Convert.ToInt32(data[4]);
                c.Status = (data[5]).ToLower();
                c.AccountNO= System.Convert.ToInt32(data[6]);

                CustomerList.Add(c);
            }

            return CustomerList;

        }

        public void SaveAdmin(Admin ad)
        {
            string text = $"{ad.AdminLoginID},{ad.AdminPinCode},{ad.AdminName}";
            text = EncodeDecode(text);
            Save(text, "AdminData.csv");
        }



        public List<Admin> ReadAdmin()
        {
            string s;
            List<String> stringList = Read("AdminData.csv");
            List<Admin> AdminList = new List<Admin>();
            foreach (string s1 in stringList)
            {
                s=EncodeDecode(s1);
                string[] data = s.Split(",");
                Admin a = new Admin();
                a.AdminLoginID = data[0].ToLower();
                a.AdminPinCode = System.Convert.ToInt32(data[1]);
                a.AdminName = data[2].ToLower();
                

                AdminList.Add(a);
            }

            return AdminList;

        }



        public void SaveTransaction(Transactions t)
        {
            string date = Convert.ToString(t.Date);
            string text = $"{t.SenderAccNo},{t.ReceiverAccNo},{t.TransType},{t.Amount},{date}";
            Save(text, "Transactions.csv");
        }

        public List<Transactions> ReadTransactions()
        {
            
            List<String> stringList = Read("Transactions.csv");
            List<Transactions> TransactionList = new List<Transactions>();
            foreach (string s in stringList)
            {

                string[] data = s.Split(",");
                Transactions t = new Transactions();
                t.SenderAccNo = System.Convert.ToInt32(data[0]);
                t.ReceiverAccNo = System.Convert.ToInt32(data[1]);
                t.TransType= data[2].ToUpper();
                t.Amount = System.Convert.ToInt32(data[3]);
                t.Date = System.Convert.ToDateTime(data[4]);
              
                TransactionList.Add(t);
            }

            return TransactionList;
        }



        public string EncodeDecode(string input)
        {
            
            string output = "";
            foreach (char c in input)
            {
                if (c == ',')
                {
                    output += c;
                }
                if (c >= 'A' && c <= 'Z')
                {
                    if (c <= 'N')
                    {
                        output += Convert.ToChar((2*('N' - c)) + c-1);
                        
                    }
                    else if (c >= 'N')
                    {
                        output += Convert.ToChar(c- ( 2 * (c - 'N'))-1) ;
                    }

                }
                if(c >= '0' && c<= '9')
                {
                    if (c <= '4')
                    {
                        output += Convert.ToChar(2 * ('5' - c) + c-1);
                    }
                    else if (c >= '5')
                    {
                        output += Convert.ToChar(c - (2 * (c - '5'))-1);
                    }
                }
                if(c>= 'a' && c <= 'z')
                {
                    if (c <= 'n')
                    {
                        output += Convert.ToChar((2 * ('n' - c)) + c-1);
                    }
                    else if (c >= 'n')
                    {
                        output += Convert.ToChar(c - (2 * (c - 'n'))-1);
                    }
                }
            }
            return output;

        }



    }
}
