using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Collections;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.Common;

namespace CINE_GT
{
    internal class CINE_GT_DB
    {
        private string connectionString = "Data Source=LAPTOP-E3AMJ72E\\SQLEXPRESS;Initial Catalog=CINE_GT;User=CINE_GT_APP_USER;Password=CINE_GT_APP_USER;";
        public bool Ok()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void userRegister(string username, string password /*LA PASS TIENE QUE VENIR YA HASHEADA AQUI*/)
        {
            string query = "USER_REGISTER";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@username", SqlDbType.VarChar, 100)).Value = username;
                cmd.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 300)).Value = password;

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex) 
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public bool userLogIn(string username, string password /*LA PASS TIENE QUE VENIR YA HASHEADA AQUI*/)
        {
            //PRIMERA PARTE SOLO PARA USUARIO
            string query = "USER_LOGIN";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@username", SqlDbType.VarChar, 100)).Value = username;

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            //SEGUNDA PARTE SOLO PARA CONTRASENA
            query = $"select _USERNAME_, _PASSWORD_ from APP_USER where _USERNAME_ = '{username}';";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        appUser userApp = new appUser();
                        userApp.username = reader.GetString(0);
                        userApp.password = reader.GetString(1);
                        conn.Close();
                        return BCrypt.Net.BCrypt.Verify(/*Verifica la cotrasena ingresada vs la hasheada en la base de datos*/password, userApp.password);
                    }
                    
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return false;
        }
        public void llenarComboBox(System.Windows.Forms.ComboBox combobox, string column, string table)
        {
            string query = $"select {column} from {table};";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        using (SqlCommand command = new SqlCommand(query, conn, transaction))
                        {
                            SqlDataReader reader = command.ExecuteReader();

                            combobox.Items.Clear();

                            while (reader.Read())
                            {
                                combobox.Items.Add(reader[column].ToString());
                            }

                            reader.Close();
                        }

                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public List<session> llenarSessionDataGridView(DateTime intialDate, DateTime terminalDate)
        {
            List<session> list = new List<session>();
            string query = "SESSION_BY_DATE";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@INITIAL_DATE", SqlDbType.DateTime)).Value = intialDate;
                        cmd.Parameters.Add(new SqlParameter("@TERMINAL_DATE", SqlDbType.DateTime)).Value = terminalDate;

                        cmd.ExecuteNonQuery();

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            session session = new session();
                            session.id = (reader.GetInt32(0));
                            session.state = (reader.GetInt32(1));
                            session.intialDate = (reader.GetDateTime(2));
                            session.terminalDate = (reader.GetDateTime(3));
                            session.movie = (reader.GetString(4));
                            session.room = (reader.GetInt32(5));
                            session.appUser = (reader.GetString(6));
                            list.Add(session);
                        }
                        reader.Close();
                        conn.Close();
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }


    }
    public class appUser
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    public class classification
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class movie
    {
        public int id { get; set; }
        public string name { get; set; }
        public String duration { get; set; }
        public int clasification { get; set; }
    }
    public class session
    {
        public int id { get; set; }
        public int state { get; set; }
        public DateTime intialDate { get; set; }
        public DateTime? terminalDate { get; set; }
        public string movie { get; set; }
        public int room { get; set; }
        public string appUser { get; set; }
    }
    public class transaction
    {
        public int id { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime? dateModified { get; set; }
        public string appUser{ get; set; }
        public int state { get; set; }
    }
    public class room
    {
        public int id { get; set; }
    }
    public class seat
    {
        public string id { get; set; }
    }
}
