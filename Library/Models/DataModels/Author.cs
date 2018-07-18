using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Library;

namespace Library.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Author(string name, int id = 0)
        {
            Id = id;
            Name = name;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO authors (name) VALUES (@name);";

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
            cmd.CommandText = @"UPDATE authors SET name = @name;";

            cmd.Parameters.AddWithValue("@id", this.Id);
            cmd.Parameters.AddWithValue("@name", newName);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }


        public static Author Find(int id)
        {
            {
                MySqlConnection conn = DB.Connection();
                conn.Open();

                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"SELECT * FROM author WHERE id = @Id;";

                MySqlParameter thisId = new MySqlParameter();
                thisId.ParameterName = "@Id";
                thisId.Value = id;
                cmd.Parameters.Add(thisId);

                var rdr = cmd.ExecuteReader() as MySqlDataReader;

                int authorId = 0;
                string authorName = "";

                while (rdr.Read())
                {
                    authorId = rdr.GetInt32(0);
                    authorName = rdr.GetString(1);
                }

                Author foundAuthor = new Author(authorName, authorId);

                conn.Close();
                if (conn != null)
                {
                    conn.Dispose();
                }

                return foundAuthor;
            }
        }
 

        public static List<Author> GetAll()
        {
            List<Author> allAuthors = new List<Author> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM authors;";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);

                Author newAuthor = new Author(name, id);
                allAuthors.Add(newAuthor);
            }

            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }

            return allAuthors;
        }

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM authors WHERE id = @id; DELETE authors_books WHERE name = @name;";

            cmd.Parameters.AddWithValue("@id", this.Id);
            cmd.Parameters.AddWithValue("@name", this.Name);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

    
    }
}
