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
    public class AuthorsApiController : ApiController
    {
        private AT_Data.Repository.AuthorsRepository authorsRepository = new AT_Data.Repository.AuthorsRepository();

        // GET: api/AuthorsApi
        public IQueryable<Author> GetAuthors()
        {
            IQueryable<Author> authors = authorsRepository.GetAllAuthors();
            return authors ;
        }

        // GET: api/AuthorsApi/5
        [ResponseType(typeof(Author))]
        public IHttpActionResult GetAuthor(int id)
        {
            Author author = authorsRepository.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        // PUT: api/AuthorsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAuthor(int id, Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != author.AuthorId)
            {
                return BadRequest();
            }
            try
            {
                authorsRepository.UpdateAuthor(author);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // POST: api/AuthorsApi
        [ResponseType(typeof(Author))]
        public IHttpActionResult PostAuthor(Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            authorsRepository.CreateAuthor(author);

            return CreatedAtRoute("DefaultApi", new { id = author.AuthorId }, author);
        }

        // DELETE: api/AuthorsApi/5
        [ResponseType(typeof(Author))]
        public IHttpActionResult DeleteAuthor(int id)
        {
            Author author = authorsRepository.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }
            else
            {
                authorsRepository.Delete(author);
            }

            return Ok(author);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool AuthorExists(int id)
        {
            return authorsRepository.AuthorExists(id);
        }
    }
}