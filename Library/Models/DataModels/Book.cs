﻿using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Library;

namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Copies { get; set; }
        public List<Author> Authors { get; set; } 

        public Book(string title, int copies, int id = 0)
        {
            Id = id;
            Title = title;
            Copies = copies;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO books (title, copies) VALUES (@title, @copies);";

            cmd.Parameters.AddWithValue("@title", this.Title);
            cmd.Parameters.AddWithValue("@copies", this.Copies);


            cmd.ExecuteNonQuery();

            Id = (int)cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Edit(string newTitle, int newCopies)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE books SET title = @title, copies = @copies WHERE book_id = @id;";

            cmd.Parameters.AddWithValue("@id", this.Id);
            cmd.Parameters.AddWithValue("@title", newTitle);
            cmd.Parameters.AddWithValue("@copies", newCopies);

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
            cmd.CommandText = @"DELETE FROM books WHERE id = @id; UPDATE patrons_books SET returned = 'true' WHERE book_id = @id;";

            cmd.Parameters.AddWithValue("@id", this.Id);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Book> GetAll()
        {
            List<Book> allBooks = new List<Book> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM books;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string title = rdr.GetString(1);
                int copies = rdr.GetInt32(2);

                Book newBook = new Book(title, copies, id);
                allBooks.Add(newBook);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return allBooks;
        }

        public static Book Find(int withId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM books WHERE id = (@id);";

            cmd.Parameters.AddWithValue("@id", withId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int id = 0;
            string title =  "";
            int copies =  0;

            while (rdr.Read())
            {
                id = rdr.GetInt32(0);
                title = rdr.GetString(1);
                copies = rdr.GetInt32(2);
            }

            Book newBook = new Book(title, copies, id);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return newBook;
        }

        public void AttributeTo(Author author)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO authors_books (author_id, book_id) VALUES (@author_id, @book_id);";

            cmd.Parameters.AddWithValue("@book_id", this.Id);
            cmd.Parameters.AddWithValue("@author_id", author.Id);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<string> ListAuthors()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT authors.* FROM
                                books JOIN authors_books ON (books.id = authors_books.book_id)
                                      JOIN authors ON (authors_books.author_id = authors.id)
                                WHERE books.id = @id;";
            
            cmd.Parameters.AddWithValue("@id", this.Id);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            List<string> AuthorNames = new List<string>();

            while (rdr.Read())
            {
                string name = rdr.GetString(1);
                AuthorNames.Add(name);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return AuthorNames;

        }

    }
}