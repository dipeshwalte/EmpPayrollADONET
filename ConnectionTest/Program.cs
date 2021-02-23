using System;
using System.Data;
using System.Data.SqlClient;

namespace ConnectionTest
{
    
    class Program
    {
        public static string connectionString = @"Data Source=DESKTOP-GBP5KKD\SQLEXPRESS;Initial Catalog=EmpPayroll;Integrated Security=True";
        public static void ReadAllRecords()
        {
            Employee emp = new Employee();
            Console.WriteLine("Welcome to employee payroll!");
            SqlConnection connection = new SqlConnection(connectionString);
            /*
            string query = @"select EmployeePayroll.ID,Name,StartDate,Gender,PhoneNumber,
                                    Address,Department,BasicPay,Deduction,TaxablePay,IncomeTax
                                    NetPay, CompanyName
                            from EmployeePayroll, Department, Payroll, Company
                            where EmployeePayroll.ID = Department.ID and
                                  EmployeePayroll.ID = Payroll.ID and
                                  EmployeePayroll.ID = Company.ID; ";
            */
            string query = @"SELECT ID,Name,StartDate,Gender,PhoneNumber,
                                    Address
                               FROM EmployeePayroll;";
            using (connection)
            {
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader dr = sqlCommand.ExecuteReader();
                if (dr.HasRows)
                {
                    Console.WriteLine();
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine("Employee details are: ");
                    Console.WriteLine("Id,Name,Phone Number");
                    while (dr.Read())
                    {
                        emp.id = dr.GetInt32(0);
                        emp.name = dr.GetString(1);
                        emp.startDate = dr.GetDateTime(2).Date;
                        emp.gender = Convert.ToChar(dr.GetString(3));
                        emp.phoneNumber = dr.IsDBNull(4) ? 0 : dr.GetInt64(4);
                        emp.address = dr.IsDBNull(5) ? "" : dr.GetString(5);
                        //emp.department = dr.GetString(6);
                        //emp.basicPay = dr.IsDBNull(7) ? 0 : dr.GetInt32(7);
                        //emp.deduction = dr.IsDBNull(8) ? 0 : dr.GetInt32(8);
                        //emp.taxablePay = dr.IsDBNull(9) ? 0 : dr.GetInt32(9);
                        //emp.incomeTax = dr.IsDBNull(10) ? 0 : dr.GetInt32(10);
                        //emp.companyName = dr.IsDBNull(11) ? "" : dr.GetString(11);
                        Console.WriteLine(emp.id+","+emp.name+","+emp.phoneNumber);
                      //  Console.WriteLine("Start Date: " + emp.startDate);
                     //   Console.WriteLine("Gender: " + emp.gender);
                     //   Console.WriteLine("Company Name: " + emp.companyName);
                    }//end while

                }//end if
                connection.Close();
            }//end using
        }
        public static void InsertRecord()
        {
            Employee emp = new Employee();
            Console.WriteLine("Enter Employee Name");
            emp.name = Console.ReadLine();
            emp.startDate = DateTime.Now.Date;
            Console.WriteLine("Enter Gender");
            emp.gender = Console.ReadLine()[0];
            emp.phoneNumber = 0;

            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"Insert into EmployeePayroll(Name,StartDate,Gender,PhoneNumber) values('"
                                + emp.name + "',@DateValue"  + ",'" + emp.gender + "'," + emp.phoneNumber + ");";
            try
            {
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.Parameters.AddWithValue("@DateValue", emp.startDate);
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public static void InsertRecordStoredProcedure()
        {
            Employee emp = new Employee();
            Console.WriteLine("Enter Employee Name");
            emp.name = Console.ReadLine();
            emp.startDate = DateTime.Now.Date;
            Console.WriteLine("Enter Gender");
            emp.gender = Console.ReadLine()[0];
            emp.phoneNumber = 0;

            SqlConnection connection = new SqlConnection(connectionString);
            string query = "sp_AddEmployeeEntry";
            try
            {
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@StartDate", emp.startDate);
                    sqlCommand.Parameters.AddWithValue("@Name", emp.name);
                    sqlCommand.Parameters.AddWithValue("@Gender", emp.gender);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", emp.phoneNumber);
                    
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public static void UpdateRecord()
        {
            Employee emp = new Employee();
            Console.WriteLine("Enter Employee Name");
            emp.name = Console.ReadLine();
            emp.startDate = DateTime.Now;
            Console.WriteLine("Enter Phone");
            emp.phoneNumber = Convert.ToInt32(Console.ReadLine());

            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"Update EmployeePayroll set PhoneNumber ="+ emp.phoneNumber + " where Name = '"+emp.name +"';";
            try
            {
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
    }

        public static void DeleteRecord()
        {
            
            Console.WriteLine("Enter Id To delete");
            
            int id = Convert.ToInt32(Console.ReadLine());

            SqlConnection connection = new SqlConnection(connectionString);
            string query1 = @"Delete from Company where id =" +id+ ";";
            string query2 = @"Delete from Payroll where id =" + id + ";";
            string query3 = @"Delete from Department where id =" + id + ";";
            string query4 = @"Delete from EmployeePayroll where id =" + id + ";";
            try
            {
                using (connection)
                {
                    SqlCommand sqlCommand1 = new SqlCommand(query1, connection);
                    SqlCommand sqlCommand2 = new SqlCommand(query2, connection);
                    SqlCommand sqlCommand3 = new SqlCommand(query3, connection);
                    SqlCommand sqlCommand4 = new SqlCommand(query4, connection);
                    connection.Open();
                    sqlCommand1.ExecuteNonQuery();
                    sqlCommand2.ExecuteNonQuery();
                    sqlCommand3.ExecuteNonQuery();
                    sqlCommand4.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        static void Main(string[] args)
        {
            int option = 1;
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
                        InsertRecordStoredProcedure();
                        break;
                    case 2:
                        ReadAllRecords();
                        break;
                    case 3:
                        ReadAllRecords();
                        UpdateRecord();
                        break;
                    case 4:
                        ReadAllRecords();
                        DeleteRecord();
                        break;
                    default:
                        break;
                }
            } while (option!=5);
            
        }
    }
}
