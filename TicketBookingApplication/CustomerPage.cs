using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicketBookingApplication
{
    public partial class CustomerPage : Form
    {
        public CustomerPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BookingPage bookingPage = new BookingPage();
            bookingPage.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyBookingPage bookingPage = new MyBookingPage();
            bookingPage.Show();
            this.Close();
        }
    }
}
