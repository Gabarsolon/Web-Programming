using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AspCoreMVCEF.Models;
using MySql.Data.MySqlClient;

namespace AspCoreMVCEF.DataAbstractionLayer
{
    public class DAL
    {
        public Student GetStudentByName(string name)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd=;database=wp;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select * from student where name='" + name + "'";
                MySqlDataReader myreader = cmd.ExecuteReader();

                Student stud = new Student();
                if (myreader.Read())
                {
                    stud.Id = myreader.GetInt32("id");
                    stud.Name = myreader.GetString("name");
                    stud.Password = myreader.GetString("password");
                    stud.Group_id = myreader.GetInt32("group_id");
                }
                myreader.Close();
                return stud;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }
            return null;

        }

        public List<Student> GetAllStudents()
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd=;database=wp;";
            List<Student> slist = new List<Student>();

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select * from student";
                MySqlDataReader myreader = cmd.ExecuteReader();

                while (myreader.Read())
                {
                    Student stud = new Student();
                    stud.Id = myreader.GetInt32("id");
                    stud.Name = myreader.GetString("name");
                    stud.Password = myreader.GetString("password");
                    stud.Group_id = myreader.GetInt32("group_id");
                    slist.Add(stud);
                }
                myreader.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.ToString());
            }
            return slist;

        }

        public List<Student> GetStudentsFromGroup(int group_id)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd=;database=wp;";
            List<Student> slist = new List<Student>();

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select * from student where group_id=" + group_id;
                MySqlDataReader myreader = cmd.ExecuteReader();

                while (myreader.Read())
                {
                    Student stud = new Student();
                    stud.Id = myreader.GetInt32("id");
                    stud.Name = myreader.GetString("name");
                    stud.Password = myreader.GetString("password");
                    stud.Group_id = myreader.GetInt32("group_id");
                    slist.Add(stud);
                }
                myreader.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }
            return slist;

        }

        public void SaveStudent(Student stud)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd=;database=wp;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "insert into student values(" + stud.Id + ",'" + stud.Name + "','" + stud.Password + "'," + stud.Group_id + ")";
                cmd.ExecuteNonQuery();

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }

        }

    }
}
