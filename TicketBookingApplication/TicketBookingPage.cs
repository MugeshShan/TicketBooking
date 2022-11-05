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

namespace TicketBookingApplication
{
    public partial class TicketBookingPage : Form
    {
        OleDbConnection oleDbConnection;
        OleDbCommand command2;
        public TicketBookingPage()
        {
            InitializeComponent();
        }

        private void TicketBookingPage_Load(object sender, EventArgs e)
        {
            this.label2.Text = Utility.Utility.Play.Name;
            this.label4.Text = Utility.Utility.Crew.Director;
            List<string> Row = new List<string>();
            Row.Add("Bronze");
            Row.Add("Silver");
            Row.Add("Gold");
            this.comboBox1.DataSource = Row;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BookingPage bookingPage = new BookingPage();
            bookingPage.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            oleDbConnection = new OleDbConnection();
            oleDbConnection.ConnectionString = ConfigurationManager.AppSettings["Ticket"];
            oleDbConnection.Open();
            int seatId = 0;
            int price = 0;
            if(this.comboBox1.Text == "Bronze")
            {
                seatId = 1;
                price = 500;
            }
            else if(this.comboBox1.Text == "Silver")
            {
                seatId = 2;
                price = 750;
            }
            else
            {
                seatId = 3;
                price = 1000;
            }
            var command = String.Format("Insert INTO [Ticket] ([Ticket_No], [Price], [Seat_Id]) VALUES ({0}, {1}, {2})", textBox1.Text, price * Convert.ToInt32(this.textBox1.Text), seatId);
            OleDbCommand command2 = new OleDbCommand(command, oleDbConnection);
            command2.ExecuteNonQuery();
            command = String.Format("Insert INTO [Payment] ([Amount], [Payment_Type], [Customer_Id], [Payment_Date]) VALUES ({0}, '{1}', {2}, '{3}')", price * Convert.ToInt32(this.textBox1.Text), "Card", Utility.Utility.Customer.Id, DateTime.Now.ToString("MM/dd/yyyy"));
            command2 = new OleDbCommand(command, oleDbConnection);
            command2.ExecuteNonQuery();
            command = String.Format("Insert INTO [Transaction] ([Transaction_Status], [Customer_Id], [Transaction_Date]) VALUES ('{0}', {1}, '{2}')", "Success", Utility.Utility.Customer.Id, DateTime.Now.ToString("MM/dd/yyyy"));
            command2 = new OleDbCommand(command, oleDbConnection);
            command2.ExecuteNonQuery();
            command = String.Format("Insert INTO [Reservation] ([Reservation_Date], [Play_Id], [Reservation_Type], [Customer_Id]) VALUES ('{0}', {1}, '{2}', {3})", DateTime.Now.ToString("MM/dd/yyyy"), Utility.Utility.Play.Id, "Online", Utility.Utility.Customer.Id);
            command2 = new OleDbCommand(command, oleDbConnection);
            command2.ExecuteNonQuery();
            BookingPage bookingPage = new BookingPage();
            bookingPage.Show();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.Text == "Bronze")
            {
                this.label7.Text = "500";
            }
            else if (this.comboBox1.Text == "Silver")
            {
                this.label7.Text = "750";
            }
            else
            {
                this.label7.Text = "1000";
            }
        }
    }
}
