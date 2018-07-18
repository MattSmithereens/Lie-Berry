using System;
using System.Collections.Generic;
using Library.Models;

namespace Library.Models.ViewModels
{
    public class AuthorsBooks
    {
        public List<Author> allAuthors { get; set; }
        public List<Book> allBooks { get; set; }

        public AuthorsBooks()
        {
            allAuthors = Author.GetAll();
            allBooks = Book.GetAll();
        }
    }
}
