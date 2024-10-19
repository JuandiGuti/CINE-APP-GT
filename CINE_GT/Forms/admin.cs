using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CINE_GT.Forms
{
    public partial class admin : Form
    {
        public string username { get; set; }
        public admin(string un)
        {
            InitializeComponent();
            username = un;
            label1.Text = ($"WELCOME, {un}").ToUpper();
        }

        private void admin_Load(object sender, EventArgs e)
        {
            
        }
        private void CenterControlHorizontally(Control control)
        {
            control.Left = (this.ClientSize.Width - control.Width) / 2;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sessions sessions_form = new sessions(username);
            sessions_form.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            movies movies_form = new movies();
            movies_form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sign Out, See you soon!");
            login login_form = new login();
            login_form.Show();

            this.Hide();

            login_form.FormClosed += (s, args) => Application.Exit();
        }
    }
}
