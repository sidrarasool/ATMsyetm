using System;
using Customer_ATM;
using Administrator_ATM;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DAL_ATM
{
    public class BaseDAL
    {
        //saves one record in append mode
        internal void Save(string text, string fileName)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory,
                fileName);
            StreamWriter sw = new StreamWriter(filePath, append: true);
            sw.WriteLine(text);
            sw.Close();

        }
        
        //Saves entire data and overwrites previous data
        internal void SaveList(List<string> text, string fileName)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, fileName);
            File.Delete(filePath);
            StreamWriter sw = new StreamWriter(filePath);
            
            
                foreach (string s in text)
                {
                    sw.WriteLine(s);
                }
            
            
            sw.Close();
            
        }
      


        internal List<string> Read(string fileName)
        {

            List<string> list = new List<string>();
            string filePath = Path.Combine(Environment.CurrentDirectory,
                fileName);
            StreamReader sr = new StreamReader(filePath);
            string line = String.Empty;
            while ((line = sr.ReadLine()) != null)
            {

                list.Add(line);

            }
            sr.Close();
            return list;
        }
       
    }

    
}
