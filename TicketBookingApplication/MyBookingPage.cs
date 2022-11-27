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
    public partial class MyBookingPage : Form
    {
        public MyBookingPage()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void MyBookingPage_Load(object sender, EventArgs e)
        {
            OleDbConnection oleDbConnection;
            oleDbConnection = new OleDbConnection();
            oleDbConnection.ConnectionString = ConfigurationManager.AppSettings["Ticket"];
            oleDbConnection.Open();
            var command = String.Format("Select * from Reservation");
            OleDbCommand command2 = new OleDbCommand(command, oleDbConnection);
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = command2;
            var ds = new DataSet();
            adapter.Fill(ds);
            var dt = ds.Tables[0];
            List<Reservation> tickets = new List<Reservation>();
            foreach (DataRow dr in dt.Rows)
            {
                var ticket = new Reservation
                {
                    Id = Convert.ToInt32(dr["Reservation_Id"]),
                    CustomerId = Convert.ToInt32(dr["Customer_Id"]),
                    PlayId = Convert.ToInt32(dr["Play_Id"]),
                    ReservationType = dr["Reservation_Type"].ToString(),
                    ReservationDate = dr["Reservation_Date"].ToString(),
                };

                if (ticket.CustomerId == Utility.Utility.Customer.Id)
                {
                    tickets.Add(ticket);
                }
            }
            this.dataGridView1.DataSource = tickets;
            oleDbConnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CustomerPage customerPage = new CustomerPage();
            customerPage.Show();
        }
    }
}
