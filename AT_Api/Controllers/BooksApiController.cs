using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AT_Core.Models;
using AT_Data;

namespace AT_Api.Controllers
{
    public class BooksApiController : ApiController
    {
        private AT_Data.Repository.Bookrepository bookrepository = new AT_Data.Repository.Bookrepository();
        // GET: api/BooksApi
        public IQueryable<Book> GetBooks()
        {
            IQueryable<Book> book = bookrepository.GetAllBooks();
            return book;
        }

        // GET: api/BooksApi/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult GetBook(int id)
        {
            Book book = bookrepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/BooksApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != book.BookId)
            {
                return BadRequest();
            }
            try
            {
                bookrepository.UpdateBook(book);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/BooksApi
        [ResponseType(typeof(Book))]
        public IHttpActionResult PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bookrepository.CreateBook(book);

            return CreatedAtRoute("DefaultApi", new { id = book.BookId }, book);
        }

        // DELETE: api/BooksApi/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteBook(int id)
        {
            Book book = bookrepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            else
            {
                bookrepository.DeleteBook(book);
            }
            return Ok(book);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool BookExists(int id)
        {
            return bookrepository.RepositoryBookExists(id);
        }
    }
}