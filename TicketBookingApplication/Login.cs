using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicketBookingApplication.Models;

namespace TicketBookingApplication
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Customer> customers = new List<Customer>();
            List<Employee> employees = new List<Employee>();
            OleDbConnection oleDbConnection = new OleDbConnection();
            oleDbConnection.ConnectionString = ConfigurationManager.AppSettings["Ticket"];
            if (this.radioButton1.Checked == true)
            {
                var command = "Select * from Customer";
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                OleDbCommand command2 = new OleDbCommand(command, oleDbConnection);
                adapter.SelectCommand = command2;
                var ds = new DataSet();
                adapter.Fill(ds);
                var dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {

                    var tempUser = new Customer
                    {
                        Id = Convert.ToInt32(dr["Customer_Id"]),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        Email = dr["Email"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        Username = dr["Username"].ToString(),
                        Password = dr["Password"].ToString(),
                        StateId = Convert.ToInt32(dr["State_Id"])
                    };
                    if (tempUser != null)
                    {
                        customers.Add(tempUser);
                    }

                }

                foreach (var customer in customers)
                {
                    if (textBox1.Text == customer.Username && maskedTextBox1.Text == customer.Password)
                    {
                        Utility.Utility.Customer = customer;
                        MessageBox.Show("Welcome " + customer.FirstName + " !!!");
                        CustomerPage bookingPage = new CustomerPage();
                        bookingPage.Show();
                        this.Close();
                    }
                }
            }
            else
            {
                var command = "Select * from Employee";
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                OleDbCommand command2 = new OleDbCommand(command, oleDbConnection);
                adapter.SelectCommand = command2;
                var ds = new DataSet();
                adapter.Fill(ds);
                var dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {

                    var tempUser = new Employee
                    {
                        Id = Convert.ToInt32(dr["Employee_Id"]),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        Email = dr["Email"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        Username = dr["Username"].ToString(),
                        Password = dr["Password"].ToString(),
                        StateId = Convert.ToInt32(dr["State_Id"])
                    };
                    if (tempUser != null)
                    {
                        employees.Add(tempUser);
                    }

                }

                foreach (var employee in employees)
                {
                    if (textBox1.Text == employee.Username && maskedTextBox1.Text == employee.Password)
                    {
                        MessageBox.Show("Welcome " + textBox1.Text + " !!!");
                        EmployeePage bookingPage = new EmployeePage();
                        bookingPage.Show();
                        this.Close();
                    }
                }
            }
        }
    }
}
