using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT_Core.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Titulo { get; set; }
        public string Isbn { get; set; }
        public DateTime Date { get; set; }

        public ICollection<Models.Author> Authors { get; set; }

    }
}
