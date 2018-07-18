using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Library;

namespace Library.Models
{
    public class Patron
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> History { get; set; }

        public Patron(string name, int id = 0)
        {
            Id = id;
            Name = name;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO patrons (name) VALUES (@name);";

            cmd.Parameters.AddWithValue("@name", this.Name);

            cmd.ExecuteNonQuery();

            Id = (int)cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Edit(string newName)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE patrons SET name = @name WHERE id = @id;";

            cmd.Parameters.AddWithValue("@id", this.Id);
            cmd.Parameters.AddWithValue("@name", newName);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }


        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM patrons WHERE id = @id;";

            cmd.Parameters.AddWithValue("@id", this.Id);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Patron> GetAll()
        {
            List<Patron> allPatrons = new List<Patron> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM patrons;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);

                Patron newPatron = new Patron(name, id);
                allPatrons.Add(newPatron);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return allPatrons;
        }

        public static Patron Find(int withId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM patrons WHERE id = (@id);";

            cmd.Parameters.AddWithValue("@id", withId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int id = 0;
            string name = "";

            while (rdr.Read())
            {
                id = rdr.GetInt32(0);
                name = rdr.GetString(1);
            }

            Patron newPatron = new Patron(name, id);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return newPatron;
        }
    }
}
