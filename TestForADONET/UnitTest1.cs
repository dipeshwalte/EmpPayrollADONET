using NUnit.Framework;
using ConnectionTest;
using System;
using System.Collections.Generic;
using System.Data;

namespace TestForADONET
{
    public class Tests
    {
        EmpPayroll empPayroll;
        [SetUp]
        public void Setup()
        {
           empPayroll = new EmpPayroll();
        }

        [Test]
        public void GivenUpdatedSalaryByName_UpdateSalary()
        {
            empPayroll.UpdateEmployeeSalary("Terissa", 300000);
            List<Employee> employees = empPayroll.ReturnAllRecords();
            Employee terissaDetails = new Employee();
            foreach (Employee employee in employees)
            {
                if (employee.name == "Terissa")
                {
                    terissaDetails = employee;
                    break;
                }
            }
            Assert.AreEqual(300000,terissaDetails.basicPay);
        }

        [Test]
        public void GivenDateRange_GetRecordsInDateRange()
        {
           
            List<Employee> employees = empPayroll.RetreiveFromDateRange(Convert.ToDateTime("2019-01-01"),Convert.ToDateTime("2020-01-01"));
            
            Assert.AreEqual(employees.Count, 3);

        }

        [Test]
        public void GivenSumQuery_GetRecordsForMaleFemale()
        {

            DataSet dataset = empPayroll.RetrieveGenderComparisonInfo();
            Assert.AreEqual("F", dataset.Tables[0].Rows[0].ItemArray.GetValue(0));
            Assert.AreEqual(300000, dataset.Tables[0].Rows[0].ItemArray.GetValue(1));
            Assert.AreEqual("M", dataset.Tables[0].Rows[1].ItemArray.GetValue(0));
            Assert.AreEqual(55000,dataset.Tables[0].Rows[1].ItemArray.GetValue(1));
           

        }
        //dataGridView1.DataSource = dset.Tables["student_detail"]; // Binding the datagridview
    }
}