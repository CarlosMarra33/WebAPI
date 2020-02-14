using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AT_Core.Models;

namespace AT_Data.Repository
{
    public class AuthorsRepository
    {
        private Contexto db = new Contexto();

        public  IQueryable<Author> GetAllAuthors()
        {
            return db.Authors;
        }

        public Author GetAuthorById(int id)
        {
            Author author = db.Authors.Find(id);
            return author;
        }


        public void UpdateAuthor(Author author)
        {
            db.Entry(author).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void CreateAuthor(Author author)
        {
            db.Authors.Add(author);
            db.SaveChanges();
        }

        public void Delete(Author author)
        {
            db.Authors.Remove(author);
            db.SaveChanges();
        }

        public bool AuthorExists(int id)
        {
            return db.Authors.Count(e => e.AuthorId == id) > 0;
        }

    }
}
