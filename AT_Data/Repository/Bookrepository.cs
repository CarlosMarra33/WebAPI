using AT_Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT_Data.Repository
{
    public class Bookrepository
    {
        private Contexto db = new Contexto();

        public IQueryable<Book> GetAllBooks()
        {
            return db.Books;
        }

        public Book GetBookById(int id)
        {
            Book book = db.Books.Find(id);
            return book;
        }

        public void UpdateBook(Book book)
        {
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void CreateBook(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
        }

        public void DeleteBook(Book book)
        {
            db.Books.Remove(book);
            db.SaveChanges();
        }

        public bool RepositoryBookExists(int id)
        {
            return db.Books.Count(e => e.BookId == id) > 0;
        }

    }
}
