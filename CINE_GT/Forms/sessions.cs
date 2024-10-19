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

namespace CINE_GT.Forms
{
    public partial class sessions : Form
    {
        public sessions(string username)
        {
            InitializeComponent();
            dateTimeInitialConfig(dateTimePicker1);
            dateTimeInitialConfig(dateTimePicker2);
            dateTimeInitialConfig(dateTimePicker3);
        }
        private void llenarDataGridView(CINE_GT_DB cineGT, DataGridView dataGridView, DateTime beginDate, DateTime endingDate)
        {
            dataGridView.DataSource = "";
            try
            {
                dataGridView.DataSource = cineGT.llenarSessionDataGridView(beginDate, endingDate);
                dataGridView.Columns[0].Width = 30;
                dataGridView.Columns[1].Width = 30;
                dataGridView.Columns[2].Width = 160;
                dataGridView.Columns[3].Width = 160;

                dataGridView.Columns[0].HeaderText = "ID";
                dataGridView.Columns[1].HeaderText = "State";
                dataGridView.Columns[2].HeaderText = "Intial Date";
                dataGridView.Columns[3].HeaderText = "Terminal Date";
                dataGridView.Columns[4].HeaderText = "Movie";
                dataGridView.Columns[5].HeaderText = "Room";
                dataGridView.Columns[6].HeaderText = "Created By";
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        private void sessions_Load(object sender, EventArgs e)
        {
            dateTimePicker1.ResetText();
            dateTimePicker2.ResetText();
            dateTimePicker3.ResetText();
            CINE_GT_DB cineGt = new CINE_GT_DB();
            try
            {
                cineGt.llenarComboBox(comboBox1, "_NAME_", "MOVIE");
                cineGt.llenarComboBox(comboBox2, "ID", "ROOM");

                DateTime fechaMinima = new DateTime(1753, 1, 1, 12, 0, 0); // 1/1/1753 12:00:00
                DateTime fechaMaxima = new DateTime(9999, 12, 31, 23, 59, 59); // 12/31/9999 23:59:59

                llenarDataGridView(cineGt, dataGridView1, fechaMinima, fechaMaxima);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dateTimeInitialConfig(DateTimePicker dataTime)
        {
            dataTime.Format = DateTimePickerFormat.Custom;
            dataTime.CustomFormat = "dd/MM/yyyy hh:mm tt";
            dataTime.ShowUpDown = true;
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dateTimePicker1.ResetText();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                int id = int.Parse(selectedRow.Cells[0].Value.ToString());
                int status = int.Parse(selectedRow.Cells[1].Value.ToString());
                if (status == 1)
                {
                    MessageBox.Show("Can not deactivate a session in progress.");
                    return;
                }

                //aqui deberia de ir a la base de datos y ejecutar un procedimiento para poder desactivar begin date a null (logica como habiamos pensado)
            }
            else
            {
                MessageBox.Show("Select a transaction to deactivate.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CINE_GT_DB cineGt = new CINE_GT_DB();
            try
            {
                llenarDataGridView(cineGt, dataGridView1, dateTimePicker2.Value, dateTimePicker3.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
