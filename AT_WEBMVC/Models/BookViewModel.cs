using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AT_WEBMVC.Models
{
    public class BookViewModel
    {
        [Key]
        public int BookId { get; set; }
        public string Titulo { get; set; }
        public string Isbn { get; set; }
        public DateTime Date { get; set; }

        public ICollection<Models.AuthorViewModel> Authors { get; set; }

    }
}