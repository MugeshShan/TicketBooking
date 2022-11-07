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
    public partial class PaymentPage : Form
    {
        public PaymentPage()
        {
            InitializeComponent();
        }

        private void PaymentPage_Load(object sender, EventArgs e)
        {
            this.label2.Text = Utility.Utility.Play.Name;
            this.label4.Text = Utility.Utility.Crew.Director;
            this.label9.Text = Utility.Utility.Section;
            this.label10.Text = Utility.Utility.No_of_Seats.ToString();
            this.label11.Text = (Utility.Utility.Amount * Utility.Utility.No_of_Seats).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var oleDbConnection = new OleDbConnection();
            oleDbConnection.ConnectionString = ConfigurationManager.AppSettings["Ticket"];
            oleDbConnection.Open();
            var command = String.Format("Insert INTO [Transaction] ([Transaction_Status], [Customer_Id], [Transaction_Date]) VALUES ('{0}', {1}, '{2}')", "Success", Utility.Utility.Customer.Id, DateTime.Now.ToString("MM/dd/yyyy"));
            var command2 = new OleDbCommand(command, oleDbConnection);
            command2.ExecuteNonQuery();
            command = String.Format("Insert INTO [Reservation] ([Reservation_Date], [Play_Id], [Reservation_Type], [Customer_Id]) VALUES ('{0}', {1}, '{2}', {3})", DateTime.Now.ToString("MM/dd/yyyy"), Utility.Utility.Play.Id, "Online", Utility.Utility.Customer.Id);
            command2 = new OleDbCommand(command, oleDbConnection);
            command2.ExecuteNonQuery();
            MessageBox.Show("Tickets Booked!!!");
            BookingPage bookingPage = new BookingPage();
            bookingPage.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TicketBookingPage ticketBookingPage = new TicketBookingPage();
            ticketBookingPage.Show();
            this.Close();
        }
    }
}
