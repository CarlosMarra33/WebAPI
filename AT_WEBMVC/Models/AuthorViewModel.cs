using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AT_WEBMVC.Models
{
    public class AuthorViewModel
    {
        [Key]
        public int AuthorId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime DatadeAniverssario { get; set; }

        public ICollection<Models.BookViewModel> Books { get; set; }


    }
}