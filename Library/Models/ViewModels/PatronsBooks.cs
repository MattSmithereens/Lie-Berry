using System;
using System.Collections.Generic;
using Library.Models;

namespace Library.Models.ViewModels
{
    public class PatronsBooks
    {
        public List<Author> allAuthors { get; set; }
        public List<Book> allBooks { get; set; }
        public Patron patron { get; set; }
        public int bookId;

        public PatronsBooks(string id)
        {
            allAuthors = Author.GetAll();
            allBooks = Book.GetAll();

            int patronId = Int32.Parse(id);
            patron = Patron.Find(patronId);
        }

        public void ThisBook(string stringBookId)
        {
            int properId = Int32.Parse(stringBookId) - 1;
            bookId = properId;


        }
    }
}
