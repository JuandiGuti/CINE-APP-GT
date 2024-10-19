using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CINE_GT.Forms
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {
            CenterControlHorizontally(label1);
            CenterControlHorizontally(label3);
            CenterControlHorizontally(label4);
            CenterControlHorizontally(textBox1);
            CenterControlHorizontally(textBox2);
            CenterControlHorizontally(button1);
            CenterControlHorizontally(linkLabel1);
            /*
            CINE_GT_DB connCINE_GT = new CINE_GT_DB();
            if (connCINE_GT.Ok())
                MessageBox.Show("Conectao");
            else
                MessageBox.Show("Nelson");*/

        }
        private void CenterControlHorizontally(Control control)
        {
            control.Left = (this.ClientSize.Width - control.Width) / 2;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            register register_form = new register();
            register_form.Show();

            this.Hide();

            register_form.FormClosed += (s, args) => Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private string hashPassword(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, 5);
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            if (!isPasswordValid)
            {
                throw new Exception("Can not hash the password correctly.");
            }
            return hashedPassword;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Login button
            string username = textBox1.Text.ToLower();
            string password = textBox2.Text;

            //metodo para verificar la contrasena y usuario

            CINE_GT_DB cineGtDb = new CINE_GT_DB();

            try
            {
                if(cineGtDb.userLogIn(username, password))
                {
                    MessageBox.Show("Login successful!, Welcome to Cine Gt App.");
                    if (username.Equals("admin"))
                    {
                        admin admin_form = new admin(username);
                        admin_form.Show();

                        this.Hide();

                        admin_form.FormClosed += (s, args) => Application.Exit();
                    }
                    else
                    {
                        //mandar a form de clientes
                    }
                    
                }
                else
                {
                    throw new Exception($"The user: {username} does not exist or the password is incorrect.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
