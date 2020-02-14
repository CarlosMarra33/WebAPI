using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT_Core.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime DatadeAniverssario { get; set; }

        public ICollection<Models.Book> Books { get; set; }

    }
}
