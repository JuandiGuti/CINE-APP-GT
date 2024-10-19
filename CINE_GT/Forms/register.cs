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
using System.Text.RegularExpressions;
using BCrypt.Net;

namespace CINE_GT.Forms
{
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
        }

        private void register_Load(object sender, EventArgs e)
        {
            CenterControlHorizontally(label1);
            CenterControlHorizontally(label2);
            CenterControlHorizontally(label3);
            CenterControlHorizontally(label4);
            CenterControlHorizontally(textBox1);
            CenterControlHorizontally(textBox2);
            CenterControlHorizontally(button1);
            CenterControlHorizontally(linkLabel1);
        }
        private void CenterControlHorizontally(Control control)
        {
            control.Left = (this.ClientSize.Width - control.Width) / 2;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            login login_form = new login();
            login_form.Show();

            this.Hide();

            login_form.FormClosed += (s, args) => Application.Exit();
        }
        private bool passwordSecure(string password)
        {
            // Al menos 8 caracteres, una letra mayúscula, una minúscula, un número y un carácter especial
            var passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$";
            return Regex.IsMatch(password, passwordPattern);
        }
        private string hashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, 5);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Register button
            string username = textBox1.Text.ToLower();
            string password = textBox2.Text;

            try
            {
                //comprobar la seguridad de la contrasena
                if (!passwordSecure(password))
                {
                    throw new Exception("The password is not secure. Min; 8 Characters; 1 Capital letter; 1 Small letter; 1 Number; 1 Especial Character");
                }
                //metodo para hashear la contrasena
                password = hashPassword(password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            CINE_GT_DB cineGtDb = new CINE_GT_DB();

            try
            {
                cineGtDb.userRegister(username, password);
                MessageBox.Show("Welcome to Cine Gt App, you can now sign in!");

                login login_form = new login();
                login_form.Show();

                this.Hide();

                login_form.FormClosed += (s, args) => Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
