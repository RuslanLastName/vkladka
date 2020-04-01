using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp2
{
    class SqlConn
    {
        private SqlConnection connect = null;
        public string qwetext { get; set; } = @"Data Source=Ruslan\SQLexp;Initial catalog=Pascal; Integrated Security=true";
        public void OpenConnection(string ConnectionString)
        {
            connect = new SqlConnection(ConnectionString);
            connect.Open();
        }
        public void CloseConnection()
        {
            connect.Close();
        }

        public void InsertSt(string name, string l_name, string receipt_date, string speciality, string faculty)
        {
            string sqlcmd = string.Format("Insert into Pascal" + "(Name, Last_name, Receipt_date, Speciality, Faculty) Values(@Name, @Last_name, @Receipt_date, @Speciality, @Faculty)");

            using (SqlCommand cmd = new SqlCommand(sqlcmd, connect))
            {
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Last_name", l_name);
                cmd.Parameters.AddWithValue("@Receipt_date", receipt_date);
                cmd.Parameters.AddWithValue("@Speciality", speciality);
                cmd.Parameters.AddWithValue("@Faculty", faculty);
                cmd.ExecuteNonQuery();
            }
        }

        public void Sqltext(string text)
        {

            using (SqlCommand cmd = new SqlCommand(text, connect))
            {

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {

                    Exception error = new Exception("Error", ex);
                    throw error;
                }
            }
        }

        public DataTable GetAllAsDataTable()
        {
            DataTable inv = new DataTable();
            string sqlcmd = "Select * from Products";
            using (SqlCommand cmd = new SqlCommand(sqlcmd, connect))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                inv.Load(dr);
                dr.Close();
            }

            return inv;
        }

        public DataTable GetAllAsDataTable(string text)
        {
            DataTable inv = new DataTable();
            string sqlcmd = text;
            using (SqlCommand cmd = new SqlCommand(sqlcmd, connect))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                inv.Load(dr);
                dr.Close();
            }

            return inv;
        }

        public void InsertStudent(string name, string l_name, string receipt_date, string speciality, string faculty)
        {
            string sqlcmd = string.Format("Insert into Pascal" +
                "(Name, Last_name, Receipt_date, Speciality, Faculty) Values(@Name, @Last_name, @Receipt_date, @Speciality, @Faculty)");
            using (SqlCommand cmd = new SqlCommand(sqlcmd, connect))
            {
                SqlParameter param = new SqlParameter();

                /*param.ParameterName = "@id";
                param.Value = id;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);*/

                param = new SqlParameter();
                param.ParameterName = "@Name";
                param.Value = name;
                param.SqlDbType = SqlDbType.Char;
                param.Size = 50;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@Last_name";
                param.Value = l_name;
                param.SqlDbType = SqlDbType.Char;
                param.Size = 50;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@Receipt_date";
                param.Value = receipt_date;
                param.SqlDbType = SqlDbType.Char;
                param.Size = 50;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@Speciality";
                param.Value = speciality;
                param.SqlDbType = SqlDbType.Char;
                param.Size = 50;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@Faculty";
                param.Value = faculty;
                param.SqlDbType = SqlDbType.Char;
                param.Size = 50;
                cmd.Parameters.Add(param);


                cmd.ExecuteNonQuery();
            }
        }

        public void mbox(string text)
        {
            MessageBox.Show(text, "Warning",
            MessageBoxButton.OK,
            MessageBoxImage.Information,
            MessageBoxResult.OK,
            MessageBoxOptions.DefaultDesktopOnly);
        }

    }
}
