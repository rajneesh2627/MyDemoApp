using EL.Student;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Student
{
    public class StudentDataOperation
    {
        ////SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);

        /// <summary>
        /// Saves student entity.
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int SaveStudent(StudentEntity student)
        {
            string a = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=MyTestDatabase;User ID=sa;Password=123456";
            SqlConnection conn1 = new SqlConnection(a);
            conn1.Open();
            SqlCommand myCommand = new SqlCommand();
            myCommand.Connection = conn1;
            myCommand.CommandType = CommandType.Text;
            myCommand.CommandText = "insert into Studentinformation (Name, Age, Gender) VALUES (@Name, @Age, @Gender)";
            myCommand.Parameters.AddWithValue("@Name", student.Name);
            myCommand.Parameters.AddWithValue("@Age", student.Age);
            myCommand.Parameters.AddWithValue("@Gender", student.Gender);
            int rowsAffected = myCommand.ExecuteNonQuery();
            conn1.Close();
            return rowsAffected;
        }

        /// <summary>
        /// Searches a student details by Id.
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public StudentEntity SearchStudentById(StudentEntity student)
        {
            string a = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=MyTestDatabase;User ID=sa;Password=123456";
            SqlConnection conn = new SqlConnection(a);
            conn.Open();
            SqlCommand myCommand = new SqlCommand();
            myCommand.Connection = conn;
            myCommand.CommandType = CommandType.Text;
            myCommand.CommandText = "select * from Studentinformation where id = @id";
            myCommand.Parameters.AddWithValue("@id", student.Id);
            SqlDataReader reader = null;
            reader = myCommand.ExecuteReader();
            StudentEntity studentReturn = null;
            if (reader != null)
            {
                while(reader.Read())
                {
                    studentReturn = new StudentEntity();
                    studentReturn.Name = reader["Name"].ToString(); ;
                    studentReturn.Age = Convert.ToInt32(reader["Age"]);
                    studentReturn.Gender = Convert.ToChar(reader["Gender"]);
                }
            }
            return studentReturn;
        }

        /// <summary>
        /// Searches a student details by name.
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public List<StudentEntity> SearchStudentByName(StudentEntity student)
        {
            string a = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=MyTestDatabase;User ID=sa;Password=123456";
            SqlConnection conn = new SqlConnection(a);
            List<StudentEntity> studentList = new List<StudentEntity>();
            conn.Open();
            SqlCommand myCommand = new SqlCommand();
            myCommand.Connection = conn;
            myCommand.CommandType = CommandType.Text;
            myCommand.CommandText = "select * from Studentinformation where name = @name";
            myCommand.Parameters.AddWithValue("@name", student.Name);
            SqlDataReader reader = null;
            reader = myCommand.ExecuteReader();
            StudentEntity studentReturn = null;
            if (reader != null)
            {
                while (reader.Read())
                {
                    studentReturn = new StudentEntity();
                    studentReturn.Id = Convert.ToInt32(reader["Id"]);
                    studentReturn.Name = reader["Name"].ToString(); ;
                    studentReturn.Age = Convert.ToInt32(reader["Age"]);
                    studentReturn.Gender = Convert.ToChar(reader["Gender"]);
                    studentList.Add(studentReturn);
                }
            }
            return studentList;
        }

        /// <summary>
        /// Updates a student record.
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int UpdateStudent(StudentEntity student)
        {
            string a = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=MyTestDatabase;User ID=sa;Password=123456";
            SqlConnection conn = new SqlConnection(a);
            conn.Open();
            SqlCommand myCommand = new SqlCommand();
            myCommand.Connection = conn;
            myCommand.CommandType = CommandType.Text;
            myCommand.CommandText = "update Studentinformation set name = @name , age = @age, gender = @gender where id = @id";
            myCommand.Parameters.AddWithValue("@id", student.Id);
            myCommand.Parameters.AddWithValue("@age", student.Age);
            myCommand.Parameters.AddWithValue("@name", student.Name);
            myCommand.Parameters.AddWithValue("@gender", student.Gender);
            int rowsAffected = myCommand.ExecuteNonQuery();
            conn.Close();
            return rowsAffected;
        }

        /// <summary>
        /// Deletes a student record.
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int DeleteStudent(StudentEntity student)
        {
            string a = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=MyTestDatabase;User ID=sa;Password=123456";
            SqlConnection conn = new SqlConnection(a);
            conn.Open();
            SqlCommand myCommand = new SqlCommand();
            myCommand.Connection = conn;
            myCommand.CommandType = CommandType.Text;
            myCommand.CommandText = "delete from Studentinformation where id = @id";
            myCommand.Parameters.AddWithValue("@id", student.Id);
            int rowsAffected = myCommand.ExecuteNonQuery();
            conn.Close();
            return rowsAffected;
        }
    }
}
