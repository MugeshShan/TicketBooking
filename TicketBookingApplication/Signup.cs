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
    public partial class Signup : Form
    {
        OleDbConnection oleDbConnection;
        OleDbCommand command2;
        public Signup()
        {
            oleDbConnection = new OleDbConnection();
            oleDbConnection.ConnectionString = ConfigurationManager.AppSettings["Ticket"];
            var command = "Select * from Location";
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            command2 = new OleDbCommand(command, oleDbConnection);
            adapter.SelectCommand = command2;
            var ds = new DataSet();
            adapter.Fill(ds);
            var dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {

                var tempUser = new Location
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    StateName = dr["StateName"].ToString(),
                };
                if (tempUser != null)
                {
                    Utility.Utility.Locations.Add(tempUser);
                }

            }
            oleDbConnection.Close();
            try
            {
                InitializeComponent();
            }
            catch(Exception ex)
            {

            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Signup_Load(object sender, EventArgs e)
        {
            List<string> LocationName = new List<string>();
            Utility.Utility.Locations.ForEach(x =>
            {
                LocationName.Add(x.StateName);
            });
            this.comboBox1.DataSource = LocationName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            oleDbConnection.Open();
            if (this.checkBox1.Checked)
            {
                var state = Utility.Utility.Locations.Find(x => x.StateName == this.comboBox1.Text);
                string gender;
                if (this.radioButton1.Checked == true)
                {
                    gender = "Male";
                }
                else
                {
                    gender = "Female";
                }
                var command = String.Format("Insert INTO [Customer] ([FirstName], [LastName], [Email], [Gender], [Username], [Password], State_Id) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6})", textBox5.Text, textBox1.Text, textBox2.Text, gender, textBox4.Text, maskedTextBox1.Text, state.Id);
                OleDbCommand command2 = new OleDbCommand(command, oleDbConnection);
                command2.ExecuteNonQuery();
                ClearTextBoxes();
                MessageBox.Show("User Added !!!");
                Form1 form1 = new Form1();
                form1.Show();
            }
            else
            {
                var state = Utility.Utility.Locations.Find(x => x.StateName == this.comboBox1.Text);
                string gender;
                if (this.radioButton1.Checked == true)
                {
                    gender = "Male";
                }
                else
                {
                    gender = "Female";
                }
                var command = String.Format("Insert INTO [Employee] ([FirstName], [LastName], [Email], [Gender], [Username], [Password], State_Id) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6})", textBox5.Text, textBox1.Text, textBox2.Text, gender, textBox4.Text, maskedTextBox1.Text, state.Id);
                OleDbCommand command2 = new OleDbCommand(command, oleDbConnection);
                command2.ExecuteNonQuery();
                ClearTextBoxes();
                MessageBox.Show("Employee Added !!!");
                Form1 form1 = new Form1();
                form1.Show();
            }
           
        }


        private void ClearTextBoxes()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else
                        func(control.Controls);
            };

            func(Controls);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.checkBox1.Checked = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.checkBox2.Checked = false;
        }
    }
}
