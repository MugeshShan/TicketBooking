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
    public partial class BookingPage : Form
    {
        OleDbConnection oleDbConnection;
        OleDbCommand command2;
        List<Crew> Crews = new List<Crew>();
        List<Play> Plays = new List<Play>();
        public BookingPage()
        {
            oleDbConnection = new OleDbConnection();
            oleDbConnection.ConnectionString = ConfigurationManager.AppSettings["Ticket"];
            var command = "Select * from Crew";
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            command2 = new OleDbCommand(command, oleDbConnection);
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
                    Crews.Add(tempUser);
                }

            }
            command = "Select * from Play";
            adapter = new OleDbDataAdapter();
            command2 = new OleDbCommand(command, oleDbConnection);
            adapter.SelectCommand = command2;
            ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {

                var tempUser = new Play
                {
                    Id = Convert.ToInt32(dr["Play_Id"]),
                    Name = dr["Play_Name"].ToString(),
                    Crew_Id = Convert.ToInt32(dr["Crew_Id"])
                };
                if (tempUser != null)
                {
                    Plays.Add(tempUser);
                }

            }
            oleDbConnection.Close();
            InitializeComponent();
            List<string> list = new List<string>();
            Plays.ForEach(x =>
            {
                list.Add(x.Name);
            });
            this.comboBox1.DataSource = list;
           
        }

        private void BookingPage_Load(object sender, EventArgs e)
        {
            this.label5.Hide();
            this.label6.Hide();
            this.label7.Hide();
            this.label8.Hide();
            this.button2.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var play = Plays.Find(x => x.Name == this.comboBox1.Text);
            var crew = Crews.Find(x => x.Id == play.Crew_Id);
            Utility.Utility.Play = play;
            Utility.Utility.Crew = crew;
            this.label5.Show();
            this.label6.Show();
            this.label7.Show();
            this.label8.Show();
            this.button2.Show();
            this.label4.Hide();
            this.label6.Text = Utility.Utility.Crew.Name;
            this.label8.Text = Utility.Utility.Crew.Director;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TicketBookingPage ticketBookingPage = new TicketBookingPage();
            ticketBookingPage.Show();
            this.Close();
        }
    }
}
