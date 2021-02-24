using System;

namespace ConnectionTest
{
    
    public class Program
    {
        
        static void Main(string[] args)
        {
            EmpPayroll empPayroll = new EmpPayroll();
            int option;
            do
            {
                Console.WriteLine("1) Insert Record");
                Console.WriteLine("2) Read All records");
                Console.WriteLine("3) Update Record");
                Console.WriteLine("4) Delete Record");
                Console.WriteLine("5) Exit");
                Console.WriteLine("Enter Choice");
                option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        empPayroll.InsertRecordStoredProcedure();
                        break;
                    case 2:
                        empPayroll.ReturnAllRecords();
                        break;
                    case 3:
                        empPayroll.ReturnAllRecords();
                        empPayroll.UpdateRecord();
                        break;
                    case 4:
                        empPayroll.ReturnAllRecords();
                        empPayroll.DeleteRecordStoredProcedure();
                        break;
                    default:
                        break;
                }
            } while (option!=5);
        }
    }
}
