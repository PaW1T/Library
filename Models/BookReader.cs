using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class BookReader
    {
        public int BookId { get; set; }
        public int ReaderId { get; set; }
        public int Amount { get; set; }
    }
}