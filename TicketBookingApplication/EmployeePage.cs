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
    public partial class EmployeePage : Form
    {
        OleDbConnection oleDbConnection;
        OleDbCommand command2;
        List<Crew> crews = new List<Crew>();
        public EmployeePage()
        {
            oleDbConnection = new OleDbConnection();
            oleDbConnection.ConnectionString = ConfigurationManager.AppSettings["Ticket"];
            InitializeComponent();
        }

        private void EmployeePage_Load(object sender, EventArgs e)
        {
            this.label1.Hide();
            this.label2.Hide();
            this.textBox1.Hide();
            this.textBox2.Hide();
            this.comboBox1.Hide();
            this.button5.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.label1.Show();
            this.label2.Show();
            this.textBox1.Show();
            this.textBox2.Show();
            this.button5.Show();
            
            Utility.Utility.ClickedButton = "AddCrew";
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.label1.Show();
            this.label2.Show();
            this.textBox1.Show();
            this.comboBox1.Show();
            this.button5.Show();
            var command = "Select * from Crew";
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            var command2 = new OleDbCommand(command, oleDbConnection);
            adapter.SelectCommand = command2;
            var ds = new DataSet();
            adapter.Fill(ds);
            var dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {

                var tempUser = new Crew
                {
                    Id = Convert.ToInt32(dr["Crew_Id"]),
                    Name = dr["Crew_Name"].ToString(),
                    Director = dr["Director"].ToString()
                };
                if (tempUser != null)
                {
                    crews.Add(tempUser);
                }
            }
            List<string> list = new List<string>();
            crews.ForEach(x =>
            {
                list.Add(x.Name);
            });
            this.comboBox1.DataSource = list;
            Utility.Utility.ClickedButton = "AddPlay";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Utility.Utility.ClickedButton == "AddPlay")
            {
                oleDbConnection.Open();
                var crewId = crews.Find(x => x.Name == this.comboBox1.Text);
                var command3 = String.Format("Insert INTO [Play] ([Play_Name], [Crew_Id]) VALUES ('{0}', '{1}')", textBox1.Text, crewId.Id);
                OleDbCommand command4 = new OleDbCommand(command3, oleDbConnection);
                command4.ExecuteNonQuery();
                MessageBox.Show("Play Added !!!");
            }
            if (Utility.Utility.ClickedButton == "AddCrew")
            {
                oleDbConnection.Open();
                var command = String.Format("Insert INTO [Crew] ([Crew_Name], [Director]) VALUES ('{0}', '{1}')", textBox1.Text, textBox2.Text);
                OleDbCommand command2 = new OleDbCommand(command, oleDbConnection);
                command2.ExecuteNonQuery();
                MessageBox.Show("Crew Added !!!");
                this.label1.Hide();
                this.label2.Hide();
                this.textBox1.Hide();
                this.textBox2.Hide();
                this.button5.Hide();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<Payment> payments = new List<Payment>();
            oleDbConnection.Open();
            var command = "Select * from Payment";
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            var command2 = new OleDbCommand(command, oleDbConnection);
            adapter.SelectCommand = command2;
            var ds = new DataSet();
            adapter.Fill(ds);
            var dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {

                var tempUser = new Payment
                {
                    Id = Convert.ToInt32(dr["Payment_Id"]),
                    CustomerId = Convert.ToInt32(dr["Customer_Id"]),
                    Amount = Convert.ToInt32(dr["Amount"]),
                    PaymentType = dr["Payment_Type"].ToString(),
                    PaymentDate = dr["Payment_Date"].ToString()
                };
                if (tempUser != null)
                {
                    payments.Add(tempUser);
                }
            }
            this.dataGridView1.DataSource = payments;
            oleDbConnection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<Reservation> payments = new List<Reservation>();
            oleDbConnection.Open();
            var command = "Select * from Reservation";
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            var command2 = new OleDbCommand(command, oleDbConnection);
            adapter.SelectCommand = command2;
            var ds = new DataSet();
            adapter.Fill(ds);
            var dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {

                var tempUser = new Reservation
                {
                    Id = Convert.ToInt32(dr["Reservation_Id"]),
                    CustomerId = Convert.ToInt32(dr["Customer_Id"]),
                    PlayId = Convert.ToInt32(dr["Play_Id"]),
                    ReservationType = dr["Reservation_Type"].ToString(),
                    ReservationDate = dr["Reservation_Date"].ToString()
                };
                if (tempUser != null)
                {
                    payments.Add(tempUser);
                }
            }
            this.dataGridView1.DataSource = payments;
            oleDbConnection.Close();
        }
    }
}
